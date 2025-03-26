using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adding the Kafka Producer service
builder.Services.AddSingleton<IProducer<Null, string>>(sp => {
    var config = new ProducerConfig {
        BootstrapServers = builder.Configuration["Kafka:BootstrapServers"]
    };
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();

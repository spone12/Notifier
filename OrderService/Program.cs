using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adding Kafka Producer
builder.Services.AddSingleton<IProducer<Null, string>>(sp => {
    var config = new ProducerConfig {
        BootstrapServers = builder.Configuration["Kafka:BootstrapServers"] ?? "localhost:9092"
    };
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();  // Add routing
app.UseEndpoints(endpoints => {  
    endpoints.MapControllers();  // Clearly we're mapping controllers
});

app.Urls.Add("http://0.0.0.0:5082");
app.Run();



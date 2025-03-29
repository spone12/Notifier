using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using System.Threading;

class Program
{
    static void Main()
    {
        // Загружаем конфигурацию
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var kafkaConfig = new ConsumerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"],
            GroupId = config["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(kafkaConfig).Build();
        consumer.Subscribe(config["Kafka:Topic"]);

        Console.WriteLine("NotificationService is listening for new orders...");

        try
        {
            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume(CancellationToken.None);
                    var order = JsonSerializer.Deserialize<Order>(consumeResult.Message.Value);
                    
                    Console.WriteLine($"[NotificationService] Sending notification: New order received!");
                    Console.WriteLine($"Order ID: {order.Id}, Product: {order.ProductName}, Price: {order.Price:C}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}

public class Order
{
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
}

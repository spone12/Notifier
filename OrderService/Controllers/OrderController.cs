using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic = "orders.created";

    public OrderController(IProducer<Null, string> producer) {
        _producer = producer;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order) {
        var message = JsonSerializer.Serialize(order);
        
        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        
        return Ok(new { Message = "Order created", Order = order });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var message = JsonSerializer.Serialize(new { test_orders = 1 });

        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });

        return Ok(new { Message = "Orders fetched", Orders = "test order" });
    }
}

public class Order {
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
}

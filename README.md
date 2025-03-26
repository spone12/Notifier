Запуск микросервиса Order на kafka вместе с Dotnet \

`docker-compose up -d && \` \
`cd OrderService/ && dotnet run`

Создание продукта:

```
curl -X POST http://localhost:5082/api/orders \
-H "Content-Type: application/json" \
-d '{"id": 1, "productName": "Laptop", "price": 1200.00}'
```

Маршрутизация с примерами Orders: `OrderService/OrderService.http` 
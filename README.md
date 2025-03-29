+ **Packages:**
    - Docker
    - Kafka
    - .Net 9

**Запуск микросервисов через docker:**
  - docker-compose up --build

**Логи:**
 - docker-logs -f [CONTAINER]
   - order-service
   - notification-service

*Старый вариант запуска*: 
  Запуск микросервиса Order через брокер сообщений Kafka вместе с DotNet
  - `docker-compose up -d && \` \
`cd OrderService/ && dotnet run`

Создание продукта:

```
curl -X POST http://localhost:5082/api/orders \
-H "Content-Type: application/json" \
-d '{"id": 1, "productName": "Laptop", "price": 1200.00}'
```

Маршрутизация с примерами Orders: `OrderService/OrderService.http` 
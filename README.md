# Exchange-Notifier

Exchange Notifier demo.
Notify via Telegram when some rules in yout favorite exchages satisfy the condition

# Documentation

We cover:

- [x] .net server worker
- [x] node js worker
- [x] mongo db databases
- [x] rabbitMQ
- [x] polly
- [x] flurl
- [x] mediatR
- [] python

To exceute
Crete a User Secret json / or add enviroment variables to the .net ExchangeHttpWorker project
{
"NETCORE_HUB_SERVER": "http://localhost:5277",
"REQUEST_RATE_IN_SECONDS": 180,
"RABBITMQ_HOST_NAME": "localhost",
"RABBITMQ_PORT": 15672,
"RABBITMQ_CONSUMER_CONCURRENCY": 50,
"RABBITMQ_USER_NAME" : "YOUR USERNAME",
"RABBITMQ_USER_PASS" : "YOUR PASS",
"RAPIDAPI_BASE_URL" : "https://currency-conversion-and-exchange-rates.p.rapidapi.com",
"X-RAPIDAPI-KEY" : "YOUR KEY",
"X-RAPIDAPI-HOST" : "currency-conversion-and-exchange-rates.p.rapidapi.com",
"TELEGRAM_BOT_BASE_URL" : "https://api.telegram.org/bot",
"TELEGRAM_BOT_TOKEN" : "YOUR TOKEN"
}

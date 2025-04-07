# Gtresury-Ticketing
Event and Tciketing API for demo purposes
#Introduction
This application contains two APIs: 1. The Events API is used by Events Admin for events, creations, deletion, updation, and making it live.
The second web service is Ticket.api used by end users to reserve ticket, view available ticket, and confirm bookings 
The third web service (reporting service,) which was not implemented,d would have handled the Reporting requirements of the application.

The whole application follows microservices architecture, which allows loose coupling between different domain areas.
Rabbitmq is used as a message broker, Mass tranist is used as wa rapper around rabbitmq to simplify   producer/consumer interaction 

#Technologies used.

1. Asp.Net Web API
2. Redis cache 
3. Sql server
4. Rabbit mq
5. Docker compose .


4. Setup .

1 . clone the repo at https://github.com/abhinay22/Gtresury-Ticketing.git

2. restore the projects package dependencies using 
 - dotnet restore (or through visual studio)

3. run the docker-compose file in repository to setup (docker compose up  -d )
   - redis
   - rabbitmq
   - sqlserver 

4. execute the script1.sql and script2.sql file to create EventDBContext and TicketingDBContext databases


# APi documentation 

Event.api


| API Endpoint                              | Endpoint Job                          | Verb  |
|-------------------------------------------|----------------------------------------|--------|
| `/api/v1/Event/GetEvent?{EventID=2}`      | Gets an existing event via eventId     | GET    |
| `/api/v1/Event/changeEventData/{id}`      | Updates the event metadata             | PUT    |
| `/api/v1/Event/CreateEvent`               | Creates a new event                    | POST   |
| `/api/v1/Event/activateEvent/{id}`        | Patch event details                    | PATCH  |


More details on api are available on swagger files in the Documents directory of the repo


Ticket.api

Tickting api listens to the EventActivated event emitted by Event.api and consumes it to create its copy of Event metadata

| API Endpoint                                             | Endpoint Job                                      | Verb  |
|----------------------------------------------------------|---------------------------------------------------|--------|
| `/api/v1/Ticketing/GetAvailableTicketDetails/{id}`       | Gets all the tickets available for an event       | GET    |
| `/api/v1/Ticketing/BookTicket`                           | Reserves the ticket in cache with TTL of 5        | POST   |
| `/api/v1/Ticketing/MakePayment/{id}`                     | Makes a payment against a booking ID              | POST   |



# Please note there are two additional jobs/services required for this app

1 . EventReporting API (not implemented): This would have listened to the TicketConfirmed Event of the Ticket api and created its database 
  according to it's quering requirement 

2. Currently, tickets expired in the  cache are not getting evaluated. -For this, we would need a redis pub/sub job or cron job to look
   for expired tickets and update the sql database Inventory according .,













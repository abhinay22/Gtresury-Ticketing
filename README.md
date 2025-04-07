# Gtresury-Ticketing
Event and Tciketing API for demo purposes
#Introduction
This application contains two api's: 1. The Events api used by Events Admin for events, creations, deletion, updation, and making it live .
The second web service is Ticket.api used by end user to reserve ticket , view available ticket and confirm booking 
The third web service (reporting service) which was not implemented would have handled the Reporting requirments of the application .

The whole application follows microservices architecure which allows loose coupling between different domain areas .
Rabbit mq is used as message broker , Mass tranist is used as wrapper around rabbit mq to simplify  btter producer/consumer interaction 

#Technologies used .

1. Asp.Net Web API
2. Redis cache 
3. Sql server
4. Rabbit mq
5. Docker compose .


4. Setup .

1 . clone the repo at https://github.com/abhinay22/Gtresury-Ticketing.git

2. restore the projects package dependencies using 
 - dotnet restore (or through visual studio)

3. run the docker-compose file in repository to setup (docker compose -d )
   - redis
   - rabbitmq
   - sqlserver 

4. execute the script1.sql and script2.sql file to create EventDBContext and TicketingDBContext databases


# APi documentation 

Event.api


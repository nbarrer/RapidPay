# RapidPay

## Basic Authentication

The basic authentication for API method is based on user stored on database. 
The default credentials for authenticated on API endpoints are:
>*user: test*
>*password: test*

A helper class was created to improve basic authentication based on claims-authorization
creating an identity with the user credentials.

After running the solution the API can be tested on https://localhost:44324/swagger/index.html

## API Performance

To improve perfomance an enabling multithreading each service method has the key asyn allowing
the method to be asynchronous and all repository class methods returns awaitable objects.

## Database design

A local SQL Server database was created to store information. Entity Framework core was used 
as ORM to improve calls to database. Repository pattern was implemented to encapsulate 
database logic and separate data access layer from service layer.



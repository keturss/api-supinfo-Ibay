# Example of RESTful Web API with JWT Implementation in .Net Core
Implementation of JWT token in .Net Core. This projec uses Entity Framework, Repository and JWT.

## Project Includes

- .Net Core 3.1 Web API
- JWT Token Implimentation
- Generic Repository Layer
- Generic Service Layer
- Entity Framework Data Layer


## Packages used
```
Microsoft.Identity.Web
Swashbuckle.AspNetCore
AutoMapper
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Authentication.OpenIdConnect
Microsoft.AspNetCore.OpenApi
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.Extensions.ApiDescription.Client
Pomelo.EntityFrameworkCore.MySql
```

## How to run?
1. Download or clone project [`https://github.com/keturss/api-supinfo-Ibay.git`](https://github.com/keturss/api-supinfo-Ibay.git)
2. Open in visual studio, then Build and Run
3. You need [Postman](https://www.postman.com/) to test this application because there is no UI includeden in this project.
4. With Postman you will be able to import the Postman package in the repo


## Database Table script
Update the dotnet database by running the command below

```
dotnet ef database update
```
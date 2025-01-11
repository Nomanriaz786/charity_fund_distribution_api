# Charity Fundraising DBMS API

## Setup Instructions

1. Create a new web project:

    ```bash
    dotnet new web -o Charity_Fundraising_DBMS -f net8.0
    ```

2. Install required packages:

    ```bash
    cd Charity_Fundraising_DBMS  # Make sure you're in your project directory
    dotnet add package Microsoft.EntityFrameworkCore.sqlserver
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite 
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Swashbuckle.AspNetCore --version 6.5.0
    dotnet add package Microsoft.AspNetCore.OpenApi --version 7.0.0
    ```

## Initial Program Setup

Replace the contents of your Program.cs with:

```csharp
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { 
         Title = "Charity Fundraising DBMS API",          
         Description = "Managing charity fundraising and distribution",  
         Version = "v1"                     
     });
});
    
var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity Fundraising DBMS API V1");
   });
}
    
app.MapGet("/", () => "Welcome to Charity Fundraising DBMS!");
    
app.Run();
```

## Database Setup

To scaffold your database context, run:

```bash
dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=Charity_Fundraising_DBMS;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
```

# Pizza Store API

## Setup Instructions

1. Create a new web project:

    ```bash
    dotnet new web -o PizzaStore -f net8.0  # You can change 'PizzaStore' to your preferred project name
    ```

2. Install required packages:

    ```bash
    cd PizzaStore  # Make sure you're in your project directory
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
         Title = "PizzaStore API",            // Customize your API title
         Description = "Making the Pizzas you love",  // Add your API description
         Version = "v1"                       // Change version as needed
     });
});
    
var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
   });
}
    
app.MapGet("/", () => "Hello World!");
    
app.Run();
```

## Database Setup

To scaffold your database context, run:

```bash
dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook" Microsoft.EntityFrameworkCore.SqlServer
```

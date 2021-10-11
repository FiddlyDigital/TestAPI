# Test WebAPI

## Features

- .Net5 Web API
- Unit of Work / Reposititory Pattern for Data Access

## Get going

### Commands
Build:```dotnet build```

Run: ```dotnet run```

Swagger doc: ```https://localhost:5001/swagger/v1/swagger.json```

### Migrations

Need to define the startup project when doing migrations due to DI setup. e.g.
```dotnet ef migrations add "Migration Name" --startup-project ../TestAPI```

Same with dierct DB Updates:
```dotnet ef database update --startup-project ../TestAPI```

## Sources

- https://dev.to/moe23/step-by-step-repository-pattern-and-unit-of-work-with-asp-net-core-5-3l92
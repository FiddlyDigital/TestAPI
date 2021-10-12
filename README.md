# Test WebAPI

## Features

- .Net5 Web API
- Unit of Work / Reposititory Pattern for Data Access
- API Versioning
- JWT Authentication/Authorisation

## Get going

### Commands
Build:```dotnet build```

Run: ```dotnet run```

### Migrations

Need to define the startup project when doing migrations due to DI setup. e.g.
```dotnet ef migrations add "Migration Name" --startup-project ../TestAPI```

Same with dierct DB Updates:
```dotnet ef database update --startup-project ../TestAPI```

## Sources

- https://dev.to/moe23/step-by-step-repository-pattern-and-unit-of-work-with-asp-net-core-5-3l92
- https://www.youtube.com/watch?v=2UlQMx3DuV0
- https://www.youtube.com/watch?v=uSNqKQEtRdw
- https://www.youtube.com/watch?v=oWPiBHh3eNc
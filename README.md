# SimpleConfigAccess
Simple class to access appsettings.json files like a dictionary (same as ASP.NET Core IConfiguration).

## usage
```csharp
Configuration configuration = new Configuration(pathToConfigFile);
string connectionString = configuration["Database:ConnectionString"]; 
``` 

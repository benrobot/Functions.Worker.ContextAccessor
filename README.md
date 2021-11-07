# FunctionContext accessor .NET 5+ (.NET isolated) Azure Functions

## Usage

Add to your `Program.cs` file
```csharp
.ConfigureFunctionsWorkerDefaults(applicationBuilder => applicationBuilder.UseFunctionContextAccessor())
```
and
```csharp
.ConfigureServices(services => services.AddFunctionContextAccessor())
```

Final result would look something like this:<br/>
`Program.cs`
```csharp
using Functions.Worker.ContextAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(configBuilder => configBuilder.AddEnvironmentVariables())
    .ConfigureFunctionsWorkerDefaults(applicationBuilder => applicationBuilder.UseFunctionContextAccessor())
    .ConfigureServices(services => services.AddFunctionContextAccessor())
    .ConfigureServices(Startup.Configure)
    .UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    })
    .Build();

host.Run();
```

### Scope Validation

It is strongly recommended that scopes are validated as seen by the inclusion of
```csharp
.UseDefaultServiceProvider((_, options) =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
})
```
because unintended behavior may occur when there's no validation.

## Why

As of this writting, the interface [IFunctionsWorkerMiddleware](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.functions.worker.middleware.ifunctionsworkermiddleware) does NOT have a IFunctionsWorkerMiddlewareFactory counterpart and gets registered as a singleton. As opposed to ASP.NET Core's [IMiddleware](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.imiddleware) which DOES have an [IMiddlewareFactory](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.imiddlewarefactory) counterpart and will allow the middleware to be registered with either a scoped or instance lifetime. This resulted in scoped access to the FunctionContext only being available to the function method and context information cannot be easily injected into other parts of the application.

This library is modeled after the implementation of [HttpContextAccessor](https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http/src/HttpContextAccessor.cs) (usage seen [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context)) and is inspired by [@dolphinspired's gist](https://gist.github.com/dolphinspired/796d26ebe1237b78ee04a3bff0620ea0).

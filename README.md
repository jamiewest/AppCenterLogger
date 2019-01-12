# AppCenter Logger
[![Build status](https://dev.azure.com/jamiewest/AppCenterLogger/_apis/build/status/AppCenterLogger-CI)](https://dev.azure.com/jamiewest/AppCenterLogger/_build/latest?definitionId=27)

A Microsoft [AppCenter](https://appcenter.ms/) logger provider implementation for `Microsoft.Extensions.Logging`. 

Visual Studio App Center is the next generation of Xamarin Test Cloud, and includes all the functionality of Xamarin Test Cloud and more. This logger is intended to be used in Xamarin.Forms projects that make use of `Microsoft.Extensions.Hosting` to manage basic app functions like logging, DI, and configuration. See [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2) for more information regarding .NET Generic Host.

This package uses dependency injection from `Microsoft.Extensions.DependencyInjection` to handle the logger injection. Here is a [link](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2) where you can read more about Microsoft's DI.


To add a provider, call the provider's Add{provider name} extension method.

## Example Usage
```csharp
    var host = new HostBuilder()
        .ConfigureLogging((hostContext, logging) =>
        {
            logging.AddAppCenter(options => options.AppCenterIosSecret == "<Your AppCenter iOS Secret>");
        })
```

Above we use a Generic HostBuilder to add in our logger implementation and specify an iOS secret (*obtained from AppCenter*). Add least one iOS, Android, or UWP secret needs to be specified in the options.

## Installation

You can add this library to your project using [NuGet][nuget].

**Package Manager Console**
Run the following command in the “Package Manager Console”:

> PM> Install-Package West.Extensions.Logging.AppCenter

**Visual Studio**
Right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘West.Extensions.Logging.AppCenter’ and click ‘Install’.
([see NuGet Gallery][nuget-gallery].)

**.NET Core Command Line Interface**
Run the following command from your favorite shell or terminal:

> dotnet add package West.Extensions.Logging.AppCenter

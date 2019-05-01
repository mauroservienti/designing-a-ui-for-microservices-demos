# Designing a UI for Microservices - Demos

This repository contains all the demos run during the [`Designing a UI for Microservices` talk](https://milestone.topics.it/talks/designing-ui-for-microservices.html).

ViewModel Composition is deeply discussed on my blog in the [ViewModel Composition](https://milestone.topics.it/categories/view-model-composition) category.

## Requirements

* Visual Studio 2017 with [.NET Core 2.2.x](https://www.microsoft.com/net/download/dotnet-core/2.2) support (Community Edition is supported), available for download at [https://www.visualstudio.com/downloads/](https://www.visualstudio.com/downloads/)

## ASP.Net Core API Gateway - demos

The `ASP.Net Core API Gateway`, 01 and 02, solution demos ViewModel Composition techniques built on top of .Net Core.

### CompositionGateway

`CompositionGateway` project shows how to create and host a .Net Core API Gateway, or reverse proxy, that composes HTTP requests to multiple API backends.

To run this sample ensure that the following projects are set as startup projects:

* `Sales.Api`
* `Shipping.Api`
* `Warehouse.Api`
* `Catalog.Api`
* `CompositionGateway`

As client to test the funzionality a REST client such as [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en) can be used.

* `ASP.Net Core API Gateway - 01` demoes how single items composition works
* `ASP.Net Core API Gateway - 02` demoes how single items and items list composition works

The [postman-collection.json](postman-collection.json) file can be imported into Postman, creating a collection with all the HTTP requests needed to test both demos.

## ASP.Net Mvc Core - Demo

`WebApp` project is a .Net Core Mvc app that composes HTTP requests to multiple backends directly in Mvc Views as Controllers are invoked. This demo implements ViewModel Composition concepts introducing the concept of Branding as a contract at the UI level.

To run this sample ensure that the following projects are set as startup projects:

* `Sales.Api`
* `Shipping.Api`
* `Warehouse.Api`
* `Catalog.Api`
* `WebApp`

## ASP.Net Mvc Core UI Composition - Demo

`WebApp` project is a .Net Core Mvc app that composes HTTP requests to multiple backends directly in Mvc Views as Controllers are invoked.  This demo implements ViewModel Composition and UI Composition concepts.

To run this sample ensure that the following projects are set as startup projects:

* `Sales.Api`
* `Shipping.Api`
* `Warehouse.Api`
* `Catalog.Api`
* `WebApp`

## Note

To simplify as much as possible the sample, frontend applications, such as `CompositionGateway` and `WebApp`, directly reference ViewModel Composition and UI Composition assemblies. This allows also to avoid the usage of Visual Studio post build events, that at the time of this writing are [affected by a bug](https://github.com/dotnet/sdk/issues/677) when used in combination with .NET Core, causing events variable to not be populated as expected.

A workaround is to use a `MSBuild` `Exec` task to simulate post build events. E.g. in order to try to run samples removing all hard-coded references:

1. Remove references to composition assemblies from `CompositionGateway` and `WebApp` project(s)
2. Edit the project file of composition assemblies that need to be copied to `CompositionGateway` and `WebApp` output folders by adding the following to each of them:

```xml
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="
     copy &quot;$(TargetDir)$(TargetName).dll&quot; &quot;$(SolutionDir)CompositionGateway\$(OutDir)$(TargetName).dll&quot; /Y /B
     copy &quot;$(TargetDir)$(TargetName).pdb&quot; &quot;$(SolutionDir)CompositionGateway\$(OutDir)$(TargetName).pdb&quot; /Y /B" />
  </Target>
```

Replacing `CompositionGateway` with `WebApp` for the last 2 demos.

NOTE:

* All solutions are configured to use the [SwitchStartupProject](https://marketplace.visualstudio.com/items?itemName=vs-publisher-141975.SwitchStartupProject) Visual Studio Extension to manage startup projects. The extension is not a requirement, but it's comfortable.

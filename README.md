# Designing a UI for Microservices - Demos

This repository contains all the demos run during the `Designing a UI for Microservices` talk

* [DDD SouthWest 2017 release](https://github.com/mauroservienti/Designing-a-UI-for-Microservices-Demos/releases/tag/DDD-SW-2017) - [event details](http://milestone.topics.it/events/ddd-south-west.html).

Slides are available on [Slideshare](https://www.slideshare.net/mauroservienti/designing-a-ui-for-microservices).

## ASP.Net Core API Gateway - demo

The `ASP.Net Core API Gateway` solution demoes UI Composition techniques built on top of .Net Core (v1.1.2).

### Divergent.CompositionGateway

`Divergent.CompositionGateway` shows how to create and host a .Net Core API Gateway, or reverse proxy, that composes http requests to multiple API backends.

To run this sample ensure that the following projects are set as startup projects:

* `Divergent.Sales.API.Host`
* `Divergent.Shipping.API.Host`
* `Divergent.CompositionGateway`

As client to test the funzionality a REST client such as [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en) can be used.

* `ASP.Net Core API Gateway - 01` demoes how single items composition works
* `ASP.Net Core API Gateway - 02` demoes how single items and items list composition works

## ASP.Net Mvc Core - Demo

### Divergent.Frontend

`Divergent.Frontend` sample is a .Net Core Mvc app that composes http requests to multiple backends directly in Mvc Views as Controllers are invoked. This demo implements ViewModel Composition concepts introducing the concept of Branding as a contract at the UI level.

To run this sample ensure that the following projects are set as startup projects:

* `Divergent.Sales.API.Host`
* `Divergent.Shipping.API.Host`
* `Divergent.Frontend`

## ASP.Net Mvc Core UI Composition - Demo

### Divergent.Frontend

`Divergent.Frontend` sample is a .Net Core Mvc app that composes http requests to multiple backends directly in Mvc Views as Controllers are invoked.  This demo implements ViewModel and UI Composition concepts.

To run this sample ensure that the following projects are set as startup projects:

* `Divergent.Sales.API.Host`
* `Divergent.Shipping.API.Host`
* `Divergent.Frontend`

## Note

To simplify as much as possible the sample, frontend applications, such as `Divergent.CompositionGateway` and `Divergent.Frontend`, directly reference ViewModel and UI Composition components. This allows also to avoid the usage of Visual Studio post build events, that at the time of this writing are [affected by a bug](https://github.com/dotnet/sdk/issues/677) when used in combination with .NET Core, causing events variable to not be populated as expected.

A workaround is to use a `MSBuild` `Exec` task to simulate post build events. E.g. in order to try to run samples removing all hard coded references:

1. Remove references to composition assemblies from frontend project(s)
2. Edit the project file of composition assemblies that need to be copied to frontend output folders by adding the following:

```xml
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="
     copy &quot;$(TargetDir)$(TargetName).dll&quot; &quot;$(SolutionDir)Divergent.CompositionGateway\$(OutDir)$(TargetName).dll&quot; /Y /B
     copy &quot;$(TargetDir)$(TargetName).pdb&quot; &quot;$(SolutionDir)Divergent.CompositionGateway\$(OutDir)$(TargetName).pdb&quot; /Y /B" />
  </Target>
```


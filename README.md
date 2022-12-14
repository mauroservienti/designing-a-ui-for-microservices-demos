# Designing a UI for Microservices - Demos

This repository contains all the demos run during the [`Designing a UI for Microservices` talk](https://milestone.topics.it/talks/designing-ui-for-microservices.html).

ViewModel Composition is deeply discussed on my blog in the [ViewModel Composition](https://milestone.topics.it/categories/view-model-composition) category.

## Requirements

* [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

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

All four solutions contain integration tests demonstrating how to test composition scenarios.

To simplify as much as possible the sample, frontend applications, such as `CompositionGateway` and `WebApp`, directly reference ViewModel Composition and UI Composition assemblies.

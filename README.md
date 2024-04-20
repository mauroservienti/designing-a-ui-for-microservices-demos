# Designing a UI for Microservices - Demos

This repository contains all the demos run during the [`Designing a UI for Microservices` talk](https://milestone.topics.it/talks/designing-ui-for-microservices.html).

ViewModel Composition is deeply discussed on my blog in the [ViewModel Composition](https://milestone.topics.it/categories/view-model-composition) category.

## Requirements

* [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

## ASP.Net Core API Gateway - demos

The `ASP.Net Core API Gateway`, 01 and 02, solution demos ViewModel Composition techniques built on top of .Net Core.

### CompositionGateway

The `CompositionGateway` project shows how to create and host a .Net Core API Gateway or reverse proxy, that composes HTTP requests to multiple API backends.

To run this sample, ensure that the following projects are set as startup projects:

* `Sales.Api`
* `Shipping.Api`
* `Warehouse.Api`
* `Catalog.Api`
* `CompositionGateway`

## ASP.Net Mvc Core - Demo

The `WebApp` project is a .Net Core Mvc app that composes HTTP requests to multiple backends directly in Mvc Views as Controllers are invoked. This demo implements ViewModel Composition concepts, introducing Branding as a contract at the UI level.

To run this sample, ensure that the following projects are set as startup projects:

* `Sales.Api`
* `Shipping.Api`
* `Warehouse.Api`
* `Catalog.Api`
* `WebApp`

## ASP.Net Mvc Core UI Composition - Demo

`WebApp` project is a .Net Core Mvc app that composes HTTP requests to multiple backends directly in Mvc Views as Controllers are invoked.  This demo implements ViewModel Composition and UI Composition concepts.

To run this sample, ensure that the following projects are set as startup projects:

* `Sales.Api`
* `Shipping.Api`
* `Warehouse.Api`
* `Catalog.Api`
* `WebApp`

## Note

All four solutions contain integration tests demonstrating how to test composition scenarios.

To simplify the sample as much as possible, frontend applications, such as `CompositionGateway` and `WebApp`, directly reference ViewModel Composition and UI Composition assemblies.

### How to run demos

The repository root contains a [request.http](request.http) file listing some test requests. Using VS Code to execute requests can be used by adding the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client).

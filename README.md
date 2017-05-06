# Designing a UI for Microservices - Demos

This repository contains all the demos run during the [DDD SouthWest 2017 event](http://milestone.topics.it/events/ddd-south-west.html).

Slides are available on [docs.com](https://doc.co/TPZhk9).

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

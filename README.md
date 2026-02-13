# Designing a UI for Microservices - Demos

This repository contains all the demos run during the [`Designing a UI for Microservices` talk](https://milestone.topics.it/talks/designing-ui-for-microservices.html).

ViewModel Composition is deeply discussed on my blog in the [ViewModel Composition](https://milestone.topics.it/categories/view-model-composition) category.

## Requirements

The following requirements must be met to run the demos successfully:

- [Visual Studio Code](https://code.visualstudio.com/) and the [Dev containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers).
- [Docker](https://www.docker.com/get-started) must be pre-installed on the machine.

## How to configure Visual Studio Code to run the demos

- Clone the repository
  - On Windows, make sure to clone on a short path, e.g., `c:\dev`, to avoid any "path too long" error
- Open one of the demo folders in Visual Studio Code
- Make sure Docker is running
  - If you're using Docker for Windows with Hyper-V, make sure that the cloned folder, or a parent folder, is mapped in Docker
- Open the Visual Studio Code command palette (`F1` on all supported operating systems, for more information on VS Code keyboard shortcuts, refer to [this page](https://www.arungudelli.com/microsoft/visual-studio-code-keyboard-shortcut-cheat-sheet-windows-mac-linux/))
- Type `Reopen in Container`, the command palette supports auto-completion; the command should be available by typing `reop`

Wait for Visual Studio Code Dev containers extension to:

- download the required container images
- configure the docker environment
- configure the remote Visual Studio Code instance with the required extensions

> Note: no changes will be made to your Visual Studio Code installation; all changes will be applied to the VS Code instance running in the remote container

The repository `devcontainer` configuration will:

- Create one .NET-enabled container where the repository source code will be mapped
- Configure the VS Code remote instance with:
  - The C# extension (`ms-dotnettools.csharp`)
  - The PostgreSQL Explorer extension (`ckolkman.vscode-postgres`)

Once the configuration is completed, VS Code will show a new `Ports` tab in the bottom-docked terminal area. The `Ports` tab will list all the ports the remote containers expose.

## How to run the demos

To execute the demo, open the root folder in VS Code, press `F1`, and search for `Reopen in container`. Wait for the Dev Container to complete the setup process.

Once the demo content has been reopened in the dev container:

1. Press `F1`, search for `Run task`, and execute the desired task to build the solution or to build the solution and deploy the required data
2. Go to the `Run and Debug` VS Code section and select the command you want to execute.

The repository root contains a [request.http](request.http) file listing some test requests. Using VS Code to execute requests can be used by adding the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) that is automatically configured when using Dev Containers.

## ASP.Net Core API Gateway - 01

The `ASP.Net Core API Gateway - 01` solution demonstrates ViewModel Composition techniques built on top of .Net Core, showing how to compose data from multiple microservices into a single unified response at the API Gateway level.

### Projects

#### API Backend Services

- **Catalog.Api** - Manages product catalog information including product names, descriptions, and availability. Exposes endpoints for retrieving product details and lists of available products. Represents the Marketing/Product Catalog bounded context.

- **Sales.Api** - Manages product pricing information. Exposes endpoints for retrieving price data for products. Represents the Sales bounded context.

- **Warehouse.Api** - Manages product inventory information. Exposes endpoints for retrieving inventory levels and stock availability. Represents the Warehouse/Inventory bounded context.

- **Shipping.Api** - Manages shipping options for products. Exposes endpoints for retrieving available shipping methods. Represents the Shipping bounded context.

#### ViewModel Composition Components

- **Catalog.ViewModelComposition** - Contains composition handlers and event subscribers that request and compose catalog data (product names, descriptions) into the composed view model. Implements handlers for both product details and available products list views.

- **Sales.ViewModelComposition** - Contains composition handlers that request and compose pricing data into the composed view model. Subscribes to composition events to enrich product information with pricing details.

- **Warehouse.ViewModelComposition** - Contains composition handlers and event subscribers that request and compose inventory data into the composed view model, including stock levels and availability status.

- **Shipping.ViewModelComposition** - Contains composition handlers that request and compose shipping options data into the composed view model.

#### Frontend Application

- **CompositionGateway** - The main ASP.NET Core API Gateway application that hosts the composition infrastructure. Uses [ServiceComposer.AspNetCore](https://www.nuget.org/packages/ServiceComposer.AspNetCore) to orchestrate composition requests to multiple backend APIs and compose their responses into unified JSON view models. Acts as a reverse proxy with built-in ViewModel Composition capabilities.

#### Utility Projects

- **ConfigurationUtils** - Provides utility extension methods for configuring HTTP clients used by composition handlers, simplifying the registration of HTTP clients with specific base addresses.

- **JsonUtils** - Provides utility extensions for JSON serialization/deserialization with support for dynamic `ExpandoObject` types and PascalCase conversion, enabling flexible data composition scenarios.

#### Test Projects

- **Catalog.Api.Tests / Sales.Api.Tests / Shipping.Api.Tests / Warehouse.Api.Tests** - Unit and integration tests for the respective API backend services.

- **Composition.Tests** - Integration tests demonstrating how to test composition scenarios end-to-end, mocking backend APIs and verifying the composed view models returned by the CompositionGateway.

### Running the Demo

To run this sample, ensure that the following projects are set as startup projects:

- `Catalog.Api`
- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `CompositionGateway`

## ASP.Net Core API Gateway - 02

The `ASP.Net Core API Gateway - 02` solution builds upon the concepts from Demo 01, adding support for composition events and demonstrating more advanced ViewModel Composition patterns with event-driven communication between composition handlers.

### Projects

This solution contains all the same projects as Demo 01, with the following additions and enhancements:

#### API Backend Services

- **Catalog.Api** - Manages product catalog information. Enhanced to support both product details and available products list endpoints.

- **Sales.Api** - Manages product pricing information.

- **Warehouse.Api** - Manages product inventory information.

- **Shipping.Api** - Manages shipping options for products.

#### ViewModel Composition Components (Enhanced)

- **Catalog.ViewModelComposition** - Contains composition handlers for catalog data. **Enhanced with event publishing** to notify other composition handlers when product data is loaded.

- **Catalog.ViewModelComposition.Events** - **New in Demo 02**: Defines composition events (e.g., `AvailableProductsLoaded`) that enable decoupled communication between composition handlers. This allows composition handlers from different bounded contexts to react to and enrich data loaded by other handlers.

- **Sales.ViewModelComposition** - Contains composition handlers for pricing data. **Enhanced with event subscribers** that react to catalog events to enrich product data with pricing information.

- **Warehouse.ViewModelComposition** - Contains composition handlers for inventory data. **Enhanced with event subscribers** to enrich product data based on composition events.

- **Shipping.ViewModelComposition** - Contains composition handlers for shipping options data.

#### Frontend Application

- **CompositionGateway** - The ASP.NET Core API Gateway application with enhanced composition capabilities, supporting event-driven composition patterns for more sophisticated data aggregation scenarios.

#### Utility Projects

- **ConfigurationUtils** - HTTP client configuration utilities.

- **JsonUtils** - JSON serialization/deserialization utilities.

#### Test Projects

- **Catalog.Api.Tests / Sales.Api.Tests / Shipping.Api.Tests / Warehouse.Api.Tests** - Tests for backend APIs.

- **Composition.Tests** - Integration tests for composition scenarios, including tests for event-driven composition flows.

### Key Differences from Demo 01

Demo 02 introduces **Composition Events**, enabling:
- **Decoupled Composition**: Handlers can react to events published by other handlers without direct dependencies
- **Progressive Enhancement**: Data can be enriched in stages as different handlers process composition events
- **Better Separation of Concerns**: Each bounded context can independently contribute data to the composed view model

### Running the Demo

To run this sample, ensure that the following projects are set as startup projects:

- `Catalog.Api`
- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `CompositionGateway`

## ASP.Net Mvc Core

The `ASP.Net Mvc Core` solution demonstrates ViewModel Composition in an ASP.NET Core MVC application, where composition happens directly within the MVC pipeline. Unlike the API Gateway demos, this approach composes data at the web application level, integrating composition with MVC controllers and views.

### Projects

#### API Backend Services

- **Catalog.Api** - Manages product catalog information including product names, descriptions, and availability. Exposes RESTful endpoints for product data.

- **Sales.Api** - Manages product pricing information. Exposes RESTful endpoints for price data.

- **Warehouse.Api** - Manages product inventory information. Exposes RESTful endpoints for inventory data.

- **Shipping.Api** - Manages shipping options for products. Exposes RESTful endpoints for shipping methods.

#### ViewModel Composition Components

- **Catalog.ViewModelComposition** - Contains MVC-integrated composition handlers that retrieve and compose catalog data. Handlers are invoked during MVC request processing using `[HttpGet]` attributes matching MVC routes (e.g., `/`, `/products/details/{id}`).

- **Catalog.ViewModelComposition.Events** - Defines composition events (e.g., `AvailableProductsLoaded`) enabling event-driven composition within the MVC pipeline.

- **Sales.ViewModelComposition** - Contains composition handlers and event subscribers for pricing data. Enriches the composed view model with price information during request processing.

- **Warehouse.ViewModelComposition** - Contains composition handlers and event subscribers for inventory data, adding stock levels and availability status to the composed view model.

- **Shipping.ViewModelComposition** - Contains composition handlers for shipping options data, enriching the view model with available delivery methods.

#### Web Application

- **WebApp** - The main ASP.NET Core MVC application that hosts the composition infrastructure. Uses [ServiceComposer.AspNetCore](https://www.nuget.org/packages/ServiceComposer.AspNetCore) integrated into the MVC pipeline to compose data from multiple microservices directly as MVC controllers are invoked. MVC views receive fully composed view models, abstracting away the distributed nature of the data sources.

This demo introduces **Branding as a Contract** at the UI level, demonstrating how different services can contribute to the user interface while maintaining consistent branding.

#### Utility Projects

- **ConfigurationUtils** - HTTP client configuration utilities for composition handlers.

- **JsonUtils** - JSON serialization/deserialization utilities supporting dynamic data composition.

#### Test Projects

- **Catalog.Api.Tests / Sales.Api.Tests / Shipping.Api.Tests / Warehouse.Api.Tests** - Unit and integration tests for backend APIs.

- **WebApp.Tests** - Integration tests demonstrating how to test MVC-based composition scenarios, verifying that views render correctly with composed data from multiple services.

### Key Features

- **MVC-Integrated Composition**: Composition happens seamlessly within the MVC request pipeline
- **View Model Composition**: Multiple microservices contribute to a single view model before the view is rendered
- **Event-Driven Enrichment**: Composition handlers can react to events to progressively enhance data
- **Transparent Distribution**: Views work with composed view models without knowing data comes from multiple services

### Running the Demo

To run this sample, ensure that the following projects are set as startup projects:

- `Catalog.Api`
- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `WebApp`

## ASP.Net Mvc Core UI Composition

The `ASP.Net Mvc Core UI Composition` solution represents the most advanced demo, implementing both **ViewModel Composition** and **UI Composition** concepts. This approach allows different microservices to contribute not just data, but also UI fragments (views/components) that are composed into a cohesive user interface.

### Projects

#### API Backend Services

- **Catalog.Api** - Manages product catalog information including product names, descriptions, and availability.

- **Sales.Api** - Manages product pricing information.

- **Warehouse.Api** - Manages product inventory information.

- **Shipping.Api** - Manages shipping options for products.

#### ViewModel Composition Components

- **Catalog.ViewModelComposition** - Contains composition handlers for catalog data with event publishing capabilities.

- **Catalog.ViewModelComposition.Events** - Defines composition events for catalog-related data loading.

- **Sales.ViewModelComposition** - Contains composition handlers and event subscribers for pricing data.

- **Warehouse.ViewModelComposition** - Contains composition handlers and event subscribers for inventory data.

- **Shipping.ViewModelComposition** - Contains composition handlers for shipping options data.

#### UI Composition Components (New Feature)

- **Catalog.ViewComponents** - Contains MVC View Components that render catalog-specific UI fragments. Includes:
  - `AvailableProductsViewComponent` - Renders the list of available products
  - View templates for displaying catalog information

- **Sales.ViewComponents** - Contains View Components for sales/pricing UI fragments. Includes:
  - `ProductDetailsPriceViewComponent` - Renders pricing information on product details pages
  - `AvailableProductPriceViewComponent` - Renders pricing in product lists
  - View templates for displaying pricing information

- **Warehouse.ViewComponents** - Contains View Components for inventory UI fragments. Includes:
  - `ProductDetailsInventoryViewComponent` - Renders inventory/stock information
  - View templates for displaying stock levels

- **Shipping.ViewComponents** - Contains View Components for shipping UI fragments. Includes:
  - `ProductDetailsShippingOptionsViewComponent` - Renders available shipping methods
  - View templates for displaying shipping options

#### Infrastructure

- **ITOps.UIComposition.Mvc** - Provides infrastructure for UI Composition in MVC, including:
  - `UICompositionSupportAttribute` - Marks view components with their owning namespace/service
  - `MvcBuilderExtensions` - Extension methods for configuring UI Composition support
  - Custom view engines for locating view components across different assemblies

#### Web Application

- **WebApp** - The main ASP.NET Core MVC application with full ViewModel and UI Composition support. The application:
  - Composes data from multiple microservices using ServiceComposer
  - Dynamically loads and renders View Components from different bounded contexts
  - Assembles the final UI from fragments provided by different services
  - Maintains a cohesive user experience while allowing services to own their UI pieces

#### Utility Projects

- **ConfigurationUtils** - HTTP client configuration utilities.

- **JsonUtils** - JSON serialization/deserialization utilities.

#### Test Projects

- **Catalog.Api.Tests / Sales.Api.Tests / Shipping.Api.Tests / Warehouse.Api.Tests** - Tests for backend APIs.

- **WebApp.Tests** - Integration tests for both ViewModel and UI Composition scenarios, verifying correct rendering of composed views.

### Key Features

**ViewModel Composition** (data level):
- Multiple services contribute data to compose unified view models
- Event-driven data enrichment across bounded contexts

**UI Composition** (presentation level):
- Each service owns and provides its UI fragments (View Components)
- View Components are discovered and invoked dynamically
- Services control how their data is displayed
- UI fragments are composed into cohesive pages
- True vertical slicing: each service owns both its data and presentation logic

### Architecture Benefits

- **Autonomous Teams**: Each service team controls both backend data and frontend presentation
- **Independent Deployment**: UI fragments can be updated independently with their services
- **Technology Isolation**: Different services can use different UI patterns within their components
- **Vertical Slice Ownership**: Complete feature ownership from database to UI

### Running the Demo

To run this sample, ensure that the following projects are set as startup projects:

- `Catalog.Api`
- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `WebApp`

## Note

All four solutions contain integration tests demonstrating how to test composition scenarios.

To simplify the sample as much as possible, frontend applications, such as `CompositionGateway` and `WebApp`, directly reference ViewModel Composition and UI Composition assemblies.

### Disclaimer

These demos are built using [ServiceComposer.AspNetCore](https://github.com/ServiceComposer/ServiceComposer.AspNetCore), a ViewModel Composition library I created. I work for [Particular Software](https://particular.net/), the makers of [NServiceBus](https://particular.net/nservicebus).

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

The repository root contains a [request.http](request.http) file listing some test requests. Using VS Code to execute requests can be used by adding the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) that is automatically configured when using Dev Conatainers.

## ASP.Net Core API Gateway - demos

The `ASP.Net Core API Gateway`, 01 and 02, solution demos ViewModel Composition techniques built on top of .Net Core.

### CompositionGateway

The `CompositionGateway` project shows how to create and host a .Net Core API Gateway or reverse proxy, that composes HTTP requests to multiple API backends.

To run this sample, ensure that the following projects are set as startup projects:

- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `Catalog.Api`
- `CompositionGateway`

## ASP.Net Mvc Core - Demo

The `WebApp` project is a .Net Core Mvc app that composes HTTP requests to multiple backends directly in Mvc Views as Controllers are invoked. This demo implements ViewModel Composition concepts, introducing Branding as a contract at the UI level.

To run this sample, ensure that the following projects are set as startup projects:

- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `Catalog.Api`
- `WebApp`

## ASP.Net Mvc Core UI Composition - Demo

`WebApp` project is a .Net Core Mvc app that composes HTTP requests to multiple backends directly in Mvc Views as Controllers are invoked.  This demo implements ViewModel Composition and UI Composition concepts.

To run this sample, ensure that the following projects are set as startup projects:

- `Sales.Api`
- `Shipping.Api`
- `Warehouse.Api`
- `Catalog.Api`
- `WebApp`

## Note

All four solutions contain integration tests demonstrating how to test composition scenarios.

To simplify the sample as much as possible, frontend applications, such as `CompositionGateway` and `WebApp`, directly reference ViewModel Composition and UI Composition assemblies.

### Disclaimer

This demo is built using [NServiceBus Sagas](https://docs.particular.net/nservicebus/sagas/); I work for [Particular Software](https://particular.net/), the makers of NServiceBus.

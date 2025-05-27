# THIS PACKAGE WAS DEPRICATED ON 26-05-2025 DUE TO CODESANDBOX UPDATES AND CHANGES, RENDERING IT ESSENTIALLY USELESS AT THE MOMENT



## An update for this package to the new standard will happen shortly, time permitting




# CodeSandbox.SDK.Net

[![Build .NET Framework 4.7 Library](https://github.com/E33orNaut/CodeSandbox.SDK.Net/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/E33orNaut/CodeSandbox.SDK.Net/actions/workflows/dotnet-desktop.yml)  
[![NuGet](https://img.shields.io/nuget/v/Codesandbox.SDK.Net.svg)](https://www.nuget.org/packages/Codesandbox.SDK.Net)  
![License](https://img.shields.io/github/license/e33ornaut/codesandbox.sdk.net)  
![Last Commit](https://img.shields.io/github/last-commit/e33ornaut/codesandbox.sdk.net)  
![Issues](https://img.shields.io/github/issues/e33ornaut/codesandbox.sdk.net)

---

**CodeSandbox.SDK.Net** is an **actively maintained, unofficial .NET client library and wrapper** for the [CodeSandbox](https://codesandbox.io) API.  
It provides complete, strongly-typed, async access to every endpoint in the official CodeSandbox SDK: FS, Port, Git, Setup, System, and Task APIs.  
All error handling is robust and exposes official API error models for maximum transparency and debugging.

> **Status:** Production-ready and feature complete, but not affiliated with CodeSandbox.  
> All endpoints, models, and error types are up-to-date with the latest official OpenAPI spec.

---

## Features

- ✅ **Complete API Coverage**: Every endpoint is implemented and functional.  
- ✅ **Strongly-Typed Models**: Request/response models match the official OpenAPI schema.  
- ✅ **Full Error Handling**: Catches and returns full API error objects.  
- ✅ **Extensive Logging**: Built-in logger with configurable verbosity.  
- ✅ **Async/Await Friendly**: All services support modern asynchronous patterns.  
- ✅ **Unofficial**: Independent from CodeSandbox Inc.  
- ⚙️ **Upcoming**: Experimental WebSocket support for realtime communication.  
- ⚙️ **Upcoming**: RS232 (serial) communication support for hardware integration and experimentation.

---

## Installation

```bash
dotnet add package CodeSandbox.SDK.Net
```

---

## Quick Start

```csharp
var client = new ApiClient("api-token");

// Example using ContainerService
var containerService = new ContainerService(client);
var containerRequest = new ContainerSetupRequest { TemplateId = "template-id" };
var containerResponse = await containerService.SetupContainerAsync(containerRequest);
Console.WriteLine($"Container setup result: {containerResponse.Result}");
```

Other services include `GitService`, `PortService`, `SandboxFsService`, and `SetupService`.

---

## API Coverage

| API         | Status | Notes                            |
|-------------|--------|----------------------------------|
| FS API      | ✅     | File operations, metadata, etc.  |
| Port API    | ✅     | Port introspection, messages     |
| Git API     | ✅     | Commits, branches, diffs         |
| Sandboxes   | ✅     | Fully implemented                |
| Deployments | ✅     | Fully implemented                |

---

## Logging

Customizable logging via `LoggerService`:

- Levels: `Trace`, `Info`, `Success`, `Warning`, `Error`  
- Easily integrated with your logger  
- Verbose in DEBUG builds

```csharp
var logger = new CustomLogger(LogLevel.Warning);
var client = new CodeSandboxClient("token", logger);
```

---

## Configuration

- Reuses `HttpClient` instances  
- Uses `Newtonsoft.Json` for JSON serialization

---

## Unit Testing

Supports mocking with Moq:

```csharp
var mockService = new Mock<IGitService>();
mockService.Setup(s => s.ListCommitsAsync(It.IsAny<string>()))
           .ReturnsAsync(new List<Commit>());
```

---

## Requirements

- .NET Framework 4.7+  
- .NET Core 3.1+  
- .NET 5+

---

## Roadmap

| Feature               | Status     | Notes                                         |
|-----------------------|------------|-----------------------------------------------|
| Full API Coverage     | ✅         | Complete and stable                           |
| Rich Error Reporting  | ✅         | Full API error models                         |
| Full Logging Support  | ✅         | Customizable and verbose                      |
| Extended Samples and Docs | 🚧 Planned | More comprehensive examples and guides         |
| **WebSocket Support** | 🚧 Planned | Bi-directional socket layer for realtime comms |
| **RS232 Support**     | 🚧 Planned | Serial port support for hardware integration  |

---

## Contributing

We welcome contributions! File issues or PRs.

---

## License

MIT License ©

---

## Acknowledgments

Thanks to CodeSandbox for the original TypeScript SDK inspiration.

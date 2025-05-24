
# CodeSandbox.SDK.Net

[![Build .NET Framework 4.7 Library](https://github.com/E33orNaut/CodeSandbox.SDK.Net/actions/workflows/dotnet-desktop.yml/badge.svg?event=status)](https://github.com/E33orNaut/CodeSandbox.SDK.Net/actions/workflows/dotnet-desktop.yml)
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
- ✅ **Actively Maintained**: Open to feedback, bugs, and contributions.
- ✅ **Unofficial**: Independent from CodeSandbox Inc.

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
- Uses `Newtonsoft.Json`, `System.Text.Json` support planned
- Retry logic is on roadmap

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

- ✅ Full API Coverage  
- ✅ Rich Error Reporting  
- ✅ Full Logging Support  
- 🚧 Retry Policies  
- 🚧 System.Text.Json Support  
- 🚧 Extended Samples and Docs  

---

## Contributing

We welcome contributions! File issues or PRs.

---

## License

MIT License ©

---

## Acknowledgments

Thanks to CodeSandbox for the original TypeScript SDK inspiration.

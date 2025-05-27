# CodeSandbox.SDK.Net

**CodeSandbox.SDK.Net** is a .NET Standard / .NET Framework-compatible client library for interacting with the [CodeSandbox](https://codesandbox.io) API from C# and .NET-based applications. It provides a strongly-typed, developer-friendly interface to CodeSandbox's FS, Port, and Git APIs with full IntelliSense support and extensible configuration.

✅ Compatible with **.NET Framework 4.7+**, **.NET Core**, and **.NET 5+**

---

## Features

- ✅ Access CodeSandbox **File System (FS) API**
- ✅ Use **Git API** to manage sandbox versioning
- ✅ Interact with **Port API** for live container communication
- ✅ Async-friendly and testable services
- ✅ Built-in logging (`Trace`, `Info`, `Success`, `Warning`, `Error`) with `DEBUG` support
- ✅ Full support for CodeSandbox **OpenAPI spec**
- ✅ Moq-friendly for unit testing

---

## Installation

```bash
dotnet add package CodeSandbox.SDK.Net
```

---

## Quick Start

```csharp
using CodeSandbox.SDK;
using CodeSandbox.SDK.Services;

var client = new CodeSandboxClient("your-api-token");

// FS API Example
var files = await client.FS.ListFilesAsync("sandbox-id");

// Git API Example
var commits = await client.Git.ListCommitsAsync("sandbox-id");

// Port API Example
var result = await client.Port.SendMessageAsync("sandbox-id", 3000, "ping");
```

---

## API Coverage

| API        | Status | Notes                                        |
|------------|--------|----------------------------------------------|
| FS API     | ✅     | Read/write, directory, metadata, delete     |
| Port API   | ✅     | Send/receive messages, port introspection   |
| Git API    | ✅     | Commits, branches, diffs, file states       |
| Sandboxes  | 🚧     | Planned                                      |
| Deployments| 🚧     | Planned                                      |

---

## Logging

The SDK includes a configurable `LoggerService` with 5 log levels:

- `Trace`
- `Info`
- `Success`
- `Warning`
- `Error`

In `DEBUG` builds, verbose logging is enabled by default. You can inject your own logger implementation if needed.

```csharp
var logger = new CustomLogger(minimumLevel: LogLevel.Warning);
var client = new CodeSandboxClient("your-api-token", logger);
```

---

## Configuration Options

- Reuses `HttpClient` instances for better performance
- Supports `Newtonsoft.Json` (with `System.Text.Json` support planned)
- Parses and exposes detailed API errors in exceptions
- Retry logic for transient errors is on the roadmap

---

## Unit Testing

All services are testable via interfaces. For mocking:

```csharp
var mockGitService = new Mock<IGitService>();
mockGitService.Setup(s => s.ListCommitsAsync(It.IsAny<string>()))
              .ReturnsAsync(new List<Commit>());
```

> **Note**: Avoid using `It.IsAnyType` as a generic argument in Moq setups. Use concrete types instead.

---

## Requirements

- .NET Framework 4.7+
- .NET Core 3.1+
- .NET 5+

---

## Roadmap

- ✅ FS API  
- ✅ Port API  
- ✅ Git API  
- 🚧 Sandbox & Deployment API support  
- 🚧 System.Text.Json support  
- 🚧 Retry policies for transient errors  
- 🚧 NuGet package documentation and samples  

---
---

# 🙏 **Special Thanks**

---

### Christian Alfoni  
**CodeSandbox Development Team**

### Ian Hutchins  
**FOSS Contributor** _(Unit testing improvements)_

---


## Contributing

Contributions are welcome! Please open an issue or pull request.

---

## License

MIT License © 

---

## Acknowledgments

This SDK is inspired by the official CodeSandbox SDK (TypeScript) and built for .NET developers who want first-class integration with CodeSandbox from C# applications.

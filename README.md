# CodeSandbox.SDK.Net

[![Build .NET Framework 4.7 Library](https://github.com/E33orNaut/CodeSandbox.SDK.Net/actions/workflows/dotnet-desktop.yml/badge.svg?event=status)](https://github.com/E33orNaut/CodeSandbox.SDK.Net/actions/workflows/dotnet-desktop.yml)
[![NuGet](https://img.shields.io/nuget/v/Codesandbox.SDK.Net.svg)](https://www.nuget.org/packages/Codesandbox.SDK.Net)
![License](https://img.shields.io/github/license/e33ornaut/codesandbox.sdk.net)
![Last Commit](https://img.shields.io/github/last-commit/e33ornaut/codesandbox.sdk.net)
![Issues](https://img.shields.io/github/issues/e33ornaut/codesandbox.sdk.net)

---

**CodeSandbox.SDK.Net** is a .NET Framework 4.7+ and .NET Core/5+ compatible client library for interacting with the [CodeSandbox](https://codesandbox.io) API from C# and .NET-based applications. It provides a strongly-typed, developer-friendly interface to CodeSandbox's FS, Port, Git, and Setup APIs with full IntelliSense support and extensible configuration.

> **Status:** HIGHLY EXPERIMENTAL. Use at your own risk. The SDK is under active development and not all features are complete. Contributions and feedback are welcome!

---

## Features

- ✅ Full support for CodeSandbox **OpenAPI spec** (FS, Port, Git, Setup, System, Task, Container)
- ✅ Strongly-typed models for all endpoints, matching the latest OpenAPI schemas
- ✅ Async-friendly, testable, and interface-driven services
- ✅ Built-in logging (`Trace`, `Info`, `Success`, `Warning`, `Error`) with `DEBUG` support
- ✅ Moq-friendly for unit testing
- ✅ .NET Framework 4.7+, .NET Core 3.1+, .NET 5+ support

---

## Installation
dotnet add package CodeSandbox.SDK.Net
---

## Quick Start

'''csharp

var client = new ApiClient("api-token");

// ContainerService example
var containerService = new ContainerService(client);
var containerRequest = new ContainerSetupRequest { TemplateId = "template-id" };
var containerResponse = await containerService.SetupContainerAsync(containerRequest);
Console.WriteLine($"Container setup result: {containerResponse.Result}");

// GitService examples
var gitService = new GitService(client);
await gitService.PostCommitAsync("Initial commit");

var gitStatus = await gitService.GetStatusAsync();
Console.WriteLine($"Git status: {gitStatus.Status}");

var gitRemotes = await gitService.GetRemotesAsync();
Console.WriteLine($"Git remotes: {gitRemotes.Status}");

var gitDiff = await gitService.GetTargetDiffAsync("main");
Console.WriteLine($"Git diff: {gitDiff.Status}");

await gitService.PostPullAsync("main");
await gitService.PostDiscardAsync(new[] { "file.txt" });
await gitService.PostRemoteAddAsync("https://github.com/user/repo.git");

// PortService example
var portService = new PortService(client);
var portList = await portService.GetPortListAsync();
foreach (var port in portList.Result.List)
    Console.WriteLine($"Port: {port.PortNumber}, Url: {port.Url}");

// SandboxFsService examples
var fsService = new SandboxFsService(client);
await fsService.WriteFileAsync(new WriteFileRequest { Path = "file.txt", Content = "Hello World" });
await fsService.FsReadFileAsync(new FSReadFileParams { Path = "file.txt" });
await fsService.FsUploadAsync(new UploadRequest { ParentId = "root", Content = "data" });
await fsService.FsDownloadAsync(new DownloadRequest { Path = "file.txt" });
await fsService.FsPathSearchAsync(new PathSearchParams());
await fsService.StatAsync(new FSStatParams { Path = "file.txt" });
await fsService.CopyAsync(new FSCopyParams { SourcePath = "file.txt" });
await fsService.RenameAsync(new FSRenameParams { /* fill as needed */ });
await fsService.RemoveAsync(new FSRemoveParams { Path = "file.txt" });
await fsService.ReadDirAsync(new FSReadDirParams { Path = "." });

// SetupService examples
var setupService = new SetupService(client);
await setupService.InitializeSetupAsync();
await setupService.GetSetupProgressAsync();
await setupService.SkipStepAsync(0);
await setupService.SkipAllStepsAsync();
await setupService.EnableSetupAsync();
await setupService.DisableSetupAsync();
await setupService.SetStepAsync(0);
---
```

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
var logger = new CustomLogger(minimumLevel: LogLevel.Warning);
var client = new CodeSandboxClient("your-api-token", logger);
---

## Configuration Options

- Reuses `HttpClient` instances for better performance
- Supports `Newtonsoft.Json` (with `System.Text.Json` support planned)
- Parses and exposes detailed API errors in exceptions
- Retry logic for transient errors is on the roadmap

---

## Unit Testing

All services are testable via interfaces. For mocking:
var mockGitService = new Mock<IGitService>();
mockGitService.Setup(s => s.ListCommitsAsync(It.IsAny<string>()))
              .ReturnsAsync(new List<Commit>());
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

## Contributing

Contributions are welcome! Please open an issue or pull request.

---

## License

MIT License © 

---

## Acknowledgments

This SDK is inspired by the official CodeSandbox SDK (TypeScript) and built for .NET developers who want first-class integration

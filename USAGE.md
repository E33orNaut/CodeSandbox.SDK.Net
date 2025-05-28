# Usage

## Initialization
var logger = new LoggerService(LogLevel.Info);
var apiClient = new ApiClient("https://api.sandbox.codesandbox.io", "api-token", logger);
## System Service
var systemService = new SystemService(new HttpClient(), logger);
var updateResult = await systemService.UpdateSystemAsync();
var hibernateResult = await systemService.HibernateSystemAsync();
var metrics = await systemService.GetSystemMetricsAsync();
## Task Service
var taskService = new TaskService(new HttpClient(), logger);
var taskList = await taskService.GetTaskListAsync();
var runTask = await taskService.RunTaskAsync("task-id");
var runCommand = await taskService.RunCommandAsync("task-id", "echo Hello");
var stopTask = await taskService.StopTaskAsync("task-id");
var createTask = await taskService.CreateTaskAsync("task-id");
var updateTask = await taskService.UpdateTaskAsync("task-id");
var saveToConfig = await taskService.SaveToConfigAsync("task-id");
## File System Service
var fsService = new SandboxFsService(apiClient, logger);
// Write a file
await fsService.WriteFileAsync(new SandboxFSWriteFileRequest { /* ... */ });
// Read a file
await fsService.FsReadFileAsync(new FSReadFileParams { /* ... */ });
// Upload a file
await fsService.FsUploadAsync(new UploadRequest { /* ... */ });
// Download a file
await fsService.FsDownloadAsync(new DownloadRequest { /* ... */ });
## Container Service
var containerService = new ContainerService(apiClient, logger);
await containerService.SetupContainerAsync(new ContainerSetupRequest { /* ... */ });
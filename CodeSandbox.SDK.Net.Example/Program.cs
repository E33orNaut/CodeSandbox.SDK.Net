using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxSystemModels;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;
using CodeSandbox.SDK.Net.Models.New.SandboxSetupModels;
using CodeSandbox.SDK.Net.Services;

namespace CodeSandbox.SDK.Net.Example
{
    internal class Program
    {
        static async Task Main()
        {
            // Setup
            var logger = new LoggerService(LogLevel.Info);
            var apiClient = new ApiClient("https://api.sandbox.codesandbox.io", "api-token", logger);

            // SystemService examples
            var systemService = new SystemService(apiClient, logger);

            try
            {
                // Update system
                var updateResult = await systemService.UpdateSystemAsync();
                Console.WriteLine($"UpdateSystemAsync: Status={updateResult.Status}");

                // Hibernate system
                var hibernateResult = await systemService.HibernateSystemAsync();
                Console.WriteLine($"HibernateSystemAsync: Status={hibernateResult.Status}");

                // Get system metrics
                var metrics = await systemService.GetSystemMetricsAsync();
                Console.WriteLine($"GetSystemMetricsAsync: CPU Used={metrics.Cpu.Used}, Memory Used={metrics.Memory.Used}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"SystemService API error: {ex.Message}");
            }

            // TaskService examples
            var taskService = new TaskService(new System.Net.Http.HttpClient(), logger);

            try
            {
                // Get task list
                var taskList = await taskService.GetTaskListAsync();
                Console.WriteLine($"GetTaskListAsync: Task count={taskList.Result.Tasks.Count}");

                // Run a task
                var runTask = await taskService.RunTaskAsync("task-id");
                Console.WriteLine($"RunTaskAsync: TaskId={runTask.Result.Id}");

                // Run a command
                var runCommand = await taskService.RunCommandAsync("task-id", "echo Hello");
                Console.WriteLine($"RunCommandAsync: Output={runCommand.Result.Shell?.Output}");

                // Stop a task
                var stopTask = await taskService.StopTaskAsync("task-id");
                Console.WriteLine($"StopTaskAsync: TaskId={stopTask.Result.Id}");

                // Create a task
                var createTask = await taskService.CreateTaskAsync("task-id");
                Console.WriteLine($"CreateTaskAsync: TaskId={createTask.Result.Id}");

                // Update a task
                var updateTask = await taskService.UpdateTaskAsync("task-id");
                Console.WriteLine($"UpdateTaskAsync: TaskId={updateTask.Result.Id}");

                // Save to config
                var saveToConfig = await taskService.SaveToConfigAsync("task-id");
                Console.WriteLine($"SaveToConfigAsync: TaskId={saveToConfig.Result.Id}");

                // Generate config
                var generateConfig = await taskService.GenerateConfigAsync("task-id");
                Console.WriteLine("GenerateConfigAsync: Completed");

                // Create setup tasks
                var setupTasksRequest = new SandboxTaskCreateSetupTasksRequest
                {
                    Tasks = new List<SandboxTaskDefinitionDTO>
                    {
                        new SandboxTaskDefinitionDTO
                        {
                            Name = "Setup Task",
                            Command = "echo Setup",
                            RunAtStart = true
                        }
                    }
                };
                var createSetupTasks = await taskService.CreateSetupTasksAsync(setupTasksRequest);
                Console.WriteLine("CreateSetupTasksAsync: Completed");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"TaskService API error: {ex.Message}");
            }

            // SetupService examples
            var setupService = new SetupService(apiClient, logger);

            try
            {
                // Get setup progress
                var setupProgress = await setupService.GetSetupProgressAsync();
                Console.WriteLine($"GetSetupProgressAsync: State={setupProgress.Result.State}");

                // Skip a step
                var skipStep = await setupService.SkipStepAsync(0);
                Console.WriteLine($"SkipStepAsync: CurrentStepIndex={skipStep.Result.CurrentStepIndex}");

                // Skip all steps
                var skipAll = await setupService.SkipAllStepsAsync();
                Console.WriteLine($"SkipAllStepsAsync: State={skipAll.Result.State}");

                // Disable setup
                var disableSetup = await setupService.DisableSetupAsync();
                Console.WriteLine($"DisableSetupAsync: State={disableSetup.Result.State}");

                // Enable setup
                var enableSetup = await setupService.EnableSetupAsync();
                Console.WriteLine($"EnableSetupAsync: State={enableSetup.Result.State}");

                // Initialize setup
                var initSetup = await setupService.InitializeSetupAsync();
                Console.WriteLine($"InitializeSetupAsync: State={initSetup.Result.State}");

                // Set step
                var setStep = await setupService.SetStepAsync(1);
                Console.WriteLine($"SetStepAsync: CurrentStepIndex={setStep.Result.CurrentStepIndex}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"SetupService API error: {ex.Message}");
            }

            // GitStatusService examples
            var gitStatusService = new GitService(apiClient, logger);
    
            try
            {
                // Get git status
                var gitStatus = await gitStatusService.GetStatusAsync();
                Console.WriteLine("GetStatusAsync: Success");
                 

                // Get remotes
                var remotes = await gitStatusService.GetRemotesAsync();
                Console.WriteLine("GetRemotesAsync: Success");
  
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"GitStatusService API error: {ex.Message}");
            }
        }
    }
}

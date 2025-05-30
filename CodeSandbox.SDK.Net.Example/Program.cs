using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;
using CodeSandbox.SDK.Net.Models.New.SandboxSetupModels;
using CodeSandbox.SDK.Net.Models.New.SandboxSystemModels;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;
using CodeSandbox.SDK.Net.Services;

namespace CodeSandbox.SDK.Net.Example
{
    internal class Program
    {
 


        private static async Task Main(string[] args)
        { 
            // Setup
            LoggerService logger = new LoggerService(LogLevel.Info);
            ApiClient apiClient = new ApiClient("https://api.sandbox.codesandbox.io", "api-token", logger);

            // SystemService examples
            SystemService systemService = new SystemService(apiClient, logger);

            try
            {
                // Update system
                SandboxSystemSuccessResponse updateResult = await systemService.UpdateSystemAsync();
                Console.WriteLine($"UpdateSystemAsync: Status={updateResult.Status}");

                // Hibernate system
                SandboxSystemSuccessResponse hibernateResult = await systemService.HibernateSystemAsync();
                Console.WriteLine($"HibernateSystemAsync: Status={hibernateResult.Status}");

                // Get system metrics
                SandboxSystemMetricsStatus metrics = await systemService.GetSystemMetricsAsync();
                Console.WriteLine($"GetSystemMetricsAsync: CPU Used={metrics.Cpu.Used}, Memory Used={metrics.Memory.Used}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"SystemService API error: {ex.Message}");
            }

            // TaskService examples
            TaskService taskService = new TaskService(apiClient, logger);

            try
            {
                // Get task list
                SandboxTaskSuccessResponse<SandboxTaskListResult> taskList = await taskService.GetTaskListAsync();
                Console.WriteLine($"GetTaskListAsync: Task count={taskList.Result.Tasks.Count}");

                // Run a task
                SandboxTaskSuccessResponse<SandboxTaskResult> runTask = await taskService.RunTaskAsync("task-id");
                Console.WriteLine($"RunTaskAsync: TaskId={runTask.Result.Id}");

                // Run a command
                SandboxTaskSuccessResponse<SandboxTaskResult> runCommand = await taskService.RunCommandAsync("task-id", "echo Hello");
                Console.WriteLine($"RunCommandAsync: Output={runCommand.Result.Shell?.Output}");

                // Stop a task
                SandboxTaskSuccessResponse<SandboxTaskResult> stopTask = await taskService.StopTaskAsync("task-id");
                Console.WriteLine($"StopTaskAsync: TaskId={stopTask.Result.Id}");

                // Create a task
                SandboxTaskSuccessResponse<SandboxTaskResult> createTask = await taskService.CreateTaskAsync("task-id");
                Console.WriteLine($"CreateTaskAsync: TaskId={createTask.Result.Id}");

                // Update a task
                SandboxTaskSuccessResponse<SandboxTaskResult> updateTask = await taskService.UpdateTaskAsync("task-id");
                Console.WriteLine($"UpdateTaskAsync: TaskId={updateTask.Result.Id}");

                // Save to config
                SandboxTaskSuccessResponse<SandboxTaskResult> saveToConfig = await taskService.SaveToConfigAsync("task-id");
                Console.WriteLine($"SaveToConfigAsync: TaskId={saveToConfig.Result.Id}");

                // Generate config
                SandboxTaskSuccessResponse<SandboxTaskResult> generateConfig = await taskService.GenerateConfigAsync("task-id");
                Console.WriteLine("GenerateConfigAsync: Completed");

                // Create setup tasks
                SandboxTaskCreateSetupTasksRequest setupTasksRequest = new SandboxTaskCreateSetupTasksRequest
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
                SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult> createSetupTasks = await taskService.CreateSetupTasksAsync(setupTasksRequest);
                Console.WriteLine("CreateSetupTasksAsync: Completed");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"TaskService API error: {ex.Message}");
            }

            // SetupService examples
            SetupService setupService = new SetupService(apiClient, logger);

            try
            {
                // Get setup progress
                SandboxSetupSuccessResponse setupProgress = await setupService.GetSetupProgressAsync();
                Console.WriteLine($"GetSetupProgressAsync: State={setupProgress.Result.State}");

                // Skip a step
                SandboxSetupSuccessResponse skipStep = await setupService.SkipStepAsync(0);
                Console.WriteLine($"SkipStepAsync: CurrentStepIndex={skipStep.Result.CurrentStepIndex}");

                // Skip all steps
                SandboxSetupSuccessResponse skipAll = await setupService.SkipAllStepsAsync();
                Console.WriteLine($"SkipAllStepsAsync: State={skipAll.Result.State}");

                // Disable setup
                SandboxSetupSuccessResponse disableSetup = await setupService.DisableSetupAsync();
                Console.WriteLine($"DisableSetupAsync: State={disableSetup.Result.State}");

                // Enable setup
                SandboxSetupSuccessResponse enableSetup = await setupService.EnableSetupAsync();
                Console.WriteLine($"EnableSetupAsync: State={enableSetup.Result.State}");

                // Initialize setup
                SandboxSetupSuccessResponse initSetup = await setupService.InitializeSetupAsync();
                Console.WriteLine($"InitializeSetupAsync: State={initSetup.Result.State}");

                // Set step
                SandboxSetupSuccessResponse setStep = await setupService.SetStepAsync(1);
                Console.WriteLine($"SetStepAsync: CurrentStepIndex={setStep.Result.CurrentStepIndex}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"SetupService API error: {ex.Message}");
            }

            // GitStatusService examples
            GitService gitStatusService = new GitService(apiClient, logger);

            try
            {
                // Get git status
                Models.New.GitModels.GitStatusResponse gitStatus = await gitStatusService.GetStatusAsync();
                Console.WriteLine("GetStatusAsync: Success");


                // Get remotes
                Models.New.GitModels.GitRemotesResponse remotes = await gitStatusService.GetRemotesAsync();
                Console.WriteLine("GetRemotesAsync: Success");

            }
            catch (ApiException ex)
            {
                Console.WriteLine($"GitStatusService API error: {ex.Message}");
            }
        }
    }
}

using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

public class WebSocketServer : IDisposable
{
    private IDisposable _webApp;
    private readonly int _port;

    // Publicly accessible API key
    public string ApiKey { get; private set; } = string.Empty;

    public WebSocketServer(int port = 5000)
    {
        _port = port;
        IsPaused = false;
        IsDisabled = false;
    }

    // Start accepts API key
    public void Start(string apiKey, int port = 5000)
    {
        if (IsDisabled)
        {
            throw new InvalidOperationException("Server is disabled.");
        }

        if (_webApp != null)
        {
            return; // already started
        }

        ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

        string url = $"http://localhost:{_port}/";
        _webApp = WebApp.Start(url, app =>
        {
            _ = app.MapSignalR("/codesandbox", new HubConfiguration()
            {
                // Hub configuration here
            });
        });

        IsPaused = false;
        Console.WriteLine($"SignalR WebSocket server started on {url} with API key: {ApiKey}");
    }

    public void Stop()
    {
        if (_webApp == null)
        {
            return; // already stopped
        }

        _webApp.Dispose();
        _webApp = null;
        Console.WriteLine("SignalR WebSocket server stopped");
    }

    public void Pause()
    {
        IsPaused = true;
        Console.WriteLine("SignalR server paused");
    }

    public void Resume()
    {
        IsPaused = false;
        Console.WriteLine("SignalR server resumed");
    }

    public void Disable()
    {
        IsDisabled = true;
        Console.WriteLine("SignalR server disabled");
    }

    public void Dispose()
    {
        Stop();
    }

    public bool IsRunning => _webApp != null && !IsPaused && !IsDisabled;
    public bool IsPaused { get; private set; }
    public bool IsDisabled { get; private set; }
}

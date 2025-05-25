using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

public class WebSocketServer : IDisposable
{
    private IDisposable _webApp;
    private bool _isPaused;
    private bool _isDisabled;
    private readonly int _port;

    // Publicly accessible API key
    public string ApiKey { get; private set; } = string.Empty;

    public WebSocketServer(int port = 5000)
    {
        _port = port;
        _isPaused = false;
        _isDisabled = false;
    }

    // Start accepts API key
    public void Start(string apiKey, int port = 5000)
    {
        if (_isDisabled) throw new InvalidOperationException("Server is disabled.");
        if (_webApp != null) return; // already started

        ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

        string url = $"http://localhost:{_port}/";
        _webApp = WebApp.Start(url, app =>
        {
            app.MapSignalR("/codesandbox", new HubConfiguration()
            {
                // Hub configuration here
            });
        });

        _isPaused = false;
        Console.WriteLine($"SignalR WebSocket server started on {url} with API key: {ApiKey}");
    }

    public void Stop()
    {
        if (_webApp == null) return; // already stopped

        _webApp.Dispose();
        _webApp = null;
        Console.WriteLine("SignalR WebSocket server stopped");
    }

    public void Pause()
    {
        _isPaused = true;
        Console.WriteLine("SignalR server paused");
    }

    public void Resume()
    {
        _isPaused = false;
        Console.WriteLine("SignalR server resumed");
    }

    public void Disable()
    {
        _isDisabled = true;
        Console.WriteLine("SignalR server disabled");
    }

    public void Dispose()
    {
        Stop();
    }

    public bool IsRunning => _webApp != null && !_isPaused && !_isDisabled;
    public bool IsPaused => _isPaused;
    public bool IsDisabled => _isDisabled;
}

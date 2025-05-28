using System;

namespace CodeSandbox.SDK.Net.Sockets
{
    public static class ServerContext
    {
        private static string _apiKey;

        public static string ApiKey
        {
            get => string.IsNullOrEmpty(_apiKey) ? throw new InvalidOperationException("API Key has not been initialized.") : _apiKey;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("API Key cannot be null or empty.");
                }

                _apiKey = value;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSandbox.SDK.Net.Sockets
{
    public static class ServerContext
    {
        private static string _apiKey;

        public static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_apiKey))
                    throw new InvalidOperationException("API Key has not been initialized.");
                return _apiKey;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("API Key cannot be null or empty.");
                _apiKey = value;
            }
        }
    }

}

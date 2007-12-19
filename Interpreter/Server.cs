using System;
using System.Net;

namespace Eft.Interpreter
{
    public class Server
    {
        private readonly string host;
        private readonly int port;
        private readonly HttpListener listener = new HttpListener();

        public Server(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Start()
        {
            listener.Prefixes.Add(new UriBuilder("http", host, port).ToString());
            listener.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}
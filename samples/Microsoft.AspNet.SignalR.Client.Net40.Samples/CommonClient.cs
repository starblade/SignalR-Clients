using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace Microsoft.AspNet.SignalR.Client.Samples
{
    public class CommonClient
    {
        private TextWriter _traceWriter;

        public CommonClient(TextWriter traceWriter)
        {
            _traceWriter = traceWriter;
        }

        public void Run()
        {
            RunHubConnectionAPI();
        }

        private void RunHubConnectionAPI()
        {
            // Url can't be localhost because Windows Phone emulator runs in a separate virtual machine. Therefore, server is located
            // in another machine
            string url = "http://signalr01.cloudapp.net/";

            var hubConnection = new HubConnection(url);
            hubConnection.TraceWriter = _traceWriter;

            var hubProxy = hubConnection.CreateHubProxy("HubConnectionAPI");
            hubProxy.On<string>("displayMessage", (data) => hubConnection.TraceWriter.WriteLine(data));

            hubConnection.Start().Wait();
            hubConnection.TraceWriter.WriteLine("transport.Name={0}", hubConnection.Transport.Name);

            hubProxy.Invoke("DisplayMessageCaller", "Hello Caller!").Wait();

            string joinGroupResponse = hubProxy.Invoke<string>("JoinGroup", hubConnection.ConnectionId, "CommonClientGroup").Result;
            hubConnection.TraceWriter.WriteLine("joinGroupResponse={0}", joinGroupResponse);
            
            hubProxy.Invoke("DisplayMessageGroup", "CommonClientGroup", "Hello Group Members!").Wait();
        }

        private void RunRawConnection()
        {
            string url = "http://signalr01.cloudapp.net/raw-connection";

            var connection = new Connection(url);
            connection.TraceWriter = _traceWriter;

            connection.Start().Wait();
            connection.TraceWriter.WriteLine("transport.Name={0}", connection.Transport.Name);

            connection.Send(new { type = 1, value = "first message" }).Wait();
            connection.Send(new { type = 1, value = "second message" }).Wait();
        }


        private void RunStreaming()
        {
            string url = "http://signalr01.cloudapp.net/streaming-connection";

            var connection = new Connection(url);
            connection.TraceWriter = _traceWriter;

            connection.Start().Wait();
            connection.TraceWriter.WriteLine("transport.Name={0}", connection.Transport.Name);
        }
    }    
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client.Samples;

namespace Microsoft.AspNet.SignalR.Client.Net40.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var writer = Console.Out;
            var client = new CommonClient(writer);
            client.Run();

            Console.ReadLine();
        }
    }
}

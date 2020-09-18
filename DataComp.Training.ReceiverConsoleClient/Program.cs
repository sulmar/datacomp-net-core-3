using DataComp.Training.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DataComp.Training.ReceiverConsoleClient
{
    // dotnet add package Microsoft.AspNetCore.SignalR.Client
    class Program
    {
        private const string url = "http://localhost:5000/signalr/users";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Received!");

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");
            await connection.StartAsync();

            Console.WriteLine("Connected.");

            connection.On<User>("AddedUser", user => Console.WriteLine($"Received {user.FullName}"));

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

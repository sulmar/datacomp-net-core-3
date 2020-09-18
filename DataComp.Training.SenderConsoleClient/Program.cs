using DataComp.Training.Fakers;
using DataComp.Training.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataComp.Training.SenderConsoleClient
{

    // dotnet add package Microsoft.AspNetCore.SignalR.Client

    class Program
    {
        private const string url = "http://localhost:5000/signalr/users";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Sender!");

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            connection.On<string>("Pong", message => Console.WriteLine(message));

            Console.WriteLine("Connecting...");
          
            await connection.StartAsync();

            Console.WriteLine("Connected.");

            await connection.SendAsync("Ping", "Hello!");

            UserFaker userFaker = new UserFaker();

            // IEnumerable<User> users = userFaker.GenerateLazy(100);

            IEnumerable<User> users = userFaker.GenerateForever();

            // User user = userFaker.Generate();

            foreach (var user in users)
            {
                Console.WriteLine($"Send {user.FullName}");

                await connection.SendAsync("SendAddedUser", user);

                await Task.Delay(TimeSpan.FromSeconds(0.1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Threading;

namespace SqlApi.Task
{
    public class WhatsappService : Controller, IHostedService, IDisposable
    {
        private readonly ILogger<WhatsappService> logger;
       
        public WhatsappService(ILogger<WhatsappService> logger)
        {
            this.logger = logger;
        }
        public System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {


                var client = new RestClient("https://api.ultramsg.com/instance29491/chats/messages?token=f5q4cw0yvxymqamo&chatId=905542530828@c.us&limit=1");
                var request = new RestRequest().AddHeader("content-type", "application/x-www-form-urlencoded");
                var response = client.Execute(request);
                Console.WriteLine(response);

            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Worker stopping");
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}

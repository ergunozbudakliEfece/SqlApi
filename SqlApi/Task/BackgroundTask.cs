
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Nancy.Json;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlApi.Task
{
    public class BackgroundTask : Controller, IHostedService,IDisposable
    {   private readonly ILogger<BackgroundTask> logger;
        private int number=0;
        private Timer timer;
        public BackgroundTask(ILogger<BackgroundTask> logger)
        {
            this.logger = logger;
        }
       



        public void Dispose()
        {
            timer?.Dispose();
        }
        

        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {
            
            timer = new Timer(o => {
                var x=GetOnlineUsers();
                foreach(var item in x)
                {
                    if (DateTime.Parse(item.LOG_DATETIME).AddHours(12) < DateTime.Now)
                    {
                        LogInTBL lognew = new LogInTBL();
                        lognew.USER_NAME = item.USER_NAME;
                        lognew.USER_ID = (int)item.USER_ID;
                        lognew.LOG_DATETIME = DateTime.Parse(item.LOG_DATETIME).AddMinutes(30);
                        lognew.ACTIVITY_TYPE = "logout";

                        lognew.PLATFORM = item.PLATFORM;
                       

                        string apiUrlnew = "http://192.168.2.13:83/api/Log";

                        var httpClientnew = new HttpClient();
                        var requestnew = new HttpRequestMessage(HttpMethod.Post, apiUrlnew)
                        {
                            Content = new StringContent(new JavaScriptSerializer().Serialize(lognew), Encoding.UTF8, "application/json")
                        };

                        var responsenew = httpClientnew.SendAsync(requestnew);
                    }
                }

            }, null, System.TimeSpan.Zero, System.TimeSpan.FromMinutes(30));
            return System.Threading.Tasks.Task.CompletedTask;
        }

       

        public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Worker stopping");
            return System.Threading.Tasks.Task.CompletedTask;
        }
        public async Task<ActionResult> LogKayıt(Log log)
        {
            
            Log log1 = new Log();
            log1.USER_NAME = log.USER_NAME;
            log1.USER_ID = log.USER_ID;
            log1.ACTIVITY_START = log.ACTIVITY_START;
            log1.ACTIVITY_END = (DateTime.Parse(log.ACTIVITY_START).AddMinutes(20)).ToString();
            log1.MODULE_ID = log.PROGRAM_ID;
            log1.PROGRAM_ID = log.PROGRAM_ID;
            log1.ACTIVITY_TYPE = log.ACTIVITY_TYPE;

           
            
            var apiUrl = "http://192.168.2.13:83/api/userlog/"+log.ACTIVITY_START;


            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, apiUrl)
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(log1), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);

            

            return null;

        }
        public List<LogIn> GetOnlineUsers()
        {


            var apiUrl = "http://192.168.2.13:83/api/log/login";
            //Connect API
            Uri url = new Uri(apiUrl);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            string json = client.DownloadString(url);
            //END

            //JSON Parse START
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LogIn> jsonList = ser.Deserialize<List<LogIn>>(json);

            //END

            return jsonList;
        }


    }

    







}


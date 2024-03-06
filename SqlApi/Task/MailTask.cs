﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlApi.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Globalization;
using static SqlApi.Controllers.SatisController;
using SqlApi.Services;
using System.Threading.Tasks;
using System.Text;
using SqlApi.Controllers;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;

namespace SqlApi.Task
{
    public class MailTask : Controller, IHostedService, IDisposable
    {
        private readonly ILogger<MailTask> logger;
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        IMemoryCache _memoryCache;
        public MailTask(ILogger<MailTask> logger, UserContext context, IConfiguration configuration,  IMemoryCache memoryCache)
        {
            this.logger = logger;
            _context = context;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
        bool test = true;
        int day = 0;
        int day1 = 0;
        int day2 = 0;
        public System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                var currentTime = DateTime.UtcNow.ToLocalTime();
                if (day != currentTime.Minute)
                {
                    day = 0;

                }
                if (day1 != currentTime.Second)
                {
                    day1 = 0;
                }
                if (day2 != currentTime.Second)
                {
                    day2 = 0;
                }

                if (currentTime.DayOfWeek == DayOfWeek.Saturday && currentTime.Hour == 15 && currentTime.Minute == 0 && currentTime.Second == 0 && day1==0)
                {
                    day1 = currentTime.Second;


                        DoWork(currentTime);





                }
                else if (currentTime.Hour == 8 && currentTime.Minute == 30 && currentTime.Second == 0 && currentTime.Millisecond==0 && currentTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    MailMessage mail = new MailMessage();
                    try
                    {
                        string query2 = @" EXEC SP_SEND_MAIL 2";
                        List<MailModel> exec2 = null;
                        string sqldataSource2 = _configuration.GetConnectionString("Connn");
                        SqlDataReader sqlreader2;
                        using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                        {
                            mycon2.Open();
                            using (SqlCommand myCommand1 = new SqlCommand(query2, mycon2))
                            {
                                sqlreader2 = myCommand1.ExecuteReader();
                                exec2 = DataReaderMapToList<MailModel>(sqlreader2);
                                sqlreader2.Close();
                                mycon2.Close();
                            }
                        }


                        string query1 = @"EXEC RAPOR_TEST";
                        var body = "";
                        List<RaporTest> exec = null;
                        string sqldataSource1 = _configuration.GetConnectionString("Connn");
                        SqlDataReader sqlreader1;
                        using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                        {
                            mycon1.Open();
                            using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                            {
                                sqlreader1 = myCommand1.ExecuteReader();
                                exec = DataReaderMapToList<RaporTest>(sqlreader1);
                                sqlreader1.Close();
                                mycon1.Close();
                            }
                        }

                        for (var i = 0; i < exec.Count; i++)
                        {
                            var col = "white";
                            var col2 = "lightgreen";
                            if (exec[i].GIRIS != "-")
                            {
                                if (Int32.Parse(exec[i].GIRIS.Replace(":", "")) > 800)
                                {
                                    col = "#FFCCCB";
                                }
                                else
                                {
                                    col = "lightgreen";
                                }
                            }

                            if (exec[i].CIKIS != "-")
                            {
                                if (currentTime.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    if (Int32.Parse(exec[i].CIKIS.Replace(":", "")) < 1500)
                                    {
                                        col2 = "#FFCCCB";
                                    }
                                }
                                else
                                {
                                    if (Int32.Parse(exec[i].CIKIS.Replace(":", "")) < 1800)
                                    {
                                        col2 = "#FFCCCB";
                                    }
                                }

                            }

                            if (!body.Contains(exec[i].NAME))
                            {
                                var liste = exec.Where(x => x.NAME.Equals(exec[i].NAME)).ToList();
                                if (liste.Count > 1)
                                {
                                    var t = "";
                                    body = body + "<tr><td rowspan='" + liste.Count + "' style='border: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>" + liste[0].NAME + "</td>";
                                    for (var a = 0; a < liste.Count; a++)
                                    {
                                        if (a == 0)
                                        {
                                            if (liste[a].GIRIS != "-")
                                            {
                                                t = "<td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col + "'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].CIKIS + "</td></tr>";
                                            }
                                            else
                                            {
                                                t = "<td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].CIKIS + "</td></tr>";
                                            }

                                        }
                                        else if (a == liste.Count - 1)
                                        {
                                            if (liste[a].CIKIS != "-")
                                            {
                                                if (currentTime.DayOfWeek == DayOfWeek.Saturday)
                                                {

                                                    if (Int32.Parse(liste[a].CIKIS.Replace(":", "")) < 1500)
                                                    {
                                                        col2 = "#FFCCCB";
                                                    }
                                                    else
                                                    {
                                                        col2 = "lightgreen";
                                                    }
                                                }
                                                else
                                                {

                                                    if (Int32.Parse(liste[a].CIKIS.Replace(":", "")) < 1800)
                                                    {
                                                        col2 = "#FFCCCB";
                                                    }
                                                    else
                                                    {
                                                        col2 = "lightgreen";
                                                    }
                                                }

                                            }
                                            if (liste[a].CIKIS != "-")
                                            {
                                                t = t + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col2 + "'>" + liste[a].CIKIS + "</td></tr>";
                                            }
                                            else
                                            {
                                                t = t + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>-</td></tr>";
                                            }

                                        }
                                        else
                                        {

                                            t = t + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].CIKIS + "</td></tr>";
                                        }


                                    }
                                    body = body + t;
                                }
                                else
                                {
                                    if (liste[0].CIKIS != "-")
                                    {
                                        body = body + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>" + liste[0].NAME + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col + "'>" + liste[0].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col2 + "'>" + liste[0].CIKIS + "</td></tr>";
                                    }
                                    else
                                    {
                                        body = body + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>" + liste[0].NAME + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col + "'>" + liste[0].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>-</td></tr>";
                                    }

                                }
                            }




                        }
                        var img = NovaImzaModel.Imza;
                        var table = "Günaydın,</br></br>" + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek] + " günü 08:30 öncesi personel hareket raporudur:</br></br> <table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<tr><th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> İsim </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspGiriş Zamanı&nbsp</th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspÇıkış Zamanı&nbsp</th></tr>" + body + "</table>" + img;
                        
                        string den = exec2[0].BCC.Substring(0, exec2[0].BCC.Length - 1);
                        string den1 = exec2[0].CC;
                        string den2 = exec2[0].TO;
                        if (den1 != null)
                        {
                            mail.CC.Add(den1.Substring(0, exec2[0].BCC.Length - 1));
                        }
                        if (den2 != null)
                        {
                            mail.To.Add(den2.Substring(0, exec2[0].TO.Length - 1));
                        }

                        mail.Bcc.Add(den);
                        //mail.Bcc.Add("ergunozbudakli@efecegalvaniz.com");
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = table;
                        mail.Subject = currentTime.Day.ToString().PadLeft(2, '0') + "." + currentTime.Month.ToString().PadLeft(2, '0') + "." + currentTime.Year.ToString().PadLeft(2, '0') + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek].ToUpper() + " PERSONEL HAREKET RAPORU";



                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = e.Message;
                        mail.Subject = currentTime.Day.ToString().PadLeft(2, '0') + "." + currentTime.Month.ToString().PadLeft(2, '0') + "." + currentTime.Year.ToString().PadLeft(2, '0') + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek].ToUpper() + " PERSONEL HAREKET RAPORU";
                        mail.Bcc.Add("surecgelistirme@efecegalvaniz.com");


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                   
                }
                else if ((currentTime.Hour == 18 && currentTime.Minute == 30 && currentTime.Second == 0 && currentTime.Millisecond == 0 && currentTime.DayOfWeek != DayOfWeek.Sunday && currentTime.DayOfWeek != DayOfWeek.Saturday) || (currentTime.Hour == 15 && currentTime.Minute == 30 && currentTime.Second == 0 && currentTime.Millisecond == 0 && currentTime.DayOfWeek == DayOfWeek.Saturday))
                {
                    MailMessage mail = new MailMessage();
                    try
                    {
                        string query2 = @" EXEC SP_SEND_MAIL 2";
                        List<MailModel> exec2 = null;
                        string sqldataSource2 = _configuration.GetConnectionString("Connn");
                        SqlDataReader sqlreader2;
                        using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                        {
                            mycon2.Open();
                            using (SqlCommand myCommand1 = new SqlCommand(query2, mycon2))
                            {
                                sqlreader2 = myCommand1.ExecuteReader();
                                exec2 = DataReaderMapToList<MailModel>(sqlreader2);
                                sqlreader2.Close();
                                mycon2.Close();
                            }
                        }


                        string query1 = @"EXEC RAPOR_TEST";
                        var body = "";
                        List<RaporTest> exec = null;
                        string sqldataSource1 = _configuration.GetConnectionString("Connn");
                        SqlDataReader sqlreader1;
                        using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                        {
                            mycon1.Open();
                            using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                            {
                                sqlreader1 = myCommand1.ExecuteReader();
                                exec = DataReaderMapToList<RaporTest>(sqlreader1);
                                sqlreader1.Close();
                                mycon1.Close();
                            }
                        }
                        for (var i = 0; i < exec.Count; i++)
                        {
                            var col = "white";
                            var col2 = "lightgreen";
                            if (exec[i].GIRIS != "-")
                            {
                                if (Int32.Parse(exec[i].GIRIS.Replace(":", "")) > 800)
                                {
                                    col = "#FFCCCB";
                                }
                                else
                                {
                                    col = "lightgreen";
                                }
                            }

                            if (currentTime.DayOfWeek == DayOfWeek.Saturday)
                            {
                                if (Int32.Parse(exec[i].CIKIS.Replace(":", "")) < 1500)
                                {
                                    col2 = "#FFCCCB";
                                }
                            }
                            else
                            {
                                if (exec[i].CIKIS != "-")
                                {
                                    if (Int32.Parse(exec[i].CIKIS.Replace(":", "")) < 1800)
                                    {
                                        col2 = "#FFCCCB";
                                    }
                                }

                            }

                            if (!body.Contains(exec[i].NAME))
                            {
                                var liste = exec.Where(x => x.NAME.Equals(exec[i].NAME)).ToList();
                                if (liste.Count > 1)
                                {
                                    var t = "";
                                    body = body + "<tr><td rowspan='" + liste.Count + "' style='border: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>" + liste[0].NAME + "</td>";
                                    for (var a = 0; a < liste.Count; a++)
                                    {
                                        if (a == 0)
                                        {

                                            t = "<td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col + "'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].CIKIS + "</td></tr>";
                                        }
                                        else if (a == liste.Count - 1)
                                        {
                                            if (liste[a].CIKIS != "-")
                                            {
                                                if (currentTime.DayOfWeek == DayOfWeek.Saturday)
                                                {
                                                    if (Int32.Parse(liste[a].CIKIS.Replace(":", "")) < 1500)
                                                    {
                                                        col2 = "#FFCCCB";
                                                    }
                                                    else
                                                    {
                                                        col2 = "lightgreen";
                                                    }
                                                }
                                                else
                                                {
                                                    if (Int32.Parse(liste[a].CIKIS.Replace(":", "")) < 1800)
                                                    {
                                                        col2 = "#FFCCCB";
                                                    }
                                                    else
                                                    {
                                                        col2 = "lightgreen";
                                                    }
                                                }

                                            }
                                            t = t + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col2 + "'>" + liste[a].CIKIS + "</td></tr>";
                                        }
                                        else
                                        {

                                            t = t + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>" + liste[a].CIKIS + "</td></tr>";
                                        }


                                    }
                                    body = body + t;
                                }
                                else
                                {
                                    if (liste[0].CIKIS != "-")
                                    {
                                        body = body + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>" + liste[0].NAME + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col + "'>" + liste[0].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col2 + "'>" + liste[0].CIKIS + "</td></tr>";
                                    }
                                    else
                                    {
                                        body = body + "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>" + liste[0].NAME + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:" + col + "'>" + liste[0].GIRIS + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white'>-</td></tr>";
                                    }

                                }
                            }




                        }
                        var img = NovaImzaModel.Imza;
                        var table = "İyi akşamlar,</br></br>" + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek] + " günü 18:30 öncesi personel hareket raporudur:</br></br><table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<tr><th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> İsim </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspGiriş Zamanı&nbsp</th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspÇıkış Zamanı&nbsp</th></tr>" + body + "</table></br></br>" + img;
                       
                        string den = exec2[0].BCC.Substring(0, exec2[0].BCC.Length - 1);
                        string den1 = exec2[0].CC;
                        string den2 = exec2[0].TO;
                        if (den1 != null)
                        {
                            mail.CC.Add(den1.Substring(0, exec2[0].BCC.Length - 1));
                        }
                        if (den2 != null)
                        {
                            mail.To.Add(den2.Substring(0, exec2[0].TO.Length - 1));
                        }

                        mail.Bcc.Add(den);
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = table;
                        mail.Subject = currentTime.Day.ToString().PadLeft(2, '0') + "." + currentTime.Month.ToString().PadLeft(2, '0') + "." + currentTime.Year.ToString().PadLeft(2, '0') + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek].ToUpper() + " PERSONEL HAREKET RAPORU";


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        mail.Bcc.Add("surecgelistirme@efecegalvaniz.com");
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = e.Message;
                        mail.Subject = currentTime.Day.ToString().PadLeft(2, '0') + "." + currentTime.Month.ToString().PadLeft(2, '0') + "." + currentTime.Year.ToString().PadLeft(2, '0') + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek].ToUpper() + " PERSONEL HAREKET RAPORU";


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                    
                }
                else if (currentTime.Hour == 18 && currentTime.Minute == 45 && currentTime.Second == 0 && currentTime.Millisecond == 0 && currentTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    MailMessage mail = new MailMessage();
                    try
                    {
                        string query2 = @" EXEC SP_SEND_MAIL 3";
                        List<MailModel> exec2 = null;
                        string sqldataSource2 = _configuration.GetConnectionString("Connn");
                        SqlDataReader sqlreader2;
                        using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                        {
                            mycon2.Open();
                            using (SqlCommand myCommand1 = new SqlCommand(query2, mycon2))
                            {
                                sqlreader2 = myCommand1.ExecuteReader();
                                exec2 = DataReaderMapToList<MailModel>(sqlreader2);
                                sqlreader2.Close();
                                mycon2.Close();
                            }
                        }


                        string query1 = @"EXEC NOVA_SP_MSIP_KONTROL";
                        var body = "";
                        List<msipModel> exec = null;
                        string sqldataSource1 = _configuration.GetConnectionString("Conn");
                        SqlDataReader sqlreader1;
                        using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                        {
                            mycon1.Open();
                            using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                            {
                                sqlreader1 = myCommand1.ExecuteReader();
                                exec = DataReaderMapToList<msipModel>(sqlreader1);
                                sqlreader1.Close();
                                mycon1.Close();
                            }
                        }
                        string query3 = @"EXEC NOVA_SP_MSIP_KONTROL2";

                        List<MSIPModel2> exec3 = null;
                        string sqldataSource3 = _configuration.GetConnectionString("Conn");
                        SqlDataReader sqlreader3;
                        using (SqlConnection mycon3 = new SqlConnection(sqldataSource3))
                        {
                            mycon3.Open();
                            using (SqlCommand myCommand3 = new SqlCommand(query3, mycon3))
                            {
                                sqlreader3 = myCommand3.ExecuteReader();
                                exec3 = DataReaderMapToList<MSIPModel2>(sqlreader3);
                                sqlreader3.Close();
                                mycon3.Close();
                            }
                        }
                        var ToplamTable = "<table style='border: 1px solid #FFDDDD;border-collapse: collapse;'><tr><th style='border: 1px solid #FFDDDD;border-collapse: collapse;width:125px;text-align: center'>GENEL TOPLAM TL</th><th style='border: 1px solid #FFDDDD;border-collapse: collapse;width:125px;text-align: center'>GENEL TOPLAM USD</th><th style='border: 1px solid #FFDDDD;border-collapse: collapse;width:125px;text-align: center'>KUR</th></tr><tr><td style='border: 1px solid #FFDDDD;border-collapse: collapse;width:125px;text-align: center'>" + exec3[0].NET_TUTAR.ToString("N", new CultureInfo("tr-TR")) + "</td><td style='border: 1px solid #FFDDDD;border-collapse: collapse;width:125px;text-align: center'>" + exec3[1].NET_TUTAR.ToString("N", new CultureInfo("tr-TR")) + "</td><td style='border: 1px solid #FFDDDD;border-collapse: collapse;width:125px;text-align: center'>" + exec3[1].KUR + "</td></tr></table>";
                        List<string> names = new List<string>();
                        names = exec.Select(x => x.PLASIYER_ADI).Distinct().ToList();
                        List<PlasiyerOrtModel> plasiyerOrtModelList = new List<PlasiyerOrtModel>();
                        int zarar = exec.Where(x => x.FIYAT_KONTROL < 1 && x.DEPO_KODU != 10).ToList().Count;
                        int kar = exec.Where(x => x.FIYAT_KONTROL >= 1 && x.DEPO_KODU != 10).ToList().Count;
                        int bilgisiz = exec.Where(x => x.FIYAT_KONTROL == null && x.DEPO_KODU != 10).ToList().Count;
                        int direkt = exec.Where(x => x.DEPO_KODU == 10).ToList().Count;
                        int toplam = exec.Count;
                        double zararort = ((double.Parse(zarar.ToString()) * 100) / double.Parse(toplam.ToString()));
                        double karort = ((double.Parse(kar.ToString()) * 100) / double.Parse(toplam.ToString()));
                        double bilgisizort = ((double.Parse(bilgisiz.ToString()) * 100) / double.Parse(toplam.ToString()));
                        double direktort = ((double.Parse(direkt.ToString()) * 100) / double.Parse(toplam.ToString()));
                        for (var i = 0; i < names.Count; i++)
                        {
                            int zararkisi = exec.Where(x => x.FIYAT_KONTROL < 1 && x.DEPO_KODU != 10 && x.PLASIYER_ADI == names[i]).ToList().Count;
                            int karkisi = exec.Where(x => x.FIYAT_KONTROL >= 1 && x.DEPO_KODU != 10 && x.PLASIYER_ADI == names[i]).ToList().Count;
                            int bilgisizkisi = exec.Where(x => x.FIYAT_KONTROL == null && x.DEPO_KODU != 10 && x.PLASIYER_ADI == names[i]).ToList().Count;
                            int direktkisi = exec.Where(x => x.DEPO_KODU == 10 && x.PLASIYER_ADI == names[i]).ToList().Count;
                            double zararortkisi = ((double.Parse(zararkisi.ToString()) * 100) / double.Parse(toplam.ToString()));
                            double karortkisi = ((double.Parse(karkisi.ToString()) * 100) / double.Parse(toplam.ToString()));
                            double bilgisizortkisi = ((double.Parse(bilgisizkisi.ToString()) * 100) / double.Parse(toplam.ToString()));
                            double direktortkisi = ((double.Parse(direktkisi.ToString()) * 100) / double.Parse(toplam.ToString()));
                            int toplamkisi = exec.Where(x => x.PLASIYER_ADI == names[i]).ToList().Count;
                            PlasiyerOrtModel plasiyerOrt = new PlasiyerOrtModel
                            {
                                Plasiyer = names[i],
                                Top = toplamkisi,
                                Zarar = zararortkisi,
                                Kar = karortkisi,
                                Eksik = bilgisizortkisi,
                                FabDirekt = direktortkisi
                            };
                            plasiyerOrtModelList.Add(plasiyerOrt);
                        }
                        for (var i = 0; i < exec.Count; i++)
                        {
                            var col = "#ACFFAC";

                            if (exec[i].DEPO_KODU != 10)
                            {
                                if (exec[i].FIYAT_KONTROL != null)
                                {
                                    if (exec[i].FIYAT_KONTROL < 1)
                                    {
                                        col = "#FFDDDD";
                                    }
                                }
                                else
                                {
                                    col = "#D5FFFF";
                                }
                            }
                            else
                            {
                                col = "#FFFFFF";
                            }



                            if (!body.Contains(exec[i].FISNO))
                            {
                                var liste = exec.Where(x => x.FISNO.Equals(exec[i].FISNO)).ToList();
                                if (liste.Count > 1)
                                {
                                    var t = "";
                                    if (liste.Count > 0)
                                    {
                                        body = body + "<tr style='background:" + col + ";border-top:4px solid black;'><td rowspan='" + liste.Count + "' style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px;background:white;'>&nbsp" + liste[0].FISNO + "&nbsp</td>";
                                    }
                                    else
                                    {
                                        body = body + "<tr style='background:" + col + ";border-top:4px solid black;border-bottom:4px solid black;'><td rowspan='" + liste.Count + "' style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px;background:white;'>&nbsp" + liste[0].FISNO + "&nbsp</td>";
                                    }

                                    for (var a = 0; a < liste.Count; a++)
                                    {
                                        if (a == 0)
                                        {
                                            t = "<td rowspan='" + liste.Count + "' style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white;'>&nbsp" + liste[a].KAYIT_ZAMANI.Substring(0, 5) + "&nbsp</td><td rowspan='" + liste.Count + "' style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;background:white;'>" + liste[a].PLASIYER_ADI + "</td><td rowspan='" + liste.Count + "' style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].CARI_ISIM + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:left;'>1- " + liste[a].STOK_ADI + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].MIKTAR2 + " " + liste[a].SP_OLCUBR2 + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].MIKTAR + " " + liste[a].SP_OLCUBR + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_BRUT_FIYAT + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_ISKONTO + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_NET_FIYAT + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_DOVIZ_FIYATI + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_DOVIZ_TIPI + "</td><" + (liste[a].BAZ_KUR == "SIPARIS" ? "th" : "td") + " style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].SP_KUR + "</" + (liste[a].BAZ_KUR == "SIPARIS" ? "th" : "td") + "><" + (liste[a].BAZ_KUR == "NETSIS" ? "th" : "td") + " style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].NETSIS_KUR + "</" + (liste[a].BAZ_KUR == "NETSIS" ? "th" : "td") + "><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].BAZ_KUR + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>&nbsp" + liste[a].FIYATKODU + "&nbsp</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].LISTE_BRUT_FIYAT.Replace(".", ",") + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].LISTE_ISKONTO + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + (liste[a].LISTE_NET_TOPTAN_FIYAT != null ? liste[a].LISTE_NET_TOPTAN_FIYAT.Replace(".", ",") : "") + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].FIYATDOVIZTIPI + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].HES_TL_FIYAT + "</td></tr>";
                                        }
                                        else if (a == liste.Count - 1)
                                        {
                                            col = "#ACFFAC";
                                            if (liste[a].DEPO_KODU != 10)
                                            {
                                                if (liste[a].FIYAT_KONTROL != null)
                                                {
                                                    if (liste[a].FIYAT_KONTROL < 1)
                                                    {
                                                        col = "#FFDDDD";
                                                    }
                                                }
                                                else
                                                {
                                                    col = "#D5FFFF";
                                                }
                                            }
                                            else
                                            {
                                                col = "#FFFFFF";
                                            }
                                            t = t + "<tr style='background:" + col + ";border-bottom:4px solid black;'><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:left;'>" + (a + 1).ToString() + "- " + liste[a].STOK_ADI + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].MIKTAR2 + " " + liste[a].SP_OLCUBR2 + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].MIKTAR + " " + liste[a].SP_OLCUBR + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_BRUT_FIYAT + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_ISKONTO + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_NET_FIYAT + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_DOVIZ_FIYATI + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_DOVIZ_TIPI + "</td><" + (liste[a].BAZ_KUR == "SIPARIS" ? "th" : "td") + " style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_KUR + "</" + (liste[a].BAZ_KUR == "SIPARIS" ? "th" : "td") + "><" + (liste[a].BAZ_KUR == "NETSIS" ? "th" : "td") + " style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].NETSIS_KUR + "</" + (liste[a].BAZ_KUR == "NETSIS" ? "th" : "td") + "><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].BAZ_KUR + "</td><td style='border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>&nbsp" + liste[a].FIYATKODU + "&nbsp</td><td style='font-style: italic;border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].LISTE_BRUT_FIYAT.Replace(".", ",") + "</td><td style='font-style: italic;border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].LISTE_ISKONTO + "</td><td style='font-style: italic;border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + (liste[a].LISTE_NET_TOPTAN_FIYAT != null ? liste[a].LISTE_NET_TOPTAN_FIYAT.Replace(".", ",") : "") + "</td><td style='font-style: italic;border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].FIYATDOVIZTIPI + "</td><td style='font-style: italic;border-bottom: 4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].HES_TL_FIYAT + "</td></tr>";
                                        }
                                        else
                                        {
                                            col = "#ACFFAC";
                                            if (liste[a].DEPO_KODU != 10)
                                            {
                                                if (liste[a].FIYAT_KONTROL != null)
                                                {
                                                    if (liste[a].FIYAT_KONTROL < 1)
                                                    {
                                                        col = "#FFDDDD";
                                                    }
                                                }
                                                else
                                                {
                                                    col = "#D5FFFF";
                                                }
                                            }
                                            else
                                            {
                                                col = "#FFFFFF";
                                            }
                                            t = t + "<tr style='background:" + col + "'><td style='border: 1px solid #CCCCCC;border-right:1px solid #CCCCCC;border-collapse: collapse;text-align:left;'>" + (a + 1).ToString() + "- " + liste[a].STOK_ADI + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].MIKTAR2 + " " + liste[a].SP_OLCUBR2 + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].MIKTAR + " " + liste[a].SP_OLCUBR + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_BRUT_FIYAT + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_ISKONTO + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_NET_FIYAT + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_DOVIZ_FIYATI + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].SP_DOVIZ_TIPI + "</td><" + (liste[a].BAZ_KUR == "SIPARIS" ? "th" : "td") + " style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[a].SP_KUR + "</" + (liste[a].BAZ_KUR == "SIPARIS" ? "th" : "td") + "><" + (liste[a].BAZ_KUR == "NETSIS" ? "th" : "td") + " style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].NETSIS_KUR + "</" + (liste[a].BAZ_KUR == "NETSIS" ? "th" : "td") + "><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].BAZ_KUR + "</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>&nbsp" + liste[a].FIYATKODU + "&nbsp</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].LISTE_BRUT_FIYAT.Replace(".", ",") + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].LISTE_ISKONTO + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + (liste[a].LISTE_NET_TOPTAN_FIYAT != null ? liste[a].LISTE_NET_TOPTAN_FIYAT.Replace(".", ",") : "") + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].FIYATDOVIZTIPI + "</td><td style='font-style: italic;border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[a].HES_TL_FIYAT + "</td></tr>";
                                        }




                                    }
                                    body = body + t;
                                }
                                else
                                {
                                    if (i != exec.Count - 1)
                                    {
                                        body = body + "<tr style='background:" + col + "'><td rowspan='" + liste.Count + "' style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>&nbsp" + liste[0].FISNO + "&nbsp</td>" +
                                                                           "<td style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center'>&nbsp" + liste[0].KAYIT_ZAMANI.Substring(0, 5) + "&nbsp</td><td style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center'>" + liste[0].PLASIYER_ADI + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;background:white;text-align:center;'>" + liste[0].CARI_ISIM + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:left;'>1- " + liste[0].STOK_ADI + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[0].MIKTAR2 + " " + liste[0].SP_OLCUBR2 + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[0].MIKTAR + " " + liste[0].SP_OLCUBR + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_BRUT_FIYAT + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_ISKONTO + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_NET_FIYAT + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_DOVIZ_FIYATI + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_DOVIZ_TIPI + "</td><" + (liste[0].BAZ_KUR == "SIPARIS" ? "th" : "td") + " style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_KUR + "</" + (liste[0].BAZ_KUR == "SIPARIS" ? "th" : "td") + "><" + (liste[0].BAZ_KUR == "NETSIS" ? "th" : "td") + " style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].NETSIS_KUR + "</" + (liste[0].BAZ_KUR == "NETSIS" ? "th" : "td") + "><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].BAZ_KUR + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].FIYATKODU + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].LISTE_BRUT_FIYAT.Replace(".", ",") + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].LISTE_ISKONTO + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + (liste[0].LISTE_NET_TOPTAN_FIYAT != null ? liste[0].LISTE_NET_TOPTAN_FIYAT.Replace(".", ",") : "") + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].FIYATDOVIZTIPI + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].HES_TL_FIYAT + "</td></tr>";
                                    }
                                    else
                                    {
                                        body = body + "<tr style='background:" + col + "'><td rowspan='" + liste.Count + "' style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse; text-align:center;width:150px'>&nbsp" + liste[0].FISNO + "&nbsp</td>" +
                                       "<td style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center'>&nbsp" + liste[0].KAYIT_ZAMANI.Substring(0, 5) + "&nbsp</td><td style='background:white;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center'>" + liste[0].PLASIYER_ADI + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;background:white;text-align:center;'>" + liste[0].CARI_ISIM + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:left;'>1- " + liste[0].STOK_ADI + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[0].MIKTAR2 + " " + liste[0].SP_OLCUBR2 + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;white-space: nowrap;'>" + liste[0].MIKTAR + " " + liste[0].SP_OLCUBR + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_BRUT_FIYAT + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_ISKONTO + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_NET_FIYAT + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_DOVIZ_FIYATI + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_DOVIZ_TIPI + "</td><" + (liste[0].BAZ_KUR == "SIPARIS" ? "th" : "td") + " style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].SP_KUR + "</" + (liste[0].BAZ_KUR == "SIPARIS" ? "th" : "td") + "><" + (liste[0].BAZ_KUR == "NETSIS" ? "th" : "td") + " style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].NETSIS_KUR + "</" + (liste[0].BAZ_KUR == "NETSIS" ? "th" : "td") + "><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].BAZ_KUR + "</td><td style='border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].FIYATKODU + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].LISTE_BRUT_FIYAT.Replace(".", ",") + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].LISTE_ISKONTO + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + (liste[0].LISTE_NET_TOPTAN_FIYAT != null ? liste[0].LISTE_NET_TOPTAN_FIYAT.Replace(".", ",") : "") + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].FIYATDOVIZTIPI + "</td><td style='font-style: italic;border-bottom:4px solid black;border-right: 1px solid #CCCCCC;border-collapse: collapse;text-align:center;'>" + liste[0].HES_TL_FIYAT + "</td></tr>";
                                    }




                                }
                            }




                        }
                        var img = NovaImzaModel.Imza;
                        var kisiselOrtTr = "";
                        plasiyerOrtModelList = plasiyerOrtModelList.OrderByDescending(x => x.Kar).ToList();
                        for (var i = 0; i < plasiyerOrtModelList.Count; i++)
                        {
                            kisiselOrtTr += "<tr style='font-size:12px'><th style='border: 1px solid #CCCCCC;text-align: center'>" + (i + 1).ToString() + "</th><td style='border: 1px solid #CCCCCC;text-align: center'>" + plasiyerOrtModelList[i].Plasiyer + "</td><td style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(plasiyerOrtModelList[i].Kar, 1) + " %" + "</td><td style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(plasiyerOrtModelList[i].Zarar, 1) + " %" + "</td><td style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(plasiyerOrtModelList[i].Eksik, 1) + " %" + "</td><td style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(plasiyerOrtModelList[i].FabDirekt, 1) + " %" + "</td><td style='border: 1px solid #CCCCCC;text-align: center'>" + plasiyerOrtModelList[i].Top + "</td></tr>";
                        }

                        var table = "İyi akşamlar,</br></br>" + currentTime.Day.ToString() + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.MonthNames[(int)currentTime.Month - 1] + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek] + " günü 18:45 öncesi açılan müşteri siparişlerinin raporudur:</br></br>" +
                            "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'><tr><td style='width:50px'></td><td style='width:150px'></td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;width:125px;background:#ACFFAC;text-align: center'>Liste Fiyatı Üstü</td><td style='width:125px;background:#FFDDDD;border: 1px solid #CCCCCC;border-collapse: collapse;text-align: center'>Liste Fiyatı Altı</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;width:100px;background:#D5FFFF;text-align: center'>Eksik Bilgi</td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align: center;width:100px'>Fabrika Direkt</td><th></th></tr><tr><th style='width:50px text-align: center'>Sıra</th><th style='width:150px text-align: center'>Plasiyer</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(karort, 1) + " %" + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(zararort, 1) + " %" + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(bilgisizort, 1) + " %" + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + Math.Round(direktort, 1) + " %" + "</th><th style='text-align: center;width:100px'>Toplam</th></tr>" + kisiselOrtTr + "</tr><tr><th style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center'></th><th style='border: 1px solid #CCCCCC;border-collapse: collapse;text-align:center'>Sipariş Sayısı</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + kar + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + zarar + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + bilgisiz + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + direkt + "</th><th style='border: 1px solid #CCCCCC;text-align: center'>" + toplam + "</th></tr></table></br>" + ToplamTable +
                            "</br><table style='border: 1px solid #CCCCCC;border-collapse: collapse;'><thead>" +
                        "<tr style='border-bottom:4px solid black;'><th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspFİŞ NO&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspKAYIT ZAMANI&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspPLASİYER ADI&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspCARİ İSİM&nbsp</th><th style='position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'> STOK ADI </th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;white-space: nowrap;'>&nbspMİKTAR 1&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;white-space: nowrap;'>&nbspMİKTAR 2&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspBRÜT FİYAT&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspİSKONTO&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspNET FİYAT&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspDÖVİZ FİYATI&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspDÖVİZ TİPİ&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspKUR&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspNETSİS&nbspKUR&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspBAZ&nbspKUR&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspFİYAT KODU&nbsp</th><th style='position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspLİSTE BRÜT FİYATI&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspLİSTE İSKONTO&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspLİSTE NET TOPTAN FİYAT&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspFİYAT DÖVİZ TİPİ&nbsp</th>" +
                        "<th style='border-bottom:4px solid black;position: sticky;border: 1px solid #CCCCCC;border-collapse: collapse;'>&nbspLİSTE FİYATI TL KARŞILIĞI&nbsp</th></tr></thead><tbody>"
                        + body + "</tbody></table>" + img;
                        
                        string den = exec2[0].BCC.Substring(0, exec2[0].BCC.Length - 1);
                        //string den1 = exec2[0].CC;
                        //string den2 = exec2[0].TO;
                        //if (den1 != null)
                        //{
                        //    mail.CC.Add(den1.Substring(0, exec2[0].BCC.Length - 1));
                        //}
                        //if (den2 != null)
                        //{
                        //    mail.To.Add(den2.Substring(0, exec2[0].TO.Length - 1));
                        //}

                        mail.Bcc.Add(den);
                        //mail.Bcc.Add("ergunozbudakli@efecegalvaniz.com");
                        //mail.Bcc.Add("ugurkonakci@efecegalvaniz.com");
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = "<html>" + table + "</html>";
                        mail.Subject = currentTime.Day.ToString().PadLeft(2, '0') + "." + currentTime.Month.ToString().PadLeft(2, '0') + "." + currentTime.Year.ToString().PadLeft(2, '0') + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek].ToUpper() + " MÜŞTERİ SİPARİŞİ KONTROL RAPORU";


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        mail.Bcc.Add("surecgelistirme@efecegalvaniz.com");
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = e.Message;
                        mail.Subject = currentTime.Day.ToString().PadLeft(2, '0') + "." + currentTime.Month.ToString().PadLeft(2, '0') + "." + currentTime.Year.ToString().PadLeft(2, '0') + " " + CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)currentTime.DayOfWeek].ToUpper() + " MÜŞTERİ SİPARİŞİ KONTROL RAPORU";


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                    
                }
                else if (currentTime.Hour == 17 && currentTime.Minute == 30 && currentTime.Second == 0 && currentTime.DayOfWeek != DayOfWeek.Sunday && currentTime.DayOfWeek != DayOfWeek.Saturday && currentTime.Millisecond == 0)
                {
                    MailMessage mail = new MailMessage();
                    try
                    {
                        DataTable table = new DataTable();
                        string query = @"EXEC dbo.SP_URETIM_GUNSONU @date='" + currentTime.ToString("yyyy-MM-dd") + "'";
                        string sqldataSource = _configuration.GetConnectionString("connn");
                        SqlDataReader sqlreader;
                        using (SqlConnection mycon = new SqlConnection(sqldataSource))
                        {
                            mycon.Open();
                            using (SqlCommand myCommand = new SqlCommand(query, mycon))
                            {
                                sqlreader = myCommand.ExecuteReader();
                                table.Load(sqlreader);
                                sqlreader.Close();
                                mycon.Close();
                            }
                        }

                        var d = StokController.DataTableToJSONWithJavaScriptSerializer(table);
                        var rapor = JsonConvert.DeserializeObject<List<RaporVeri>>(d).AsQueryable();
                        string body = GunlukUretimRaporuOlustur(rapor) + GunlukDepoRaporuOlustur(rapor);
                        var img = NovaImzaModel.Imza;
                        mail.Bcc.Add("ugurkonakci@efecegalvaniz.com");
                        mail.Bcc.Add("ergunozbudakli@efecegalvaniz.com");
                        mail.Bcc.Add("dincersipka@efecegalvaniz.com");
                        mail.Bcc.Add("muratruzgar@efecegalvaniz.com");
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = body + img;
                        mail.Subject = "ÜRETİM RAPORU " + currentTime.ToString("dd.MM.yyyy");


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;


                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        mail.From = new MailAddress("sistem@efecegalvaniz.com");
                        mail.Body = e.Message;
                        mail.Subject = "ÜRETİM RAPORU " + currentTime.ToString("dd.MM.yyyy");


                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = "192.168.2.13";
                        smtp.UseDefaultCredentials = true;


                        smtp.Send(mail);

                    }
                   

                }
                else
                {
                    test = true;
                    CashClass.Clear(_memoryCache);
                }
               
                //if (currentTime.Minute % 2 == 0 && currentTime.Second == 0)
                //{
                //    _ = DetayliAktarAsync();
                //    _ = StokAktarAsync();
                //}

                //var onlineusers = _context.VW_SESSION.Where(x => x.ACTIVITY_TYPE == "login").ToList();
                //for (var i = 0; i < onlineusers.Count; i++)
                //{
                //    if (DateTime.Parse(onlineusers[i].LOG_DATETIME).AddHours(2) < currentTime)
                //    {
                //        LogInTBL item = new LogInTBL();
                //        item.PLATFORM = onlineusers[i].PLATFORM;
                //        item.LOG_DATETIME = DateTime.Parse(onlineusers[i].LOG_DATETIME).AddHours(2);
                //        item.ACTIVITY_TYPE = "logout";
                //        item.USER_ID = Int32.Parse(onlineusers[i].USER_ID.ToString());
                //        item.USER_NAME = onlineusers[i].USER_NAME;
                //        _context.TBL_USER_SESSION_LOG.Add(item);
                //        _context.SaveChanges();
                //    }
                //}



            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        private void DoWork(DateTime date)
        {
            MailMessage mail = new MailMessage();
            try
            {
                string query2 = @" EXEC SP_SEND_MAIL 1";
                List<MailModel> exec2 = null;
                string sqldataSource2 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader2;
                using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                {
                    mycon2.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query2, mycon2))
                    {
                        sqlreader2 = myCommand1.ExecuteReader();
                        exec2 = DataReaderMapToList<MailModel>(sqlreader2);
                        sqlreader2.Close();
                        mycon2.Close();
                    }
                }

                string URI = "http://192.168.2.13:83/api/login/login";
                string myParameters = "Email=ergunozbudakli@efecegalvaniz.com&Password=begum142088";
                string HtmlResult = "";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    HtmlResult = wc.UploadString(URI, myParameters);

                }

                var apiUrl = "http://192.168.2.13:83/api/musteri";
                //Connect API
                Uri url = new Uri(apiUrl);
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("Authorization", "Bearer " + HtmlResult);
                string json = client.DownloadString(url);
                var apiUrluser = "http://192.168.2.13:83/api/user";
                //Connect API
                Uri urluser = new Uri(apiUrluser);
                WebClient clientuser = new WebClient();
                clientuser.Encoding = System.Text.Encoding.UTF8;
                clientuser.Headers.Add("Authorization", "Bearer " + HtmlResult);
                string jsonuser = clientuser.DownloadString(urluser);
                List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(jsonuser);
                var apiUrlurun = "http://192.168.2.13:83/api/musteri/urun";
                //Connect API
                Uri urlurun = new Uri(apiUrlurun);
                WebClient clienturun = new WebClient();
                clienturun.Encoding = System.Text.Encoding.UTF8;
                clienturun.Headers.Add("Authorization", "Bearer " + HtmlResult);
                string jsonurun = clienturun.DownloadString(urlurun);
                List<MusteriUrunModel> urunler = JsonConvert.DeserializeObject<List<MusteriUrunModel>>(jsonurun);
                List<MusteriModel> musteriList = JsonConvert.DeserializeObject<List<MusteriModel>>(json);

                var randevuUrl = "http://192.168.2.13:83/api/musteri/randevu";
                //Connect API
                Uri urlrandevu = new Uri(randevuUrl);
                WebClient clientrandevu = new WebClient();
                clientrandevu.Encoding = System.Text.Encoding.UTF8;
                string jsonrandevu = clientrandevu.DownloadString(urlrandevu);
                List<MusteriRandevuModel> randevu = JsonConvert.DeserializeObject<List<MusteriRandevuModel>>(jsonrandevu);
                var randevuplanlanan = randevu.Where(x => (DateTime)x.PLANLANAN_TARIH > date && (DateTime)x.PLANLANAN_TARIH < date.AddDays(7)).ToList();
                var randevugerceklesen = randevu.Where(x => (DateTime)x.PLANLANAN_TARIH > date.AddDays(-7)).ToList();
                var uruneklenen = urunler.Where(x => (DateTime)x.DEGISIKLIK_TARIHI > date.AddDays(-7)).ToList();
                List<string> plasiyerler = new List<string>();

                int GetWeekNumber(DateTime date)
                {
                    return (date.DayOfYear - GetWeekday(date.DayOfWeek) + 10) / 7;
                }
                int GetWeekday(DayOfWeek dayOfWeek)
                {
                    return dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
                }
                Parallel.ForEach(musteriList, musteri =>
                {
                    var plasiyer = musteri.PLASIYER;
                    if (!plasiyerler.Contains(plasiyer))
                    {
                        plasiyerler.Add(plasiyer);
                    }
                });
                Parallel.ForEach(plasiyerler, plasiyer =>
                {
                    var fullname = users.Where(x => x.USER_NAME == plasiyer).ToList();
                    List<int> musteriIdler = new List<int>();
                    var icerik = "";
                    var icerik1 = "";
                    var icerik2 = "";
                    var icerik3 = "";
                    string subject = "CRM Raporu | " + GetWeekNumber(date) + ".Hafta | " + fullname[0].USER_FIRSTNAME + " " + fullname[0].USER_LASTNAME;
                    var plMusteriList = musteriList.Where(x => x.PLASIYER == plasiyer).ToList();
                    var plrandevuplanlanan = randevuplanlanan.Where(x => x.KAYIT_YAPAN_KULLANICI_ID == fullname[0].USER_ID).ToList();
                    var plUrunList = uruneklenen.Where(x => x.KAYIT_YAPAN_KULLANICI == plasiyer).ToList();
                    for (int a = 0; a < plMusteriList.Count; a++)
                    {
                        var musteriId = plMusteriList[a].MUSTERI_ID;
                        if (!musteriIdler.Contains(musteriId))
                        {
                            musteriIdler.Add(musteriId);
                        }
                        if (plMusteriList[a].KAYIT_TARIHI > date.AddDays(-7))
                        {
                            var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_ADI + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_IL + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_ILCE + " </td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_NOTU + " </td></tr>";
                            icerik = icerik + tr;
                        }

                    }
                    for (int a = 0; a < musteriIdler.Count; a++)
                    {
                        var f = randevuplanlanan.Where(x => x.MUSTERI_ID == musteriIdler[a]).ToList();

                        if (f != null)
                        {
                            for (int b = 0; b < f.Count(); b++)
                            {
                                var ad = musteriList.Find(x => x.MUSTERI_ID == musteriIdler[a]).MUSTERI_ADI;
                                if (f[b].GERCEKLESEN_TARIH == null)
                                {
                                    var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + ad + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(f[b].PLANLANAN_TARIH.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(f[b].KAYIT_TARIHI.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + f[b].RANDEVU_NOTU + " </td></tr>";
                                    icerik1 = icerik1 + tr;
                                }




                            }

                        }
                        var e = randevugerceklesen.Where(x => x.MUSTERI_ID == musteriIdler[a]).ToList();

                        if (e != null)
                        {
                            for (int b = 0; b < e.Count(); b++)
                            {
                                var ad = musteriList.Find(x => x.MUSTERI_ID == musteriIdler[a]).MUSTERI_ADI;
                                if (e[b].GERCEKLESEN_TARIH != null)
                                {
                                    var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + ad + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(e[b].PLANLANAN_TARIH.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(e[b].GERCEKLESEN_TARIH.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + e[b].RANDEVU_NOTU + " </td></tr>";
                                    icerik2 = icerik2 + tr;
                                }




                            }

                        }

                    }

                    if (plUrunList.Count > 0)
                    {
                        for (int a = 0; a < plUrunList.Count; a++)
                        {
                            var ad = musteriList.Find(x => x.MUSTERI_ID == plUrunList[a].MUSTERI_ID).MUSTERI_ADI;
                            var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + ad + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plUrunList[a].URUN_GRUBU + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + Decimal.Parse(plUrunList[a].YILLIK_KULLANIM.ToString()).ToString("#,##0") + " " + plUrunList[a].OLCU_BIRIMI + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(plUrunList[a].KAYIT_TARIHI.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(plUrunList[a].DEGISIKLIK_TARIHI.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td></tr>";
                            icerik3 = icerik3 + tr;

                        }
                    }

                    var table1 = "";
                    var table2 = "";
                    var table3 = "";
                    var table4 = "";
                    var img = NovaImzaModel.Imza;
                    if (icerik == "")
                    {
                        table1 = "<p>Geçtiğimiz hafta kaydedilen herhangi bir müşteri yoktur.</p>";
                    }
                    else
                    {
                        table1 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri İl </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri İlçe </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Notu </th>" + icerik + "</table></br></br></br>";
                    }
                    if (icerik1 == "")
                    {
                        table2 = "<p>Gelecek hafta için planlanmış herhangi bir ziyaret yoktur.</p>";
                    }
                    else
                    {
                        table2 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Planlanan Tarih </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Kayıt Tarihi </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Randevu Notu </th>" + icerik1 + "</table>";
                    }
                    if (icerik2 == "")
                    {
                        table3 = "<p>Geçtiğimiz hafta için gerçekleşmiş herhangi bir ziyaret yoktur.</p>";
                    }
                    else
                    {
                        table3 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Planlanan Tarih </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Gerçekleşme Tarihi </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Randevu Notu </th>" + icerik2 + "</table>";
                    }
                    if (icerik3 == "")
                    {
                        table4 = "<p>Geçtiğimiz hafta için eklenmiş veya düzenlenmiş herhangi bir ürün yoktur.</p>";
                    }
                    else
                    {
                        table4 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Ürün Grubu </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Yıllık Kullanım </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Kayıt Tarihi </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Düzenleme Tarihi </th>" + icerik3 + "</table>";
                    }

                    string body = "<h2>Geçtiğimiz Hafta Kaydedilen Müşteriler</h2>" +
                             table1 +
                            "<hr>" +
                            "<h2>Geçtiğimiz Hafta Gerçekleşen Müşteri Ziyaretleri</h2>" +
                            table3 +
                            "<hr><h2>Geçtiğimiz Hafta Eklenen veya Düzenlenen Ürünler</h2>" +
                            table4 +
                            "<hr><h2>Gelecek Haftaya Planlanan Müşteri Ziyaretleri</h2>" +
                            table2 +
                            "</br></br></br>" + img;


                    
                    string den = exec2[0].BCC.Substring(0, exec2[0].BCC.Length - 1);
                    mail.Bcc.Add(den);
                    //mail.Bcc.Add("ergunozbudakli@efecegalvaniz.com");
                    //mail.Bcc.Add("ugurkonakci@efecegalvaniz.com");
                    mail.From = new MailAddress("sistem@efecegalvaniz.com");
                    mail.Body = body;
                    mail.Subject = subject;


                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = "192.168.2.13";
                    smtp.UseDefaultCredentials = true;

                    if (fullname[0].USER_FIRSTNAME != "Admin")
                    {

                        smtp.Send(mail);


                    }
                });
            }
            catch (Exception e)
            {
                mail.Bcc.Add("surecgelistirme@efecegalvaniz.com");
                mail.From = new MailAddress("sistem@efecegalvaniz.com");
                mail.Body = e.Message;
                mail.Subject = "HATA";


                mail.IsBodyHtml = true;

                SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "192.168.2.13";
                smtp.UseDefaultCredentials = true;
                smtp.Send(mail);


            }
            
           



        }
      
      
        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }
        public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Worker stopping");
            return System.Threading.Tasks.Task.CompletedTask;
        }

       
        class UserModel
        {
            public int USER_ID { get; set; }
            public string USER_NAME { get; set; }
            public string USER_FIRSTNAME { get; set; }
            public string USER_LASTNAME { get; set; }
        }
        class ContentModel
        {
            public string body { get; set; }
            public bool fromMe { get; set; }
        }
        class FiyatKodModel
        {
            public string FIYATKODU { get; set; }
        }
        class FiyatKodModel2
        {
            public int SIRA_NO { get; set; }
            public string FIYATKODU { get; set; }
        }
        public class Rapor
        {
            public string NAME { get; set; }
            public string DATE { get; set; }
        }
        public class RaporTest
        {
            public int USER_ID { get; set; }
            public string NAME { get; set; }
            public string GIRIS { get; set; }
            public string CIKIS { get; set; }
        }
        public class MailModel
        {
            public string? TO { get; set; }
            public string? CC { get; set; }
            public string? BCC { get; set; }
            public string? SUBJECT { get; set; }
            public string? SALUTATION { get; set; }
            public string? BODY { get; set; }
            public string? CLOSING { get; set; }
            public string? SIGNATURE { get; set; }
        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        class PlasiyerOrtModel
        {
            public string Plasiyer { get; set; }
            public double Kar { get; set; }
            public double Zarar { get; set; }
            public double Eksik { get; set; }
            public double FabDirekt { get; set; }
            public int Top { get; set; }
        }
        #region UretimRaporlari

        #region Ortak
        public string DetayStil = "height: 25px; background-color: #1f4e780d; border: 1px solid #2e509033; padding: 0.25rem;";
        public string DetayKoyuStil = "height: 25px; background-color: #1f4e7817; border: 1px solid #2e509033; padding: 0.25rem;";
        public string BosSatirStil = "height: 25px; background-color: white;";

        public Dictionary<string, string> UretimRaporToplam(IQueryable<RaporVeri> Data)
        {
            double? MIKTAR1 = 0;
            double? MIKTAR2 = 0;
            double? METRAJ = 0;

            foreach (RaporVeri Veri in Data)
            {
                MIKTAR1 += Veri.MIKTAR1;
                MIKTAR2 += Veri.MIKTAR2;
                METRAJ += Veri.TOPLAM_METRAJ;

                Console.WriteLine(Veri.TOPLAM_METRAJ.ToString());
            }

            return new Dictionary<string, string>()
     {
         { "MIKTAR1", SayiFormat(MIKTAR1) },
         { "MIKTAR2", SayiFormat(MIKTAR2) },
         { "METRAJ", SayiFormat(METRAJ, 1) }
     };
        }

        public Dictionary<string, string> DepoRaporToplam(IQueryable<RaporVeri> Data)
        {
            double? MIKTAR1 = 0;
            double? MIKTAR2 = 0;

            foreach (RaporVeri Veri in Data)
            {
                MIKTAR1 += Veri.MIKTAR1;
                MIKTAR2 += Veri.MIKTAR2;
            }

            return new Dictionary<string, string>()
     {
         { "MIKTAR1", SayiFormat(MIKTAR1) },
         { "MIKTAR2", SayiFormat(MIKTAR2) }
     };
        }

        public string SayiFormat(double? Value, int Digit = 0)
        {
            if (Value == 0)
                return "";

            return Value?.ToString($"N{Digit}").Replace(',', ' ').Replace('.', ',').Replace(' ', '.');
        }

        public class RaporVeri
        {
            public string TARIH { get; set; }
            public string TIP { get; set; }
            public string MAKINE { get; set; }
            public string MALZEME_ADI1 { get; set; }
            public string OLCU_BR1 { get; set; }
            public double? BOY { get; set; }
            public double? MIKTAR1 { get; set; }
            public double? MIKTAR2 { get; set; }
            public string SIPARISI_ACAN { get; set; }
            public string MUSTERI { get; set; }
            public string SEVK_YERI { get; set; }
            public double? TOPLAM_METRAJ { get; set; }
        }
        #endregion

        #region GunlukRapor

        public string GunlukRaporTipStil = "background-color: #2e5090; color: white; padding: 0;";
        public string GunlukRaporTipToplamStil = "height: 25px; background-color: #2e5090; color: white; padding: 0; border-bottom: 2px solid black; border-top: 2px solid black; font-weight: 600;";
        public string GunlukRaporOlcuBirimiStil = "background-color: #8ea9db;padding: 0;";
        public string GunlukRaporOlcuBirimiToplamStil = "height: 25px; background-color: #8ea9db; padding: 0; border-bottom: 2px solid black; border-top: 2px solid black; font-weight: 600;";
        public string GunlukRaporMakineStil = "background-color: #d9e1f2; padding: 0; border: 1px solid #2e509033;";
        public string GunlukRaporMakineToplamStil = "height: 25px; background-color: #d9e1f2; padding: 0; border-bottom: 2px solid black; border-top: 2px solid black; font-weight: 600;";

        public string GunlukUretimRaporuOlustur(IQueryable<RaporVeri> Rapor)
        {
            return $"<div style=\"height: 60px;font-weight: 600;text-align: center;vertical-align: middle;font-size: x-large;margin-bottom: 1rem;\">GÜNLÜK RAPOR</div>\r\n\r\n                <table id=\"uretim-sevkiyat-table\" style=\"border-collapse: collapse;text-align: center;vertical-align: middle;  width: 100%; margin-bottom: 3rem;\">\r\n                <thead>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">ÜRETİM/SEVKİYAT</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">ÖLÇÜ BİRİMİ</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">MAKİNE</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">SİPARİŞİ AÇAN</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">MÜŞTERİ İSMİ</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">SEVK YERİ</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">MALZEME ADI</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">Toplam MİKTAR1</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">Toplam MİKTAR2</th>\r\n                    <th style=\"height: 60px; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">Toplam METRAJ**</th>\r\n                </thead>\r\n                <tbody>\r\n                    {GunlukUretimRaporuTip(Rapor.Where(r => r.TIP != "DEPO"))["Template"]}\r\n                </tbody>\r\n            </table>";
        }

        public IDictionary<string, string> GunlukUretimRaporuTip(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> Tipler = Data.GroupBy(r => r.TIP)
                                            .Select(grouping => grouping.OrderBy(r => r.TIP)
                                            .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri Tip in Tipler)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.TIP == Tip.TIP);

                Dictionary<string, string> TipToplam = UretimRaporToplam(FiltreliVeri);

                IDictionary<string, string> OlcuBirimiBilgi = GunlukUretimRaporuOlcuBirimi(FiltreliVeri);

                RowSpan += Int16.Parse(OlcuBirimiBilgi["Span"]) + 3;

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{GunlukRaporTipStil}\" rowspan=\"{Int16.Parse(OlcuBirimiBilgi["Span"]) + 1}\">{Tip.TIP}</td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine($"{OlcuBirimiBilgi["Template"]}");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{GunlukRaporTipToplamStil}\" colspan=\"7\">Toplam {Tip.TIP}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporTipToplamStil}\">{TipToplam["MIKTAR1"]}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporTipToplamStil}\">{TipToplam["MIKTAR2"]}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporTipToplamStil}\">{TipToplam["METRAJ"]} </td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{BosSatirStil}\" colspan=\"10\"></td>");
                Builder.AppendLine("</tr>");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        public IDictionary<string, string> GunlukUretimRaporuOlcuBirimi(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> OlcuBirimleri = Data.GroupBy(r => r.OLCU_BR1)
                                                    .Select(grouping => grouping.OrderBy(r => r.OLCU_BR1)
                                                    .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri OlcuBirimi in OlcuBirimleri)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.OLCU_BR1 == OlcuBirimi.OLCU_BR1);

                Dictionary<string, string> OlcuBirimiToplam = UretimRaporToplam(FiltreliVeri);

                IDictionary<string, string> MakineBilgi = GunlukUretimRaporuMakine(FiltreliVeri);

                RowSpan += Int16.Parse(MakineBilgi["Span"]) + 3;

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td  style=\"{GunlukRaporOlcuBirimiStil}\" rowspan=\"{Int16.Parse(MakineBilgi["Span"]) + 1}\">{OlcuBirimi.OLCU_BR1}</td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine($"{MakineBilgi["Template"]}");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{GunlukRaporOlcuBirimiToplamStil}\" colspan=\"6\">Toplam {OlcuBirimi.OLCU_BR1}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporOlcuBirimiToplamStil}\">{OlcuBirimiToplam["MIKTAR1"]}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporOlcuBirimiToplamStil}\">{OlcuBirimiToplam["MIKTAR2"]}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporOlcuBirimiToplamStil}\">{OlcuBirimiToplam["METRAJ"]} </td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{BosSatirStil}\" colspan=\"9\"></td>");
                Builder.AppendLine("</tr>");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        public IDictionary<string, string> GunlukUretimRaporuMakine(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> Makineler = Data.GroupBy(p => p.MAKINE)
                                                .Select(grouping => grouping.OrderBy(g => g.MAKINE)
                                                .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri Makine in Makineler)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.MAKINE == Makine.MAKINE);

                Dictionary<string, string> MakineToplam = UretimRaporToplam(FiltreliVeri);

                RowSpan += FiltreliVeri.Count() + 3;

                Builder.AppendLine($"<tr><td style=\"{GunlukRaporMakineStil}\" rowspan=\"{FiltreliVeri.Count() + 1}\">{Makine.MAKINE}</td></tr>");

                for (int i = 0; i < FiltreliVeri.Count(); i++)
                {
                    Builder.AppendLine("<tr>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{FiltreliVeri.ElementAt(i).SIPARISI_ACAN}</td>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{FiltreliVeri.ElementAt(i).MUSTERI}</td>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{FiltreliVeri.ElementAt(i).SEVK_YERI}</td>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{FiltreliVeri.ElementAt(i).MALZEME_ADI1}</td>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{SayiFormat(FiltreliVeri.ElementAt(i).MIKTAR1)}</td>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{SayiFormat(FiltreliVeri.ElementAt(i).MIKTAR2)}</td>");
                    Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{SayiFormat(FiltreliVeri.ElementAt(i).TOPLAM_METRAJ, 1)}</td>");
                    Builder.AppendLine("</tr>");
                }

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{GunlukRaporMakineToplamStil}\" colspan=\"5\">Toplam {Makine.MAKINE}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporMakineToplamStil}\">{MakineToplam["MIKTAR1"]}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporMakineToplamStil}\">{MakineToplam["MIKTAR2"]}</td>");
                Builder.AppendLine($"<td style=\"{GunlukRaporMakineToplamStil}\">{MakineToplam["METRAJ"]} </td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{BosSatirStil}\" colspan=\"8\"></td>");
                Builder.AppendLine("</tr>");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        #endregion

        #region DepoRapor

        public string DepoRaporMakineStil = "background-color: #2e5090; color: white; padding: 0;";
        public string DepoRaporMakineToplamStil = "height: 25px; background-color: #2f75b5; color: white; padding: 0; border-bottom: 2px solid black; border-top: 2px solid black; font-weight: 600;";
        public string DepoRaporSiparisiAcanStil = "background-color: #8ea9db; padding: 0;";
        public string DepoRaporMusteriStil = "background-color: #fff2cc;padding: 0;";
        public string DepoRaporMusteriToplamStil = "height: 25px; background-color: #fff2cc; padding: 0; border-top: 2px solid black; border-bottom: 2px solid black;";
        public string DepoRaporSevkYeriStil = "background-color: #8ea9db; padding: 0;";
        public string DepoRaporToplamStil = "height: 25px; background-color: #1f4e780d; padding: 0;";

        public string GunlukDepoRaporuOlustur(IQueryable<RaporVeri> Rapor)
        {
            Dictionary<string, string> DepoToplam = DepoRaporToplam(Rapor);

            return $"<div style=\"height: 2rem;font-weight: 600;text-align: center;vertical-align: middle;font-size: x-large;margin-bottom: 1rem;\">DEPO BAKİYELERİ</div>\r\n\r\n                    <table id=\"depo-table\" style=\"outline: 2px solid #1f4e78; outline-style: auto; border-collapse: collapse;text-align: center;vertical-align: middle; width: 100%;\">\r\n                        <thead>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">MAKİNE</th>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">SİPARİŞİ AÇAN</th>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">MÜŞTERİ İSMİ</th>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">SEVK YERİ</th>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">MALZEME ADI</th>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">Toplam DEPO BAKİYE 1</th>\r\n                            <th style=\"height: 2.5rem; background-color: #1f4e78; color: white; border: 1px solid #2c6ca5;padding: 0.25rem;\">Toplam DEPO BAKİYE 2</th>\r\n                        </thead>\r\n                        <tbody>\r\n\r\n                            {GunlukDepoRaporuMakine(Rapor.Where(r => r.TIP == "DEPO"))["Template"]}\r\n\r\n                            <tr>\r\n                                <td style=\"{DepoRaporToplamStil}\"  colspan=\"5\">Genel Toplam</td>\r\n                                <td style=\"{DepoRaporToplamStil}\" >{DepoToplam["MIKTAR1"]}</td>\r\n                                <td style=\"{DepoRaporToplamStil}\" >{DepoToplam["MIKTAR2"]}</td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>";
        }

        public IDictionary<string, string> GunlukDepoRaporuMakine(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> Veriler = Data.GroupBy(r => r.MAKINE)
                                                    .Select(grouping => grouping.OrderBy(r => r.MAKINE)
                                                    .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri Veri in Veriler)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.MAKINE == Veri.MAKINE);

                Dictionary<string, string> MakineToplam = DepoRaporToplam(FiltreliVeri);

                IDictionary<string, string> SiparisiAcanBilgi = GunlukDepoRaporuSiparisiAcan(FiltreliVeri);

                RowSpan += Int16.Parse(SiparisiAcanBilgi["Span"]) + 1;

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td  style=\"{DepoRaporMakineStil}\" rowspan=\"{Int16.Parse(SiparisiAcanBilgi["Span"]) + 1}\">{Veri.MAKINE}</td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine($"{SiparisiAcanBilgi["Template"]}");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{DepoRaporMakineToplamStil}\" colspan=\"5\">Toplam {Veri.MAKINE}</td>");
                Builder.AppendLine($"<td style=\"{DepoRaporMakineToplamStil}\">{MakineToplam["MIKTAR1"]}</td>");
                Builder.AppendLine($"<td style=\"{DepoRaporMakineToplamStil}\">{MakineToplam["MIKTAR2"]}</td>");
                Builder.AppendLine("</tr>");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        public IDictionary<string, string> GunlukDepoRaporuSiparisiAcan(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> Veriler = Data.GroupBy(r => r.SIPARISI_ACAN)
                                                    .Select(grouping => grouping.OrderBy(r => r.SIPARISI_ACAN)
                                                    .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri Veri in Veriler)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.SIPARISI_ACAN == Veri.SIPARISI_ACAN);

                IDictionary<string, string> MusteriBilgi = GunlukDepoRaporuMusteri(FiltreliVeri);

                RowSpan += Int16.Parse(MusteriBilgi["Span"]) + 1;

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td  style=\"{DepoRaporSiparisiAcanStil}\" rowspan=\"{Int16.Parse(MusteriBilgi["Span"]) + 1}\">{Veri.SIPARISI_ACAN}</td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine($"{MusteriBilgi["Template"]}");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        public IDictionary<string, string> GunlukDepoRaporuMusteri(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> Veriler = Data.GroupBy(r => r.MUSTERI)
                                                    .Select(grouping => grouping.OrderBy(r => r.MUSTERI)
                                                    .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri Veri in Veriler)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.MUSTERI == Veri.MUSTERI);

                Dictionary<string, string> MusteriToplam = DepoRaporToplam(FiltreliVeri);

                IDictionary<string, string> SevkYeriBilgi = GunlukDepoRaporuSevkYeri(FiltreliVeri);

                RowSpan += Int16.Parse(SevkYeriBilgi["Span"]) + 2;

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td  style=\"{DepoRaporMusteriStil}\" rowspan=\"{Int16.Parse(SevkYeriBilgi["Span"]) + 1}\">{Veri.MUSTERI}</td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine($"{SevkYeriBilgi["Template"]}");

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{DepoRaporMusteriToplamStil}\" colspan=\"3\">Toplam {Veri.MUSTERI}</td>");
                Builder.AppendLine($"<td style=\"{DepoRaporMusteriToplamStil}\">{MusteriToplam["MIKTAR1"]}</td>");
                Builder.AppendLine($"<td style=\"{DepoRaporMusteriToplamStil}\">{MusteriToplam["MIKTAR2"]}</td>");
                Builder.AppendLine("</tr>");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        public IDictionary<string, string> GunlukDepoRaporuSevkYeri(IQueryable<RaporVeri> Data)
        {
            IQueryable<RaporVeri> Veriler = Data.GroupBy(r => r.SEVK_YERI)
                                                    .Select(grouping => grouping.OrderBy(r => r.SEVK_YERI)
                                                    .First());

            StringBuilder Builder = new StringBuilder();
            int RowSpan = 0;

            foreach (RaporVeri Veri in Veriler)
            {
                IQueryable<RaporVeri> FiltreliVeri = Data.Where(r => r.SEVK_YERI == Veri.SEVK_YERI);

                IDictionary<string, string> MalzemeBilgi = GunlukDepoRaporuMalzeme(FiltreliVeri);

                RowSpan += Int16.Parse(MalzemeBilgi["Span"]) + 1;

                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td  style=\"{DepoRaporSevkYeriStil}\" rowspan=\"{Int16.Parse(MalzemeBilgi["Span"]) + 1}\">{Veri.SEVK_YERI}</td>");
                Builder.AppendLine("</tr>");

                Builder.AppendLine($"{MalzemeBilgi["Template"]}");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", RowSpan.ToString()}
         };
        }

        public IDictionary<string, string> GunlukDepoRaporuMalzeme(IQueryable<RaporVeri> Data)
        {
            StringBuilder Builder = new StringBuilder();

            for (int i = 0; i < Data.Count(); i++)
            {
                Builder.AppendLine("<tr>");
                Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{Data.ElementAt(i).MALZEME_ADI1}</td>");
                Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{SayiFormat(Data.ElementAt(i).MIKTAR1)}</td>");
                Builder.AppendLine($"<td style=\"{(i % 2 == 0 ? DetayStil : DetayKoyuStil)}\" >{SayiFormat(Data.ElementAt(i).MIKTAR2)}</td>");
                Builder.AppendLine("</tr>");
            }

            return new Dictionary<string, string>()
         {
             {"Template", Builder.ToString()},
             {"Span", Data.Count().ToString()}
         };
        }

        #endregion

        #endregion
    }
}

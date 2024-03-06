using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
//using NetOpenX50;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using static SqlApi.Controllers.FiyatController;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeklifController : ControllerBase
    {
        [HttpGet]
        public IEnumerable GetJson()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            var apiUrl = "http://assets.ino.com/data/quote/?format=json&s=FOREX_USDTRY";


            Uri url = new Uri(apiUrl);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string json = client.DownloadString(url);
           
            JObject jsonobj = JObject.Parse(json);
            //END

            return jsonobj;
        }
        //[HttpGet("TeklifOlustur")]
        //public IEnumerable TeklifOlustur()
        //{

            
        //    Kernel kernel = new Kernel();
        //    Sirket sirket = default(Sirket);
        //    Fatura fatura = default(Fatura);
        //    FatUst fatUst = default(FatUst);
        //    FatKalem fatKalem = default(FatKalem);
        //    try
        //    {
        //        sirket = kernel.yeniSirket(TVTTipi.vtMSSQL,
        //                                         "TEST2022",
        //                                         "TEMELSET",
        //                                         "",
        //                                         "nova",
        //                                         "Efc@+180", 0);

        //        //kaynakfatura = kernel.yeniFatura(sirket, TFaturaTip.ftSatTeklif);
        //        //kaynakfatura.OkuUst("T00000000003410", "DENEME 1");
        //        ////fatUst.CariKod = "12035200101568";
        //        //fatUst = kaynakfatura.Ust();
        //        //fatUst.Tarih = DateTime.Now;
        //        //kaynakfatura.kayitDuzelt();
        //        fatura = kernel.yeniFatura(sirket, TFaturaTip.ftSatTeklif);
        //        fatura.OkuUst("T00000000003423", "DENEME 1");
        //        fatura.OkuKalem();
        //        fatKalem = fatura.get_Kalem(0);
        //        fatKalem.STra_NF = 31.31;
        //        fatKalem.STra_BF = 31.31;
        //        fatura.kayitDuzelt();

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    finally
        //    {
        //        Marshal.ReleaseComObject(fatKalem);
        //        Marshal.ReleaseComObject(fatUst);
        //        Marshal.ReleaseComObject(fatura);
        //        Marshal.ReleaseComObject(sirket);
        //        kernel.FreeNetsisLibrary();
        //        Marshal.ReleaseComObject(kernel);
        //    }

        //    return null;
        //}

    }
}

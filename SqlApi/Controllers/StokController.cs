using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using static SqlApi.Controllers.ExcelTestController;
using Nancy.Json;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Nancy;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StokController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        public JsonResult Get()
        {
            
            
            DataTable table = new DataTable();
            
            
                string query = @"EXEC dbo.NOVA_OZELKOSUL";

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
            

            
            
            
            return new JsonResult(table);
        }
        [HttpGet("yedek")]
        public IActionResult GetYedek()
        {


            DataTable table = new DataTable();


            string query = @"EXEC dbo.NOVA_OZELKOSUL_YEDEK";

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





            return new JsonResult(table);
        }
        public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JsonSerializer jsSerializer = new JsonSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName.ToUpper(), row[col]);
                }
                parentRow.Add(childRow);
            }
         

            return  JsonConvert.SerializeObject(parentRow);
        }
        [HttpGet("yedek1")]
        public string GetYedek1()
        {


            DataTable table = new DataTable();


            string query = @"EXEC oz.NOVA_OZELKOSUL_YEDEK";
            string sqldataSource = _configuration.GetConnectionString("connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader =myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }
            




            return  DataTableToJSONWithJavaScriptSerializer(table);
        }
        [HttpGet("ozelkosul")]
        public JsonResult GetOzel()
        {


            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_NOVA_FIYAT_OZEL_KOSUL WITH(NOLOCK)";

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





            return new JsonResult(table);
        }
        [HttpGet("gruplar")]
        public JsonResult GetGruplar()
        {


            DataTable table = new DataTable();


            string query = @"SELECT GRUP_KOD,EFECE2023.DBO.TRK(GRUP_ISIM) AS GRUP_ISIM FROM EFECE2023..TBLSTGRUP WITH (NOLOCK) WHERE GRUP_ISIM IS NOT NULL AND GRUP_KOD IN(SELECT DISTINCT(GRUP_KODU) FROM EFECE2023..TBLSTSABIT ST WITH (NOLOCK) INNER JOIN EFECE2023..TBLSTHAR AS HAR WITH (NOLOCK) ON HAR.STOK_KODU = ST.STOK_KODU AND HAR.DEPO_KODU IN (1, 5, 8, 45) WHERE GRUP_KODU IS NOT NULL) AND GRUP_KOD NOT IN('0137','0135','0006') ORDER BY EFECE2023.DBO.TRK(GRUP_ISIM)";

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





            return new JsonResult(table);
        }
        [HttpGet("fiat")]
        public JsonResult GetExec()
        {

            DataTable table = new DataTable();
           

            string query = @"EXEC STOK_FIAT_PROC";

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
            

            



            return new JsonResult(table);
        }
        [HttpGet("rehber")]
        public JsonResult GETReh()
        {

            DataTable table = new DataTable();


            string query = @"EXEC SP_STOK_REHBER";

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
            return new JsonResult(table);
        }
        [HttpGet("depo")]
        public JsonResult GETDEPO()
        {

            DataTable table = new DataTable();


            string query = @"EXEC SP_DEPO_REHBER";

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






            return new JsonResult(table);
        }
        [HttpGet("miktar/{kod}")]
        public JsonResult GET(string kod)
        {


            DataTable table = new DataTable();


            string query = @"SELECT * FROM EFECE2023..TBLSTSABIT WHERE STOK_KODU='" + kod+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
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

            return new JsonResult(table);
        }
        public List<UretSeriNoModel> GetSeriNo()
        {


            var apiUrl = "http://192.168.2.13:83/api/URETSERINO/";

            //Connect API
            Uri url = new Uri(apiUrl);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            string json = client.DownloadString(url);
            //END

            //JSON Parse START
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<UretSeriNoModel> jsonList = ser.Deserialize<List<UretSeriNoModel>>(json);

            //END

            return jsonList;
        }
        [HttpGet("fiat2")]
        public JsonResult GetView()
        {
            DataTable table = new DataTable();
            string query = @"SELECT * FROM NOVA_VW_URETSERI_NO2";

            string sqldataSource = _configuration.GetConnectionString("conn");
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
            
            return new JsonResult(table);
        }
        public class FiatList
        {
            public string Depo { get; set; }
            public string Fiyatkodu { get; set; }
            public string FkAciklama { get; set; }
            public string StokKodu { get; set; }
            public string StokAdi { get; set; }
            public string? Olcubr { get; set; }
            public string? Fiyatdoviztipi { get; set; }
            public string Bakiye { get; set; }
            public string ListeFiyati { get; set; }
            public string? ToptanIsk { get; set; }
            public string? PerakendeIsk { get; set; }
            public string? ToptanFiyat { get; set; }
            public string? PerakendeFiyat { get; set; }
            public string? ToptanDeger { get; set; }
            public string? PerakendeDeger { get; set; }
            public string? Kur { get; set; }
            public string? ToptanDegerTl { get; set; }
            public string? PerakendeDegerTl { get; set; }
        }
        [HttpGet("fiat/{depo}")]
        public JsonResult GetExec(string depo)
        {

            DataTable table = new DataTable();


            string query = @"EXEC STOK_FIAT_PROC '"+depo+"'";

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





            return new JsonResult(table);
        }
        [HttpGet("{grupkodu}")]
        public JsonResult Get(string grupkodu)
        {

            DataTable table = new DataTable();


            string query = @"Select * From NOVA_VW_STOK_BAKIYE WHERE GRUP_KODU= '" + grupkodu+"'";

            string sqldataSource = _configuration.GetConnectionString("conn");
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





            return new JsonResult(table);
        }
        [HttpGet("{grupkodu}/{stokadi}")]
        public JsonResult Get(string grupkodu,string stokadi)
        {
            string query = null;
            string subquery = "";
            DataTable table = new DataTable();
            string[] separated = stokadi.Split(" ");
            for (int i = 0; i < separated.Length; i++)
            {
                if (i == separated.Length - 1)
                {
                    subquery += "lower(STOK_ADI) LIKE " + "'%" + separated[i] + "%' COLLATE Latin1_General_CI_AI";
                }
                else
                {
                    subquery += "lower(STOK_ADI) LIKE " + "'%" + separated[i] + "%' AND ";
                }

            }

            if (grupkodu == "null" && stokadi!="null")
            {
                query = @"Select * From NOVA_VW_STOK_BAKIYE WHERE " + subquery;
            }
            else if(grupkodu!="null" && stokadi!="null"){
                query = @"Select * From NOVA_VW_STOK_BAKIYE WHERE GRUP_KODU= '" + grupkodu + "' AND " + subquery;
            }
            else if(grupkodu== "null" && stokadi=="null") {
                query = @"Select * From NOVA_VW_STOK_BAKIYE ";
            }
            else if(grupkodu!="null" && stokadi == "null")
            {
                query = @"Select * From NOVA_VW_STOK_BAKIYE WHERE GRUP_KODU= '" + grupkodu+"'";
            }
            

            string sqldataSource = _configuration.GetConnectionString("conn");
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





            return new JsonResult(table);
        }
        [HttpGet("giriscikis/{stokkodu}")]
        public IActionResult GetGirisCikis(string stokkodu)
        {

            DataTable table = new DataTable();


            string query = @"EXEC SON3_GIRIS_CIKIS '"+ stokkodu+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
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
           
            return new JsonResult(table);
        }
        [HttpGet("exec/{sipno}")]
        public IActionResult GetBySipNoProc(string sipno)
        {
            DataTable table = new DataTable();
            string query = @"Exec NOVA_PROC_DETAYLI_URETMSIP " + "'"+sipno+"'";
            if (sipno == null || sipno=="")
            {
                query = @"Exec NOVA_PROC_DETAYLI_URETMSIP";
            }
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


            return new JsonResult(table);

        }
        [HttpGet("exec")]
        public IActionResult GetBySipNoProc1()
        {
            DataTable table = new DataTable();
            string query = @"Exec NOVA_PROC_DETAYLI_URETMSIP";
            
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


            return new JsonResult(table);

        }
        [HttpGet("stokfiat")]
        public IActionResult GetStokFiat()
        {
            DataTable table = new DataTable();
            string query = @"SELECT * FROM NOVA_VW_MEVCUT_STOK_FIYAT";

            string sqldataSource = _configuration.GetConnectionString("conn");
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


            return new JsonResult(table);

        }
        [HttpGet("giriscikis")]
        public JsonResult GetGirisCikis()
        {

            DataTable table = new DataTable();


            string query = @"EXEC SON3_GIRIS_CIKIS";

            string sqldataSource = _configuration.GetConnectionString("Connn");
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

            return new JsonResult(table);
        }

    }
}

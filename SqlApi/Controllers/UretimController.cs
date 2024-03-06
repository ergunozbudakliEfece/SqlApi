
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UretimController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{hat}")]
        public JsonResult Get(string hat)
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_URETIM_HRDKLT '"+hat+"'";
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
        [HttpGet("kontrol/{fis}")]
        public JsonResult GetKontrol(string fis)
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_NOVA_FISKONTROL '" + fis + "'";
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
        [HttpGet("anlik")]
        public JsonResult GetAnlik()
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_MES2";
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
        [HttpGet("stok/{stok}")]
        public JsonResult GetCariler(string stok)
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_STOK_CARI_BUL '"+stok+"'";
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
        [HttpGet("anlik/{date}")]
        public JsonResult GetAnlik(string date)
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_MES2 '"+date+"'";
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
        [HttpGet("AmGirisTablo")]
        public JsonResult GetAmGir()
        {
            DataTable table = new DataTable();
            string query = @"SELECT * FROM TBLSERI_AMBAR_GIRIS";
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
        [HttpGet("click/{serino}")]
        public JsonResult GetByBelgeNo(string serino)
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_MES3_CLICK '"+serino+"'";
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
        [HttpGet("click/toplu/{serinolar}")]
        public JsonResult GetToplu(string serinolar)
        {
            var seriler = new List<string>();
            var leng=serinolar.Split(',').Length;
            for(var i = 0; i < leng; i++)
            {
                seriler.Add(serinolar.Split(',')[i]);
            }
            DataTable table = new DataTable();
            for(var i=0;i< seriler.Count; i++)
            {
                string query = @"EXEC SP_MES3_CLICK '" + seriler[i] + "'";
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
            }
            
            return new JsonResult(table);
        }
        public class SeriNoModal
        {
            public string SERI_NO { get; set; }
        }
        [HttpGet("BelgeBul/{serino}")]
        public JsonResult GetByBelgeBul(string serino)
        {
            DataTable table = new DataTable();
            string query = @"EXEC SP_BELGENO_BUL '" + serino + "'";
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
        [HttpGet("Yazdirilanlar/{barkod}")]
        public string SetYaz(string barkod)
        {
            
          
                string query = @"INSERT INTO TBL_YAZDIRILAN_BARKODLAR(BARKODNO) VALUES('"+barkod+"')";
                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }
            
           
            return "BAŞARILI";
        }
        [HttpGet("stoklar")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client, NoStore = false)]
        public string GetStoklar([FromBody] DateTime date)
        {

            DataTable table = new DataTable();
            string query = @"EXEC dbo.SP_URETIM_GUNSONU @date='" + date.ToString("yyyy-MM-dd")+"'";
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





            return StokController.DataTableToJSONWithJavaScriptSerializer(table);
        }
        [HttpGet("tumstoklar")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client, NoStore = false)]
        public string GetAllStoklar()
        {

            DataTable table = new DataTable();
            string query = @"EXEC dbo.SP_NOVA_ALL_STOK";
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





            return StokController.DataTableToJSONWithJavaScriptSerializer(table);
        }
    }
}

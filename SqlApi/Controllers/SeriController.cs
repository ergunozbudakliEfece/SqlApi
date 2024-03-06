using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SeriController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{tip}")]
        public JsonResult GET(string tip)
        {


            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_SERISIRA WITH(NOLOCK) WHERE SERI_TIP='"+tip+"'";

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
        [HttpGet("hi/{depo}")]
        public JsonResult Hesapla(string depo)
        {


            DataTable table = new DataTable();


            string query = @"SP_SERI_REHBER_HI '" + depo+"'";

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
        [HttpGet("hesapla/{seri}/{miktar}")]
        public JsonResult Hesapla(string seri,int miktar)
        {


            DataTable table = new DataTable();


            string query = @"SP_DL_DAGITIM '"+seri+"',"+miktar;

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
        [HttpGet("exec/{tip}")]
        public JsonResult GETAll(string tip)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_GET_SERINUM '"+tip+"'";

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

        [HttpGet("{tip}/{num}")]
        public IActionResult Update(string tip,int num)
        {

            string query = @"EXEC SP_UPD_SERINUM '"+tip+"',"+num;

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

            return Ok();
        }
        [HttpGet("receteden/{stok}/{genislik}/{tip}")]
        public IActionResult GetRecetedenGetir(string stok,string genislik, string tip)
        {
            DataTable table = new DataTable();

            string query = @"exec SP_RECETEDEN_GETIR '"+stok+"','"+genislik+"','"+tip+"'";

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
        [HttpGet("serirehber")]
        public JsonResult GetSeriRehber()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SERI_REHBER";

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
        [HttpGet("serirehbertumu")]
        public JsonResult GetSeriRehberTumu()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SERI_REHBER_TUMU";

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
        [HttpGet("manuelhat/{hat}")]
        public JsonResult GetHatMan(string hat)
        {


            DataTable table = new DataTable();
            string query = "";
            if (hat == "TP01")
            {
                query = @"EXEC MANUEL_HAM_GIRDI_KONTROL '" + hat + "'";
            }
            else
            {
                query = @"EXEC MANUEL_HAM_GIRDI_KONTROL '" + hat + "'";
            }
           

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
        [HttpGet("manuelgir/{hat}/{seri}/{id}")]
        public string SeriGir(string hat,string seri,int id)
        {

            try
            {
                string query = @"INSERT INTO TBL_HAT_GIRDI(HAT_KODU,SERI_NO,USER_ID) VALUES('" + hat + "','" + seri + "'," + id + ")";

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
            }
            catch (System.Exception e)
            {

                return "Hata:" + e.Message;
            }
            

            return "Başarılı";
        }
        [HttpGet("manuelsil/{seri}")]
        public string SeriSil(string seri)
        {

            try
            {
                string query = @"DELETE FROM TBL_HAT_GIRDI WHERE SERI_NO='"+seri+"'";

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
            }
            catch (System.Exception e)
            {

                return "Hata:" + e.Message;
            }


            return "Başarılı";
        }
        [HttpGet("bakiye/{seri}/{depo}")]
        public JsonResult GETBAK(string seri,int depo)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_SERI_BAKIYE '"+seri+"',"+depo;

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
        [HttpGet("rehber/{stokkodu}/{depo}")]
        public JsonResult GETREHBER(string stokkodu,int depo)
        {


            DataTable table = new DataTable();
            string query = @"EXEC SP_SERI_REHBER '" + stokkodu + "',"+depo;
            
            

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
        [HttpGet("kontrol/{seri}")]
        public JsonResult GETSERI(string seri)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SERITRA_KONTROL '" + seri + "'";

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

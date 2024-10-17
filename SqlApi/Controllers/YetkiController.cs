using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YetkiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public YetkiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_YETKI_TABLOSU";

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
        [HttpGet("ozelyetkiler")]
        public JsonResult GetOzelYetki()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_YETKI_OZEL_TABLOSU";

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
        [HttpGet("ozel/detay")]
        public JsonResult GetOZEL()
        {
            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_OZEL_YETKI_DETAY";

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
        [HttpGet("ozelyetki")]
        public JsonResult GetOZELYETKILER()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_YETKI_OZEL_TABLOSU";

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
        [HttpGet("Kontrol/{userid}/{moduleid}")]
        public JsonResult GetKontrol(int userid,int moduleid)
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_YETKI_KONTROL "+userid+","+moduleid;

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
        [HttpGet("Ekle/{userid}/{moduleid}")]
        public string GetEkle(int userid, int moduleid)
        {
            try
            {
               

                string query = @"INSERT INTO TBL_YETKI  VALUES(" + userid + "," + moduleid + ")";

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
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpGet("copyrole/{userid}/{id}")]
        public string GetCopy(int userid, int id)
        {
            try
            {


                string query = @"SP_COPY_YETKI " + userid + "," + id;

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
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpGet("copyuser/{userid1}/{userid2}")]
        public string GetCopyUser(int userid1, int userid2)
        {
            try
            {


                string query = @"SP_COPY_YETKI_USER " + userid1 + "," + userid2;

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
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpGet("Sil/{userid}/{moduleid}")]
        public string GetSil(int userid, int moduleid)
        {
            try
            {


                string query = @"DELETE FROM TBL_YETKI WHERE USER_ID= "+userid+" AND MODULE_ID="+moduleid;

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
            catch (System.Exception)
            {

                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpGet("ozel/Ekle/{userid}/{moduleid}")]
        public string GetOZELEkle(int userid, int moduleid)
        {
            try
            {


                string query = @"INSERT INTO TBL_OZEL_YETKI  VALUES(" + userid + "," + moduleid + ")";

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
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        
        [HttpGet("ozel/Sil/{userid}/{moduleid}")]
        public string GetOZELSil(int userid, int moduleid)
        {
            try
            {


                string query = @"DELETE FROM TBL_OZEL_YETKI WHERE USER_ID= " + userid + " AND MODULE_ID=" + moduleid;

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
            catch (System.Exception)
            {

                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }

    }
    

}


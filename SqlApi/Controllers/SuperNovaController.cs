using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperNovaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SuperNovaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("request")]
        [Authorize]
        public JsonResult GET(ProcModel m)
        {


            DataTable table = new DataTable();
            string query = "";
            
                if (m.TEXT != "0")
                {
                    query = @"EXEC " + m.PROC + " '" + m.TEXT + "','" + m.USERID + "'";
                }
                else
                {
                    query = @"EXEC " + m.PROC + " '','" + m.USERID + "'";
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
        [HttpGet("request/{id}")]
        public JsonResult GETReq(string id)
        {


            DataTable table = new DataTable();
            string query = "";
            if(id != "0")
            {
                query = @"EXEC SP_SUPERNOVA_REQUEST '" + id + "'";
            }
            else
            {
                query = @"EXEC SP_SUPERNOVA_REQUEST ''";
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
        [HttpGet("stok/{stokadi}")]
        public JsonResult GETStokAdi(string stokadi)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SUPERNOVA_STOK_ADI '" + stokadi + "'";

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
        [HttpGet("auth/{id}")]
        public JsonResult GETAuth(int id)
        {


            DataTable table = new DataTable();
            string query = "";
            if (id != 0)
            {
                query = @"EXEC SP_SUPERNOVA_AUTH '" + id + "'";
            }
            else
            {
                query = @"EXEC SP_SUPERNOVA_AUTH";
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
        [HttpPost("auth/update/{questionid}/{userid}/{auth}")]
        public JsonResult UptAuth(string questionid,int userid,bool auth)
        {


            DataTable table = new DataTable();
            string query = @"EXEC SP_SUPERNOVA_AUTH_UPT '" + userid + "','"+ questionid + "',"+auth;
            


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

        public class ProcModel
        {
            public string TEXT { get; set; }
            public string PROC { get; set; }
            public int USERID { get; set; }
        }
    }
}

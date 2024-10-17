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
    public class BelgeController : ControllerBase
    {
       
        private readonly IConfiguration _configuration;

        public BelgeController( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{tip}")]
        public JsonResult GetBelgeSira(string tip)
        {


            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_BELGESIRA WITH(NOLOCK) WHERE SERI_TIP='" + tip + "'";

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


            string query = @"EXEC SP_GET_BELGENUM '" + tip + "'";

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

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
    public class MenuController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MenuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{userid}")]
        public JsonResult Get(int userid)
        {


            DataTable table = new DataTable();


            string query = "EXEC SP_USER_MENU "+userid;

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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace SqlApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration) { 
            _configuration = configuration; 
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * From TBL_NOVA_USERDATA";
            DataTable table= new DataTable();
            string sqldataSource = _configuration.GetConnectionString("con");
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

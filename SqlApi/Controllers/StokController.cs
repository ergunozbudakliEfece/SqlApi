using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            string query = @"Select * From BTE_VW_STOK_BAKIYE";
            DataTable table = new DataTable();
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
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/[Controller]")]
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
            
            
                string query = @"Select * From BTE_VW_STOK_BAKIYE";

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
        [HttpGet("{grupkodu}")]
        public JsonResult Get(string grupkodu)
        {

            DataTable table = new DataTable();


            string query = @"Select * From BTE_VW_STOK_BAKIYE WHERE GRUP_KODU= '"+grupkodu+"'";

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

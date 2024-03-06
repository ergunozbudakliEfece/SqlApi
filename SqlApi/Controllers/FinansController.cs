using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinansController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FinansController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{riskgrup}")]
        public JsonResult Get(string riskgrup)
        {
            DataTable table = new DataTable();

            string query = "";
            if(riskgrup != "0")
            {
                query = @"EXEC NOVA_SP_DETAYLI_RISK_LIMIT2 '" + riskgrup + "'";
            }
            else
            {
                query = @"EXEC NOVA_SP_DETAYLI_RISK_LIMIT2 ";
            }
            

            string sqldataSource = _configuration.GetConnectionString("Conn");
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

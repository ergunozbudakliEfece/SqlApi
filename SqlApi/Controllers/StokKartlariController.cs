using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokKartlariController : ControllerBase
    {
        private readonly FavContext _context;
        private readonly IConfiguration _configuration;
        public StokKartlariController(FavContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
      
        [HttpGet]
        public JsonResult GetAll()
        {
           DataTable table = new DataTable();


            string query = @"EXEC SP_STOK_KARTLARI";

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

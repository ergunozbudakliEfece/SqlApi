using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakineController : ControllerBase
    {
        private readonly FavContext _context;
        private readonly IConfiguration _configuration;
        public MakineController(FavContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_ACIK_IE_MAK.ToList();
        }
        [HttpGet("makineler")]
        public JsonResult GetMakineler()
        {
            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBLURETHAT WITH(NOLOCK)";

            string sqldataSource = _configuration.GetConnectionString("Con");
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
        [HttpGet("mes")]
        public JsonResult GetMes()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_MES";

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

        [HttpGet("mes/sub")]
        public JsonResult GetMesSub()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_MES_SUB";

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
        [HttpGet("uretim/{id}")]
        public JsonResult GetMakinelerUretimTipi(int id)
        {
            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_HAT_PRM WHERE ISEMRI_TIPI='"+id+"'";

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

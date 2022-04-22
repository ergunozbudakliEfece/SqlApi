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
        [HttpGet("{grupkodu}/{stokadi}")]
        public JsonResult Get(string grupkodu,string stokadi)
        {
            string query = null;
            string subquery = "";
            DataTable table = new DataTable();
            string[] separated = stokadi.Split(" ");
            for (int i = 0; i < separated.Length; i++)
            {
                if (i == separated.Length - 1)
                {
                    subquery += "lower(STOK_ADI) LIKE " + "'%" + separated[i] + "%' COLLATE Latin1_General_CI_AI";
                }
                else
                {
                    subquery += "lower(STOK_ADI) LIKE " + "'%" + separated[i] + "%' AND ";
                }

            }

            if (grupkodu == "null" && stokadi!="null")
            {
                query = @"Select * From BTE_VW_STOK_BAKIYE WHERE " + subquery;
            }
            else if(grupkodu!="null" && stokadi!="null"){
                query = @"Select * From BTE_VW_STOK_BAKIYE WHERE GRUP_KODU= '" + grupkodu + "' AND " + subquery;
            }
            else if(grupkodu== "null" && stokadi=="null") {
                query = @"Select * From BTE_VW_STOK_BAKIYE ";
            }
            else if(grupkodu!="null" && stokadi == "null")
            {
                query = @"Select * From BTE_VW_STOK_BAKIYE WHERE GRUP_KODU= '" + grupkodu+"'";
            }
            

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

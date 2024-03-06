using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SqlApi.Migrations;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAll]
    public class DetayliSipController : ControllerBase
    {
        private readonly StokContext _context;
        private readonly IConfiguration _configuration;
        public DetayliSipController(StokContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JsonSerializer jsSerializer = new JsonSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName.ToUpper(), row[col]);
                }
                parentRow.Add(childRow);
            }

            return JsonConvert.SerializeObject(parentRow);
        }
        [HttpGet]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IEnumerable> GetAllProducts()
        {
            //return await _context.URETMSIP.FromSqlInterpolated($"EXEC URETMSIP").ToListAsync();
            string query1 = @"EXEC dbo.URETMSIP";
            List<DetayliSipModel> stoklist = null;
            string sqldataSource1 = _configuration.GetConnectionString("Conn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = await myCommand1.ExecuteReaderAsync();
                    stoklist = DataReaderMapToList<DetayliSipModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return stoklist;
        }
            [HttpGet("{sipno}", Name = "GetSipByNo")]
        public IEnumerable GetBySipNo(string sipno)
        {
            string query1 = @"EXEC URETMSIP_DETAY '" + sipno +"'";
            List<DetayliSipModel> stoklist = null;
            string sqldataSource1 = _configuration.GetConnectionString("Conn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    stoklist = DataReaderMapToList<DetayliSipModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            
            return stoklist;
        }
        [HttpGet("E/{sipno}")]
        public JsonResult GetE(string sipno)
        {
            DataTable table = new DataTable();


            string query = @"EXEC EFECE2023..URETMSIP_DETAY_E '" + sipno + "'";

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
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        [HttpGet("acik", Name = "GetAcik")]
        public IActionResult GetAcik()
        {
            var item = _context.NOVA_VW_DETAYLI_URETMSIP.OrderBy(x=>x.TARIH).Take(100);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

    }
}

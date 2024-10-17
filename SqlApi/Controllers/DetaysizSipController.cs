using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetaysizSipController : ControllerBase
    {
        private readonly StokContext _context;

        private readonly IConfiguration _configuration;
        public DetaysizSipController(StokContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IEnumerable> GetAll()
        {
            return await _context.NOVA_VW_DETAYSIZ_MSIP.ToListAsync();
        }
        [HttpGet("E/{tip}")]
        public IEnumerable GetDetaysiz(string tip)
        {
            try
            {
                string query1 = @"EXEC NOVA_DETAYSIZ_MSIP_E '"+tip+"'";
                List<DetayliSipIhracatDetaysizModel> stoklist = null;
                string sqldataSource1 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader1;
                using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                {
                    mycon1.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                    {
                        sqlreader1 = myCommand1.ExecuteReader();
                        stoklist = DataReaderMapToList<DetayliSipIhracatDetaysizModel>(sqlreader1);
                        sqlreader1.Close();
                        mycon1.Close();
                    }
                }
                return stoklist;
            }
            catch (Exception exp)
            {
                var e = exp.Message;
                return e;
            }
           

            
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
    }
}

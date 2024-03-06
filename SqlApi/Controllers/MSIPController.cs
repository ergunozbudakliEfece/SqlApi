using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MSIPController : ControllerBase
    {
        private readonly FavContext _context;
        private readonly StokContext _context2023;
        private readonly IConfiguration _configuration;
        public MSIPController(FavContext context,IConfiguration configuration, StokContext context2023)
        {
            _context = context;
            _configuration = configuration;
            _context2023 = context2023;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_ACIK_MSIP_NO.ToList();
        }
        [HttpGet("TEST")]
        public IEnumerable GetA()
        {
            string query1 = @"SELECT * FROM NOVA_VW_ACIK_MSIP_NO";
            List<MSIPAcik> stoklist = null;
            string sqldataSource1 = _configuration.GetConnectionString("Conn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    stoklist = DataReaderMapToList<MSIPAcik>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return stoklist;
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
        [HttpGet("seri")]
        public IEnumerable GetSeri()
        {
            return _context2023.NOVA_VW_CIKTI_SERI_URETME.ToList();
        }
    }
}

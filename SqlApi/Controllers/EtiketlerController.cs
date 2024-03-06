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
using System.Reflection;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtiketlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EtiketlerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{hatkodu}")]
        public JsonResult GetAll(string hatkodu)
        {
            DataTable etiketList = new DataTable(); 
            string query;
            if(hatkodu=="0")
                query = @"EXEC SP_ETIKETLER";
            else
                query= @"EXEC SP_ETIKETLER '"+hatkodu+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    etiketList.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(etiketList);
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
        //public class EtiketModal
        //{
        //    public string? SERI_NO { get; set; }
        //    public string? STOK_KODU { get; set; }
        //    public string? STOK_ADI { get; set; }
        //    public string? MENSEI { get; set; }
        //    public string? MIKTAR { get; set; }
        //    public string? MIKTAR2 { get; set; }
        //    /*public string? ACIK1 { get; set; }
        //    public string? ACIK2 { get; set; }
        //    public string? ACIK3 { get; set; }
        //    public string? ACIKLAMA_4 { get; set; }
        //    public string? ACIKLAMA_5 { get; set; }
        //    public string? SERI_NO_3 { get; set; }
        //    public string? SERI_NO_4 { get; set; }
        //    public string? TARIH { get; set; }
        //    public string? SAAT { get; set; }
        //    public string? URETILECEK_URUN { get; set; }*/
        //}
    }
}

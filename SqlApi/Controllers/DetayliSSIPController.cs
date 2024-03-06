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

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetayliSSIPController : ControllerBase
    {
        private readonly StokContext _context;
        private readonly IConfiguration _configuration;
       
        public DetayliSSIPController(StokContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            
            
           
            
            return _context.NOVA_VW_DETAYLI_SSIP.ToList();

        }
        [HttpGet("acik")]
        public JsonResult GetAciklar()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_NOVA_DETAYLI_SSIP";

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
        [HttpGet("kapali")]
        public JsonResult GetKapali()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_NOVA_DETAYLI_SSIP 'K'";

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
        [HttpGet("{sip}", Name = "GetBySipDurumu")]
        public IActionResult GetById(string sip)
        {
            var item = _context.NOVA_VW_DETAYLI_SSIP.Where(t => t.SIP_DURUMU == sip);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}

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
    public class RolesController : Controller
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public RolesController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_ROLES.ToList();
        }

        [HttpGet("{id:int}", Name = "GetRole")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_ROLES.FirstOrDefault(t => t.ROLE_ID == id.ToString());
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("names")]
        public IActionResult GetRoleNames()
        {
            DataTable table = new DataTable();


            string query = @"SELECT ID,NAME FROM TBL_ROLES_DETAY";

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
        [HttpGet("yetkiler")]
        public IActionResult GetRoleYetki()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_YETKI_ROLES_TABLOSU";

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
        [HttpGet("Ekle/{id}/{moduleid}")]
        public string GetEkle(int id, int moduleid)
        {
            try
            {


                string query = @"INSERT INTO TBL_ROLE_YETKI  VALUES(" + id + "," + moduleid + ")";

                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }

            }
            catch (System.Exception e)
            {
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpGet("Sil/{id}/{moduleid}")]
        public string GetSil(int id, int moduleid)
        {
            try
            {


                string query = @"DELETE FROM TBL_ROLE_YETKI WHERE ROLE_ID= " + id + " AND MODULE_ID=" + moduleid;

                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }

            }
            catch (System.Exception)
            {

                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpPost]
        public IActionResult Create([FromBody] Roles item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_ROLES.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = item.ROLE_ID }, item);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Roles item)
        {
            if (item == null || item.ROLE_ID != id.ToString())
            {
                return BadRequest();
            }

            var role = _context.TBL_ROLES.FirstOrDefault(t => t.ROLE_ID == id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            role.ROLE_NAME = item.ROLE_NAME;
            role.ROLE_ID = item.ROLE_ID;
            


            _context.TBL_ROLES.Update(role);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpGet("ekle/{rolName}")]
        public string Update(string rolName)
        {
            try
            {


                string query = @"INSERT INTO TBL_ROLES_DETAY VALUES('"+rolName+"')";

                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }

            }
            catch (System.Exception e)
            {
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
        [HttpGet("sil/{id}")]
        public string Sil(int id)
        {
            try
            {


                string query = @"DELETE FROM TBL_ROLES_DETAY WHERE ID="+id;

                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }

            }
            catch (System.Exception e)
            {
                var m = e.Message;
                return "BAŞARISIZ";
            }
            return "BAŞARILI";
        }
    }
}

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
    public class IsEmriController : ControllerBase
    {
        private readonly FavContext _context;
        private readonly StokContext _context2;
        private readonly IConfiguration _configuration;
        public IsEmriController(FavContext context, StokContext context2, IConfiguration configuration)
        {
            _context = context;
            _context2 = context2;
            _configuration = configuration;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_SERINO_ISEMRI_TAKIP.ToList();
        }
        [HttpGet("VW")]
        public IEnumerable GetAllVW()
        {
            return _context2.NOVA_VW_ISEMRI.ToList();
        }
        [HttpGet("{seri}", Name = "GetIsEmriByNo")]
        public IActionResult GetBySube(string seri)
        {
            var item = _context.NOVA_VW_SERINO_ISEMRI_TAKIP.Where(t => t.HAMM_SERI_NO == seri);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("max")]
        public JsonResult GetMAX()
        {
                DataTable table = new DataTable();
                string query = @"EXEC SP_NOVA_SIRANO";
             
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LocationController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public LocationController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       
        [HttpGet("Cities")]
        public async Task<IEnumerable> GetCitiesAsync()
        {
            return await _context.TBL_CITIES.ToListAsync();
        }
        [HttpGet("Districts")]
        public async Task<IEnumerable> GetDistrictsAsync()
        {
            return await _context.TBL_DISTRICTS.ToListAsync();
        }
       
        [HttpGet("Mahalle/{semtadi}")]
        public JsonResult GetGirisCikis(int semtadi)
        {

            DataTable table = new DataTable();


            string query = @"EXEC GetMahalle "+ semtadi;

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

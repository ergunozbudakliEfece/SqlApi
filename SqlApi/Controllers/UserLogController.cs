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
    public class UserLogController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public UserLogController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
       

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_USER_LOG.ToList();
        }

        [HttpGet("{id:int}", Name = "GetLog")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_USER_LOG.OrderByDescending(t=>t.INCKEY).FirstOrDefault(t=>t.USER_ID==id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);

            
        }
       
        [HttpPost]
        public IActionResult Create([FromBody] Log item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_USER_LOG.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLog", new { id = item.USER_ID }, item);
        }
       
        [HttpPut("{date}")]
        public IActionResult Update(string date, [FromBody] Log item)
        {
            if (item == null || item.ACTIVITY_START != date)
            {
                return BadRequest();
            }

            var user = _context.TBL_USER_LOG.FirstOrDefault(t => t.ACTIVITY_START == date);
            if (user == null)
            {
                return NotFound();
            }

            user.ACTIVITY_END = item.ACTIVITY_END;
            


            _context.TBL_USER_LOG.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }


    }
}

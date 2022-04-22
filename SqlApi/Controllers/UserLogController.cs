using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogController : ControllerBase
    {
        private readonly UserContext _context;

        public UserLogController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_USER_LOG.ToList();
        }

        [HttpGet("{id:int}", Name = "GetLog")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_USER_LOG.FirstOrDefault(t => t.USER_ID == id.ToString());
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

        
    }
}

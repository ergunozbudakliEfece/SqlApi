using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly UserContext _context;

        public RolesController(UserContext context)
        {
            _context = context;
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
    }
}

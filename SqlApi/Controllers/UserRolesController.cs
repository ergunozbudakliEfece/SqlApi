using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserRolesController : Controller
    {
        private readonly UserRoleContext _context;

        public UserRolesController(UserRoleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_USER_AUTH.ToList();
        }

        [HttpGet("{id}", Name = "GetUserRoles")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_USER_AUTH.FirstOrDefault(t => t.USER_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] UserRole item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_USER_AUTH.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = item.USER_ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserRole item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var userrole = _context.TBL_USER_AUTH.FirstOrDefault(t => t.USER_ID == id);
            if (userrole == null)
            {
                return NotFound();
            }

            userrole.ROLE_ID = item.ROLE_ID;
            userrole.MODULE_ID = item.MODULE_ID;
            userrole.PROGRAM_ID = item.PROGRAM_ID;
            userrole.USER_AUTH = item.USER_AUTH;
            userrole.SELECT_AUTH = item.SELECT_AUTH;
            userrole.INSERT_AUTH = item.INSERT_AUTH;
            userrole.UPDATE_AUTH = item.UPDATE_AUTH;
            userrole.DELETE_AUTH = item.DELETE_AUTH;
            userrole.LOG_USER_ID = item.LOG_USER_ID;
            userrole.ACTIVITY_START = item.ACTIVITY_START;
            userrole.LAST_UPD_USER_ID = item.LAST_UPD_USER_ID;
            userrole.LAST_UPD_DATE = item.LAST_UPD_DATE;


            _context.TBL_USER_AUTH.Update(userrole);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

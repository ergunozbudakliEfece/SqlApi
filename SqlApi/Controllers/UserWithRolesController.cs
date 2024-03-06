using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserWithRolesController : Controller
    {
        private readonly UserContext _context;

        public UserWithRolesController(UserContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.VW_USER_WITH_ROLES.ToList();
        }
        
        [HttpGet("{id:int}", Name = "GetUserwithRolesById")]
        public IActionResult GetById(int id)
        {
            var item = _context.VW_USER_WITH_ROLES.Where(t => t.USER_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpGet("{id}/{inckey}", Name = "GetUserwithRolesByIncandId")]
        public IActionResult GetByIdandINC(int id,int inckey)
        {
            var item = _context.VW_USER_WITH_ROLES.Where(t => t.USER_ID == id && t.MODULE_INCKEY==inckey);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpGet("inc:{inckey}", Name = "GetUserwithRolesByInc")]
        public IActionResult GetByINC(int inckey)
        {
            var item = _context.VW_USER_WITH_ROLES.Where(t => t.MODULE_INCKEY == inckey);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpGet("name:{name}", Name = "GetUserwithRolesByName")]
        public IActionResult GetByName(string name)
        {
            var item = _context.VW_USER_WITH_ROLES.Where(t => t.USER_NAME == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        
        [HttpPut("{id}/{inckey}")]
        public IActionResult Update(int id,int inckey, [FromBody] UserWithRoles item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var user = _context.VW_USER_WITH_ROLES.FirstOrDefault((t) =>  t.USER_ID == id && t.MODULE_INCKEY == inckey );
            if (user == null)
            {
                return NotFound();
            }
            user.USER_ID = item.USER_ID;
            user.USER_NAME = item.USER_NAME;
            user.USER_PASSWORD = item.USER_PASSWORD;
            user.USER_FIRSTNAME = item.USER_FIRSTNAME;
            user.USER_LASTNAME = item.USER_LASTNAME;
            user.USER_ROLE = item.USER_ROLE;
            user.ACTIVE = item.ACTIVE;
            user.USER_MAIL = item.USER_MAIL;
            user.USER_AUTH = item.USER_AUTH;
            user.SELECT_AUTH = item.SELECT_AUTH;
            user.INSERT_AUTH = item.INSERT_AUTH;
            user.UPDATE_AUTH = item.UPDATE_AUTH;
            user.DELETE_AUTH = item.DELETE_AUTH;
            user.MODULE_INCKEY = item.MODULE_INCKEY;
            _context.VW_USER_WITH_ROLES.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

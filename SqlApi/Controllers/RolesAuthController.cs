using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesAuthController : ControllerBase
    {
        private readonly UserContext _context;

        public RolesAuthController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_ROLES_AUTH.ToList();
        }
        [HttpGet("{id:int}", Name = "GetRolesAuthById")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_ROLES_AUTH.Where(t => t.ROLE_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        

        [HttpPut("{id}/{inckey}")]
        public IActionResult Update(int id,int inckey, [FromBody] RolesAuth item)
        {
            if (item == null || item.ROLE_ID != id)
            {
                return BadRequest();
            }

            var roles = _context.TBL_ROLES_AUTH.FirstOrDefault(t => t.MODULE_INCKEY == inckey && t.ROLE_ID==id);
            if (roles == null)
            {
                return NotFound();
            }
            roles.USER_AUTH = item.USER_AUTH;
            roles.SELECT_AUTH = item.SELECT_AUTH;
            roles.INSERT_AUTH = item.INSERT_AUTH;
            roles.UPDATE_AUTH = item.UPDATE_AUTH;
            roles.DELETE_AUTH = item.DELETE_AUTH;
            _context.TBL_ROLES_AUTH.Update(roles);
            _context.SaveChanges();
            return new NoContentResult();
        }
       
    }
}

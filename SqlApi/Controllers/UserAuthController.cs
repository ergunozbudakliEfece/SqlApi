using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserContext _context;

        public UserAuthController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_ROLES_AUTH.ToList();
        }
        [HttpGet("{id}/{inckey}", Name = "GetUserAuthByIncandId")]
        public IActionResult GetByIdandINC(int id, int inckey)
        {
            var item = _context.TBL_USER_AUTH.Where(t => t.USER_ID == id && t.MODULE_INCKEY == inckey);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        



        [HttpPut("{id}/{inckey}")]
        public IActionResult Update(int id,int inckey, [FromBody] UserAuth item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var auth = _context.TBL_USER_AUTH.FirstOrDefault(t => t.MODULE_INCKEY == inckey && t.USER_ID==id);
            if (auth == null)
            {
                return NotFound();
            }
            auth.USER_AUTH = item.USER_AUTH;
            auth.SELECT_AUTH = item.SELECT_AUTH;
            auth.INSERT_AUTH = item.INSERT_AUTH;
            auth.UPDATE_AUTH = item.UPDATE_AUTH;
            auth.DELETE_AUTH = item.DELETE_AUTH;
            _context.TBL_USER_AUTH.Update(auth);
            _context.SaveChanges();
            return new NoContentResult();
        }
       
    }
}

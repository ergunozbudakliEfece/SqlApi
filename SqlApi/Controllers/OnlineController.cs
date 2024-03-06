using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    
    [Route("api/[controller]")]
    public class OnlineController : Controller
    {
        private readonly UserContext _context;

        public OnlineController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_USER_STATUS.ToList();
        }
        [HttpGet("time")]
        public DateTime Get()
        {
            return DateTime.Now;
        }


        [HttpGet("{status}", Name = "GetOnlineUsers")]
        public IActionResult GetById(string status)
        {
            var item = _context.TBL_USER_STATUS.Where(t => t.ONLINE_STATUS == status);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Online item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var user = _context.TBL_USER_STATUS.FirstOrDefault(t => t.USER_ID == id);
            if (user == null)
            {
                return NotFound();
            }
            user.USER_ID = item.USER_ID;
            user.ONLINE_STATUS = item.ONLINE_STATUS;
            user.USER_NAME = item.USER_NAME;


            _context.TBL_USER_STATUS.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

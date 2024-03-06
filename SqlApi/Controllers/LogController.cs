using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqlApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public LogController(UserContext context)
        {
            _context = context;
           
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.VW_SESSION.ToList();
        }

        [HttpGet("max:{id}", Name = "GetUserLog")]
        public IActionResult GetById(int id)
        {
            var item = _context.VW_SESSION.Where(t => t.USER_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("logout", Name = "GetLogOut")]
        public IActionResult GetLogout()
        {
            var item = _context.VW_SESSION.Where(t => t.ACTIVITY_TYPE == "logout");
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("login", Name = "GetLogIn")]
        public IActionResult GetLogin()
        {
            var item = _context.VW_SESSION.Where(t => t.ACTIVITY_TYPE == "login");
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody] LogInTBL item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_USER_SESSION_LOG.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUserLog", new { id = item.USER_ID }, item);
        }

       
    }
}

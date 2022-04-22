﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_USER_DATA.ToList();
        }

        [HttpGet("{id:int}",Name = "GetUserById")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("{name}", Name = "GetUserByName")]
        public IActionResult GetByName(string name)
        {
            var item = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_NAME == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_USER_DATA.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUserByName", new { name = item.USER_NAME }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var user = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_ID == id);
            if (user == null)
            {
                return NotFound();
            }

            user.USER_NAME = item.USER_NAME;
            user.USER_PASSWORD = item.USER_PASSWORD;
            user.USER_ROLE = item.USER_ROLE;
            user.ACTIVE = item.ACTIVE;
            user.USER_MAIL = item.USER_MAIL;
            

            _context.TBL_USER_DATA.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

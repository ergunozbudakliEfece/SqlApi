using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly UserContext _context;

        public ModulesController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_MODULES.ToList();
        }

        [HttpGet("{id:int}", Name = "GetModulesById")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_MODULES.FirstOrDefault(t => t.INCKEY == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("{name}", Name = "GetModuleByName")]
        public IActionResult GetByName(string name)
        {
            var item = _context.TBL_MODULES.FirstOrDefault(t => t.MODULE_NAME == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Module item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_MODULES.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetModuleByName", new { name = item.MODULE_NAME }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Module item)
        {
            if (item == null || item.INCKEY != id)
            {
                return BadRequest();
            }

            var module = _context.TBL_MODULES.FirstOrDefault(t => t.INCKEY == id);
            if (module == null)
            {
                return NotFound();
            }
            module.MODULE_ID = item.MODULE_ID;
            module.MODULE_NAME = item.MODULE_NAME;
            module.PROGRAM_ID = item.PROGRAM_ID;
            module.PROGRAM_NAME = item.PROGRAM_NAME;
            _context.TBL_MODULES.Update(module);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

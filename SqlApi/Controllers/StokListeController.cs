using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StokListeController : Controller
    {
        private readonly StokContext _context;

        public StokListeController(StokContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            
            return _context.NOVA_VW_STOK_BAKIYE.ToArray();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var item = _context.NOVA_VW_STOK_BAKIYE.Where(t => t.GRUP_KODU == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }




    }
}

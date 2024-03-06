using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarkodController : ControllerBase
    {
        private readonly StokContext _context;

        public BarkodController(StokContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.UUR_VW_URETIM_ETIKETTR.ToList();
        }
        [HttpGet("{id}", Name = "GetBySeri")]
        public IActionResult GetById(string id)
        {
            var item = _context.UUR_VW_URETIM_ETIKETTR.Where(t => t.SERI_NO == id && t.STHAR_GCKOD == "C" && t.STHAR_BGTIP == "V");
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}

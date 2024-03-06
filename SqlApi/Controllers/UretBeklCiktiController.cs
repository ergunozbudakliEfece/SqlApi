using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretBeklCiktiController : ControllerBase
    {
        private readonly FavContext _context;
        public UretBeklCiktiController(FavContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_URET_BEKL_CIKTI.ToList();
        }
        [HttpGet("{mak}", Name = "GetByyMak")]
        public IActionResult GetById(string mak)
        {
            var item = _context.NOVA_VW_URET_BEKL_CIKTI.Where(t => t.MAK_KODU == mak);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("{mak}/{stokadi}", Name = "bystokadi")]
        public IActionResult GetById(string mak, string stokadi)
        {
            var item = _context.NOVA_VW_URET_BEKL_CIKTI.Where(t => t.MAK_KODU == mak && t.CIKTI_STOK_ADI == stokadi);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("vw")]
        public IActionResult GetVw()
        {
            
            return new ObjectResult(_context.NOVA_VW_URETIM_BEKL_CIKTI_BAKIYE);
        }
    }
}

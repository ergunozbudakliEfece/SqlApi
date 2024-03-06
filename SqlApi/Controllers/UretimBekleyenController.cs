using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimBekleyenController : ControllerBase
    {
        private readonly FavContext _context;
        public UretimBekleyenController(FavContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_URET_BEKL_GIRDI.ToList();
        }
        [HttpGet("{mak}", Name = "GetIsEmriByMak")]
        public IActionResult GetById(string mak)
        {
            var item = _context.NOVA_VW_URET_BEKL_GIRDI.Where(t => t.MAK_KODU == mak);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("{mak}/{stokadi}", Name = "stokkodu")]
        public IActionResult GetById(string mak,string stokadi)
        {
            var item = _context.NOVA_VW_URET_BEKL_GIRDI.Where(t => t.MAK_KODU == mak && t.CIKTI_STOK_ADI==stokadi);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("vw")]
        public IActionResult GetGirdi()
        {
            var item = _context.NOVA_VW_URETIM_BEKL_GIRDI_BAKIYE;
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}

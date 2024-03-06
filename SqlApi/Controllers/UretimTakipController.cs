using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimTakipController : ControllerBase
    {
        private readonly FavContext _context;
        public UretimTakipController(FavContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_URETIM_SUREC_TAKIP.ToList();
        }
        [HttpGet("{seri}", Name = "GetItemBySeri")]
        public IActionResult GetBySube(string seri)
        {
            var item = _context.NOVA_VW_URETIM_SUREC_TAKIP.Where(t => t.SERI_NO == seri);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
       
    }
}

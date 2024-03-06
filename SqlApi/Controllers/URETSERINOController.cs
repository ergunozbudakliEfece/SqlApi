using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URETSERINOController : ControllerBase
    {
        private readonly StokContext _context;
        public URETSERINOController(StokContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_SERI_BILGI.ToList();

        }
        [HttpGet("{sip}", Name = "GetSeriNo")]
        public IActionResult GetById(string sip)
        {
            var item = _context.NOVA_VW_SERI_BILGI.Where(t => t.SERI_NO.Substring(0,5) == sip);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}

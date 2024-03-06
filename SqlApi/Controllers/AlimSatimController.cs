using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimSatimController : ControllerBase
    {
        private readonly StokContext _context;
        public AlimSatimController(StokContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_ALIM_SATIM.ToList();
        }
    }
}

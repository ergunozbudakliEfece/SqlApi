using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetaysizSSIPController : ControllerBase
    {
        // GET: api/<DetaysizSSIPController>
        private readonly StokContext _context;
        public DetaysizSSIPController(StokContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.NOVA_VW_DETAYSIZ_SSIP.ToList();

        }

    }
}

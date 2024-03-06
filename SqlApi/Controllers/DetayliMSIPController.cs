using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetayliMSIPController : ControllerBase
    {
        private readonly StokContext _context;
        public DetayliMSIPController(StokContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            
            
            return _context.NOVA_VW_DETAYLI_MSIP.ToList();
        }
    }
}

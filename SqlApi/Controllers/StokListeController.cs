using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]

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
            
            return _context.BTE_VW_STOK_BAKIYE.ToArray();
        }

        

        
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimGirdiController : ControllerBase
    {
        private readonly UserContext _context;
        public UretimGirdiController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_NOVA_URETIM_GIRDI_KAYIT.ToList();
        }
        [HttpPost]
        public IActionResult Create([FromBody] UretimGirdiModel uretim)
        {
            if (uretim == null)
            {
                return BadRequest();
            }
            UretimGirdiModel model = new UretimGirdiModel();
            model.MAK_KODU=uretim.MAK_KODU;
            model.URETIM_KAYIT_ID = uretim.URETIM_KAYIT_ID;
            model.GIRDI_SERI_NO = uretim.GIRDI_SERI_NO;
            model.GIRDI_MIKTAR2 = uretim.GIRDI_MIKTAR2;

            _context.TBL_NOVA_URETIM_GIRDI_KAYIT.Add(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetAll",model);
        }
        [HttpPut("{inckey}")]
        public IActionResult Update(int inckey, [FromBody] UretimGirdiModel item)
        {
            if (item == null || item.INCKEY != inckey)
            {
                return BadRequest();
            }

            var item2 = _context.TBL_NOVA_URETIM_GIRDI_KAYIT.FirstOrDefault(t => t.INCKEY == inckey);


            item2.URETILDIMI = item.URETILDIMI;
            item2.URETIM_ONAY_ID = item.URETIM_ONAY_ID;
            
           
            
           
            _context.TBL_NOVA_URETIM_GIRDI_KAYIT.Update(item2);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

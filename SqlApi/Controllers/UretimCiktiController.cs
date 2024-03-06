using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimCiktiController : ControllerBase
    {
        private readonly UserContext _context;
        public UretimCiktiController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_NOVA_URETIM_CIKTI_KAYIT.ToList();
        }
        [HttpPost]
        public IActionResult Create([FromBody] UretimCiktiModel uretim)
        {
            if (uretim == null)
            {
                return BadRequest();
            }
            UretimCiktiModel model = new UretimCiktiModel();
            model.MAK_KODU = uretim.MAK_KODU;
            model.URETIM_KAYIT_ID = uretim.URETIM_KAYIT_ID;
            model.CIKTI_MIKTAR = uretim.CIKTI_MIKTAR;
            model.CIKTI_MIKTAR2 = uretim.CIKTI_MIKTAR2;
            model.CIKTI_STOK_KODU = uretim.CIKTI_STOK_KODU;

            _context.TBL_NOVA_URETIM_CIKTI_KAYIT.Add(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetAll", model);
        }
        [HttpPut("{inckey}")]
        public IActionResult Update(int inckey, [FromBody] UretimBekleyenCikti item)
        {
            if (item == null || item.INCKEY != inckey)
            {
                return BadRequest();
            }

            var item2 = _context.TBL_NOVA_URETIM_CIKTI_KAYIT.FirstOrDefault(t => t.INCKEY == inckey);


            item2.URETILDIMI = item.URETILDIMI;
            item2.URETIM_ONAY_ID = item.URETIM_ONAY_ID;




            _context.TBL_NOVA_URETIM_CIKTI_KAYIT.Update(item2);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

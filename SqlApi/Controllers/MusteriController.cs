using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MusteriController : Controller
    {
        private readonly UserContext _context;
        private readonly FavContext _context2;
        private readonly StokContext _context3;
        private readonly IConfiguration _configuration;
        public MusteriController(UserContext context, FavContext context2, StokContext context3)
        {
            _context = context;
            _context2 = context2;
            _context3 = context3;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable GetAll()
        {
            return _context.TBL_MUSTERI.ToList();
        }
        [HttpGet("plasiyerrandevu/{plasiyer}")]
        public IEnumerable GetRandevuS(int plasiyer)
        {
            return _context.TBL_MUSTERI_RANDEVU.Where(x=>x.KAYIT_YAPAN_KULLANICI_ID==plasiyer).ToList();
        }
        [HttpGet("musteriplasiyer/{plasiyer}")]
        public IEnumerable GetMusvuS(string plasiyer)
        {
            return _context.TBL_MUSTERI.Where(x => x.PLASIYER == plasiyer).ToList();
        }
        [HttpGet("urun")]
        [Authorize]
        public IEnumerable GetUrun()
        {
            return _context.TBL_MUSTERI_URUN_BILGI.ToList();
        }
        [HttpGet("urungruplari")]
        public IEnumerable GetUrunGruplari()
        {
            return _context.TBL_URUN_GRUBU.ToList();
        }
        [HttpGet("randevu")]
        public IEnumerable GetRandevu()
        {
            return _context.TBL_MUSTERI_RANDEVU.ToList();
        }
        [HttpGet("randevu/son3")]
        public IEnumerable GetRandevuSon3()
        {
            return _context.TBL_MUSTERI_RANDEVU.Where(x => x.KAYIT_TARIHI < DateTime.Now.AddMonths(-3)).ToList();
        }
        [HttpGet("cariler")]
        public IEnumerable GetCariler()
        {
            return _context3.TBLCASABIT.ToList();
        }
        [HttpGet("stoklar")]
        public IEnumerable GetStoklar()
        {
            return _context2.TBLSTSABIT.ToList();
        }
        [HttpGet("ozelkod1")]
        public IEnumerable GetOZelkod1()
        {
            return _context2.TBLOZELKOD1.ToList();
        }
        [HttpGet("ozelkod2")]
        public IEnumerable GetOZelkod2()
        {
            return _context2.TBLOZELKOD2.ToList();
        }
        [HttpGet("teklif/{teklifno}")]
        public IEnumerable GetTeklif(string teklifno)
        {
            return _context2.TBLTEKLIFTRA.Where(x=>x.FISNO==teklifno).ToList();
        }
        [HttpGet("teklifmas/{teklifno}")]
        public TeklifMasModel GetTeklifMas(string teklifno)
        {
            
            return _context2.TBLTEKLIFMAS.FirstOrDefault(x => x.FATIRS_NO == teklifno);
        }
        [HttpGet("fatuek/{teklifno}")]
        public FatuEkModel GetTeklifler(string teklifno)
        {

            return _context2.TBLFATUEK.FirstOrDefault(x => x.FATIRSNO == teklifno);
        }
        [HttpGet("randevu/{inckey}")]
        public IEnumerable GetRandevuByInckey(int inckey)
        {
            return _context.TBL_MUSTERI_RANDEVU.Where(x=>x.INCKEY==inckey).ToList();
        }
        [HttpPost("randevu")]
        public async Task<IActionResult> CreateRandevu([FromBody] MusteriRandevu musteri)
        {
            if (musteri == null)
            {
                return BadRequest();
            }

            _context.TBL_MUSTERI_RANDEVU.Add(musteri);
            _context.SaveChanges();

            return new NoContentResult();
        }
        [HttpPut("randevu")]
        public IActionResult UpdateRandevu([FromBody] MusteriRandevu item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var module = _context.TBL_MUSTERI_RANDEVU.FirstOrDefault(t => t.INCKEY == item.INCKEY);
            if (module == null)
            {
                return NotFound();
            }
            module.PLANLANAN_TARIH = item.PLANLANAN_TARIH;
            module.RANDEVU_NOTU = item.RANDEVU_NOTU;
            module.DEGISIKLIK_YAPAN_KULLANICI_ID = item.DEGISIKLIK_YAPAN_KULLANICI_ID;
            module.GERCEKLESEN_TARIH = item.GERCEKLESEN_TARIH;
            _context.TBL_MUSTERI_RANDEVU.Update(module); 
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("randevu")]
        public IActionResult DeleteRandevu([FromBody] MusteriRandevu item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var module = _context.TBL_MUSTERI_RANDEVU.FirstOrDefault(t => t.INCKEY == item.INCKEY);
            if (module == null)
            {
                return NotFound();
            }

            _context.TBL_MUSTERI_RANDEVU.Remove(module);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("sil/{id}")]
        public IActionResult DeleteMusteri(int id)
        {
           

            var module = _context.TBL_MUSTERI.FirstOrDefault(t => t.MUSTERI_ID == id);
            if (module == null)
            {
                return NotFound();
            }

            _context.TBL_MUSTERI.Remove(module);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPut("randevu/{id}")]
        public IActionResult UpdateRandevu(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var module = _context.TBL_MUSTERI_RANDEVU.FirstOrDefault(t => t.INCKEY == id);
            if (module == null)
            {
                return NotFound();
            }
            module.GERCEKLESEN_TARIH = DateTime.Now;
            _context.TBL_MUSTERI_RANDEVU.Update(module);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPost]
        public IActionResult CreateMusteri([FromBody] MusteriModel musteri)
        {
            if (musteri == null)
            {
                return BadRequest();
            }

            _context.TBL_MUSTERI.Add(musteri);
            _context.SaveChanges();

            return null;
        }
        [HttpPut]
        public IActionResult UpdateMusteri([FromBody] MusteriModel musteri)
        {
            if (musteri == null)
            {
                return BadRequest();
            }

            var module = _context.TBL_MUSTERI.FirstOrDefault(t => t.MUSTERI_ID == musteri.MUSTERI_ID);
            if (module == null)
            {
                return NotFound();
            }
            module.MUSTERI_IL = musteri.MUSTERI_IL;
            module.MUSTERI_ILCE = musteri.MUSTERI_ILCE;
            module.MUSTERI_MAHALLE = musteri.MUSTERI_MAHALLE;
            module.MUSTERI_ADRES = musteri.MUSTERI_ADRES;
            module.FIRMA_YETKILISI = musteri.FIRMA_YETKILISI;
            module.MUSTERI_TEL1 = musteri.MUSTERI_TEL1;
            module.MUSTERI_TEL2 = musteri.MUSTERI_TEL2;
            module.MUSTERI_ID = musteri.MUSTERI_ID;
            module.MUSTERI_MAIL = musteri.MUSTERI_MAIL;
            module.MUSTERI_SEKTOR = musteri.MUSTERI_SEKTOR;
            module.MUSTERI_SEKTOR_DIGER = musteri.MUSTERI_SEKTOR_DIGER;
            module.MUSTERI_NITELIK = musteri.MUSTERI_NITELIK;
            module.MUSTERI_NITELIK_DIGER = musteri.MUSTERI_NITELIK_DIGER;
            module.MUSTERI_NOTU = musteri.MUSTERI_NOTU;
            module.PLASIYER = musteri.PLASIYER;
            module.DUZELTME_YAPAN_KULLANICI = musteri.DUZELTME_YAPAN_KULLANICI;
            _context.TBL_MUSTERI.Update(module);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPost("urun")]
        public IActionResult CreateUrun([FromBody] MusteriUrunModel urun)
        {
            if (urun == null)
            {
                return BadRequest();
            }

            _context.TBL_MUSTERI_URUN_BILGI.Add(urun);
            _context.SaveChanges();

            return new NoContentResult();
        }
        [HttpPut("guncelle")]
        public IActionResult UpdateUrun([FromBody] MusteriUrunModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var module = _context.TBL_MUSTERI_URUN_BILGI.FirstOrDefault(t => t.MUSTERI_ID == item.MUSTERI_ID && t.URUN_ID==item.URUN_ID);
            if (module == null)
            {
                return NotFound();
            }
            module.URUN_GRUBU = item.URUN_GRUBU;
            module.OLCU_BIRIMI = item.OLCU_BIRIMI;
            module.MUSTERI_ID = item.MUSTERI_ID;
            module.YILLIK_KULLANIM = item.YILLIK_KULLANIM;
            module.DEGISIKLIK_YAPAN_KULLANICI = item.DEGISIKLIK_YAPAN_KULLANICI;
            _context.TBL_MUSTERI_URUN_BILGI.Update(module);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("sil/{id}/{urunid}")]
        public IActionResult DeleteUrun(int id,int urunid)
        {
            if (urunid == 0 || id == 0)
            {
                return BadRequest();
            }

            var urunbilgisi = _context.TBL_MUSTERI_URUN_BILGI.FirstOrDefault(t => t.MUSTERI_ID == id && t.URUN_ID==urunid);
            if (urunbilgisi == null)
            {
                return NotFound();
            }
           
            _context.TBL_MUSTERI_URUN_BILGI.Remove(urunbilgisi);
            _context.SaveChanges();
            return new NoContentResult();
        }


    }
}

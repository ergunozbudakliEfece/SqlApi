using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.Collections;
using System.Linq;

namespace SqlApi.Controllers
{
    
    [Route("api/[controller]")]
    public class PersonelController : Controller
    {
        private readonly UserContext _context;

        public PersonelController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_PERSONAL_DATA.Where(x=>x.ISIM!="Admin"&& x.ISIM != "Test").ToList();
        }
        [HttpGet("{name}", Name = "GetPersonelByName")]
        public IActionResult GetByName(string name)
        {
            var item = _context.TBL_PERSONAL_DATA.Where(t => t.ISIM == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("id:{id}", Name = "GetPersonelById")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_PERSONAL_DATA.Where(t => t.USER_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("sube:{sube}", Name = "GetPersonelBySube")]
        public IActionResult GetBySube(string sube)
        {
            var item = _context.TBL_PERSONAL_DATA.Where(t => t.SUBE == sube);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Personel personel)
        {
            if (personel == null)
            {
                return BadRequest();
            }

            _context.TBL_PERSONAL_DATA.Add(personel);
            _context.SaveChanges();

            return CreatedAtRoute("GetPersonelByName", new { id = personel.ISIM }, personel);
        }
        [HttpDelete("{name}/{lastname}")]
        public IActionResult Delete(string name, string lastname, [FromBody] Personel personel)
        {
            if (personel == null || personel.ISIM != name)
            {
                return BadRequest();
            }

            var user = _context.TBL_PERSONAL_DATA.FirstOrDefault(t => t.ISIM == name && t.SOYISIM==lastname);
            if (user == null)
            {
                return NotFound();
            }
            user.ISIM = personel.ISIM;
            user.SOYISIM = personel.SOYISIM;



            _context.TBL_PERSONAL_DATA.Remove(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Personel item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var personel = _context.TBL_PERSONAL_DATA.FirstOrDefault(t => t.USER_ID == id);
            if (personel == null)
            {
                return NotFound();
            }
            personel.SUBE = item.SUBE;
            personel.TCKN = item.TCKN;
            if (item.DOGUM_TARIHI != null) { personel.DOGUM_TARIHI = item.DOGUM_TARIHI.Value.AddDays(1); } 
            else
            {
                personel.DOGUM_TARIHI = null;
            }
            
            personel.DOGUM_YERI_IL = item.DOGUM_YERI_IL;
            personel.DOGUM_YERI_ILCE = item.DOGUM_YERI_ILCE;
            personel.OGRENIM_DURUMU = item.OGRENIM_DURUMU;
            personel.MEZUN_OKUL = item.MEZUN_OKUL;
            personel.MEZUN_BOLUM = item.MEZUN_BOLUM;
            personel.MEZUN_YIL = item.MEZUN_YIL;
            personel.IKAMETGAH_ADRES = item.IKAMETGAH_ADRES;
            personel.IKAMETGAH_IL = item.IKAMETGAH_IL;
            personel.IKAMETGAH_ILCE = item.IKAMETGAH_ILCE;
            personel.MEDENI_HAL = item.MEDENI_HAL;
            personel.ES_CALISMA_DURUMU = item.ES_CALISMA_DURUMU;
            personel.ES_CALISMA_FIRMA = item.ES_CALISMA_FIRMA;
            personel.ES_UNVANI = item.ES_UNVANI;
            personel.COCUK_SAYI = item.COCUK_SAYI;
            personel.IKAMET_EV_DURUM = item.IKAMET_EV_DURUM;
            personel.ARAC_DURUM = item.ARAC_DURUM;
            personel.ARAC_PLAKA = item.ARAC_PLAKA;
            personel.EHLIYET_VAR = item.EHLIYET_VAR;
            personel.EHLIYET_SINIF = item.EHLIYET_SINIF;
            if (item.MEVCUT_IS_ILK_TARIH != null)
            {
                personel.MEVCUT_IS_ILK_TARIH = item.MEVCUT_IS_ILK_TARIH.Value.AddDays(1);
            }
            else
            {
                personel.MEVCUT_IS_ILK_TARIH = null;
            }
            if (item.MEVCUT_IS_ILK_TARIH2 != null)
            {
                personel.MEVCUT_IS_ILK_TARIH2 = item.MEVCUT_IS_ILK_TARIH2.Value.AddDays(1);
            }
            else
            {
                personel.MEVCUT_IS_ILK_TARIH2 = null;
            }
            if (item.MEVCUT_IS_ILK_TARIH3 != null)
            {
                personel.MEVCUT_IS_ILK_TARIH3 = item.MEVCUT_IS_ILK_TARIH3.Value.AddDays(1);
            }
            else
            {
                personel.MEVCUT_IS_ILK_TARIH3 = null;
            }
            if (item.IS_CIKIS_TARIH != null)
            {
                personel.IS_CIKIS_TARIH = item.IS_CIKIS_TARIH.Value.AddDays(1);
            }
            else
            {
                personel.IS_CIKIS_TARIH = null;
            }
            if (item.IS_CIKIS_TARIH2 != null)
            {
                personel.IS_CIKIS_TARIH2 = item.IS_CIKIS_TARIH2.Value.AddDays(1);
            }
            else
            {
                personel.IS_CIKIS_TARIH2 = null;
            }
            if (item.IS_CIKIS_TARIH3 != null)
            {
                personel.IS_CIKIS_TARIH3 = item.IS_CIKIS_TARIH3.Value.AddDays(1);
            }
            else
            {
                personel.IS_CIKIS_TARIH3 = null;
            }
            personel.CALISILAN_BIRIM = item.CALISILAN_BIRIM;
            personel.GOREV = item.GOREV;
            if (item.ILK_IS_TARIH != null)
            {
                personel.ILK_IS_TARIH = item.ILK_IS_TARIH.Value.AddDays(1);
            }
            else
            {
                personel.ILK_IS_TARIH = null;
            }
            personel.KAN_GRUP = item.KAN_GRUP;
            personel.VARSA_SUREKLI_HAST = item.VARSA_SUREKLI_HAST;
            personel.VARSA_ENGEL_DURUM = item.VARSA_ENGEL_DURUM;
            personel.VARSA_SUREKLI_KULL_ILAC = item.VARSA_SUREKLI_KULL_ILAC;
            personel.COVID_ASI_DURUM = item.COVID_ASI_DURUM;
            personel.KAC_DOZ_ASI = item.KAC_DOZ_ASI;
            personel.ILETISIM_BILGI_TEL = item.ILETISIM_BILGI_TEL;
            personel.ILETISIM_BILGI_MAIL = item.ILETISIM_BILGI_MAIL;
            personel.ACIL_DURUM_KISI = item.ACIL_DURUM_KISI;
            personel.ACIL_DURUM_KISI_ILETISIM = item.ACIL_DURUM_KISI_ILETISIM;
            personel.KAYIT_TARIH = item.KAYIT_TARIH;
            personel.CINSIYET = item.CINSIYET;
            _context.TBL_PERSONAL_DATA.Update(personel);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

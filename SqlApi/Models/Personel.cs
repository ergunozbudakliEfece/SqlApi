using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class Personel
    {
        [Key]
        public int USER_ID { get; set; }
        public string ISIM { get; set; }
        public string SOYISIM { get; set; }
        public string SUBE { get; set; }
        public string CINSIYET { get; set; }
        public string? TCKN { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DOGUM_TARIHI { get; set; }
        public string? DOGUM_YERI_IL { get; set; }
        public string? DOGUM_YERI_ILCE { get; set; }
        public string? OGRENIM_DURUMU { get; set; }
        public string? MEZUN_OKUL { get; set; }
        public string? MEZUN_BOLUM { get; set; }
       
        public int? MEZUN_YIL { get; set; }
        public string? IKAMETGAH_ADRES { get; set; }
        public string? IKAMETGAH_IL { get; set; }
        public string? IKAMETGAH_ILCE { get; set; }
        public string? MEDENI_HAL { get; set; }
        public string? ES_CALISMA_DURUMU { get; set; }
        public string? ES_CALISMA_FIRMA { get; set; }
        public string? ES_UNVANI { get; set; }
        public Int16? COCUK_SAYI { get; set; }
        public string? IKAMET_EV_DURUM { get; set; }
        public string? ARAC_DURUM { get; set; }
        public string? ARAC_PLAKA { get; set; }
        public string? EHLIYET_VAR { get; set; }
        public string? EHLIYET_SINIF { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MEVCUT_IS_ILK_TARIH { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MEVCUT_IS_ILK_TARIH2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MEVCUT_IS_ILK_TARIH3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IS_CIKIS_TARIH { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IS_CIKIS_TARIH2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IS_CIKIS_TARIH3 { get; set; }
        public string? CALISILAN_BIRIM { get; set; }
        public string? GOREV { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ILK_IS_TARIH { get; set; }
        public string? KAN_GRUP { get; set; }
        public string? VARSA_SUREKLI_HAST { get; set; }
        public string? VARSA_ENGEL_DURUM { get; set; }
        public string? VARSA_SUREKLI_KULL_ILAC { get; set; }
        public string? COVID_ASI_DURUM { get; set; }
        public Int16? KAC_DOZ_ASI { get; set; }
        public string? ILETISIM_BILGI_TEL { get; set; }
        public string? ILETISIM_BILGI_MAIL { get; set; }
        public string? ACIL_DURUM_KISI { get; set; }
        public string? ACIL_DURUM_KISI_ILETISIM { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? KAYIT_TARIH { get; set; }

    }
}

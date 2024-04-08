using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using static SqlApi.Task.MailTask;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;

namespace SqlApi.Helpers
{
    public sealed class Singleton
    {
        private static Singleton Instance = null;
        
        public static Singleton GetInstance()
        {
            
            if (Instance == null)
            {
                Instance = new Singleton();
            }
            
            return Instance;
        }
       
        public void DoWork(DateTime date, IConfiguration _configuration)
        {

            MailMessage mail = new MailMessage();
            try
            {
                string query2 = @"EXEC SP_SEND_MAIL 1";
                List<MailModel> exec2 = null;
                string sqldataSource2 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader2;
                using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                {
                    mycon2.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query2, mycon2))
                    {
                        sqlreader2 = myCommand1.ExecuteReader();
                        exec2 = DataReaderMapToList<MailModel>(sqlreader2);
                        sqlreader2.Close();
                        mycon2.Close();
                    }
                }
                List<MusteriModel> musteriList;
                using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                {
                    mycon2.Open();
                    using (SqlCommand myCommand1 = new SqlCommand("Select * FROM TBL_MUSTERI WHERE PLASIYER<>'admin'", mycon2))
                    {
                        sqlreader2 = myCommand1.ExecuteReader();
                        musteriList = DataReaderMapToList<MusteriModel>(sqlreader2);
                        sqlreader2.Close();
                        mycon2.Close();
                    }
                }
                List<UserModel> users;
                using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                {
                    mycon2.Open();
                    using (SqlCommand myCommand1 = new SqlCommand("Select * FROM TBL_USER_DATA", mycon2))
                    {
                        sqlreader2 = myCommand1.ExecuteReader();
                        users = DataReaderMapToList<UserModel>(sqlreader2);
                        sqlreader2.Close();
                        mycon2.Close();
                    }
                }
                List<MusteriUrunModel> urunler;
                using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                {
                    mycon2.Open();
                    using (SqlCommand myCommand1 = new SqlCommand("Select * FROM TBL_MUSTERI_URUN_BILGI", mycon2))
                    {
                        sqlreader2 = myCommand1.ExecuteReader();
                        urunler = DataReaderMapToList<MusteriUrunModel>(sqlreader2);
                        sqlreader2.Close();
                        mycon2.Close();
                    }
                }
                List<MusteriRandevuModel> randevu;
                using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
                {
                    mycon2.Open();
                    using (SqlCommand myCommand1 = new SqlCommand("Select * FROM TBL_MUSTERI_RANDEVU", mycon2))
                    {
                        sqlreader2 = myCommand1.ExecuteReader();
                        randevu = DataReaderMapToList<MusteriRandevuModel>(sqlreader2);
                        sqlreader2.Close();
                        mycon2.Close();
                    }
                }
                var randevuplanlanan = randevu.Where(x => (DateTime)x.PLANLANAN_TARIH > date && (DateTime)x.PLANLANAN_TARIH < date.AddDays(7)).ToList();
                var randevugerceklesen = randevu.Where(x => (DateTime)x.PLANLANAN_TARIH > date.AddDays(-7)).ToList();
                var uruneklenen = urunler.Where(x => (DateTime)x.DEGISIKLIK_TARIHI > date.AddDays(-7)).ToList();
                List<string> plasiyerler = new List<string>();

                int GetWeekNumber(DateTime date)
                {
                    return (date.DayOfYear - GetWeekday(date.DayOfWeek) + 10) / 7;
                }
                int GetWeekday(DayOfWeek dayOfWeek)
                {
                    return dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
                }
                for(var m=0;m<musteriList.Count;m++)
                {
                    var plasiyer = musteriList[m].PLASIYER;
                    if (!plasiyerler.Contains(plasiyer))
                    {
                        plasiyerler.Add(plasiyer);
                    }
                };
                for (var j = 0; j < plasiyerler.Count; j++)
                {
                    var fullname = users.Where(x => x.USER_NAME == plasiyerler[j]).ToList();
                    List<int> musteriIdler = new List<int>();
                    var icerik = "";
                    var icerik1 = "";
                    var icerik2 = "";
                    var icerik3 = "";
                    //string subject = "CRM Raporu | " + GetWeekNumber(date) + ".Hafta | " + fullname[0].USER_FIRSTNAME + " " + fullname[0].USER_LASTNAME;
                    string subject = "CRM Raporu | " + GetWeekNumber(date) + ".Hafta | " + fullname[0].USER_FIRSTNAME + " " + fullname[0].USER_LASTNAME;
                    var plMusteriList = musteriList.Where(x => x.PLASIYER == plasiyerler[j]).ToList();
                    var plrandevuplanlanan = randevuplanlanan.Where(x => x.KAYIT_YAPAN_KULLANICI_ID == fullname[0].USER_ID).ToList();
                    var plUrunList = uruneklenen.Where(x => x.KAYIT_YAPAN_KULLANICI == plasiyerler[j]).ToList();
                    for (int a = 0; a < plMusteriList.Count; a++)
                    {
                        var musteriId = plMusteriList[a].MUSTERI_ID;
                        if (!musteriIdler.Contains(musteriId))
                        {
                            try
                            {

                                musteriIdler.Add(musteriId);
                            }
                            catch (Exception e)
                            {
                                var error = e.Message;
                            }
                        }
                        if (plMusteriList[a].KAYIT_TARIHI > date.AddDays(-7))
                        {
                            var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_ADI + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_IL + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_ILCE + " </td><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plMusteriList[a].MUSTERI_NOTU + " </td></tr>";
                            icerik = icerik + tr;
                        }

                    }
                    for (int a = 0; a < musteriIdler.Count; a++)
                    {
                        var f = randevuplanlanan.Where(x => x.MUSTERI_ID == musteriIdler[a]).ToList();

                        if (f != null)
                        {
                            for (int b = 0; b < f.Count(); b++)
                            {
                                var ad = musteriList.Find(x => x.MUSTERI_ID == musteriIdler[a]).MUSTERI_ADI;
                                if (f[b].GERCEKLESEN_TARIH == null)
                                {
                                    var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + ad + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(f[b].PLANLANAN_TARIH.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(f[b].KAYIT_TARIHI.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + f[b].RANDEVU_NOTU + " </td></tr>";
                                    icerik1 = icerik1 + tr;
                                }




                            }

                        }
                        var e = randevugerceklesen.Where(x => x.MUSTERI_ID == musteriIdler[a]).ToList();

                        if (e != null)
                        {
                            for (int b = 0; b < e.Count(); b++)
                            {
                                var ad = musteriList.Find(x => x.MUSTERI_ID == musteriIdler[a]).MUSTERI_ADI;
                                if (e[b].GERCEKLESEN_TARIH != null)
                                {
                                    var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + ad + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(e[b].PLANLANAN_TARIH.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(e[b].GERCEKLESEN_TARIH.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + e[b].RANDEVU_NOTU + " </td></tr>";
                                    icerik2 = icerik2 + tr;
                                }




                            }

                        }

                    }

                    if (plUrunList.Count > 0)
                    {
                        for (int a = 0; a < plUrunList.Count; a++)
                        {
                            var ad = musteriList.Find(x => x.MUSTERI_ID == plUrunList[a].MUSTERI_ID).MUSTERI_ADI;
                            var tr = "<tr><td style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + ad + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + plUrunList[a].URUN_GRUBU + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + Decimal.Parse(plUrunList[a].YILLIK_KULLANIM.ToString()).ToString("#,##0") + " " + plUrunList[a].OLCU_BIRIMI + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(plUrunList[a].KAYIT_TARIHI.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td><td  style='border: 1px solid #CCCCCC;border-collapse: collapse;'> " + DateTime.Parse(plUrunList[a].DEGISIKLIK_TARIHI.ToString()).ToString("dddd, dd MMMM yyyy HH:mm") + " </td></tr>";
                            icerik3 = icerik3 + tr;

                        }
                    }

                    var table1 = "";
                    var table2 = "";
                    var table3 = "";
                    var table4 = "";
                    var img = NovaImzaModel.Imza;
                    if (icerik == "")
                    {
                        table1 = "<p>Geçtiğimiz hafta kaydedilen herhangi bir müşteri yoktur.</p>";
                    }
                    else
                    {
                        table1 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri İl </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri İlçe </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Notu </th>" + icerik + "</table></br></br></br>";
                    }
                    if (icerik1 == "")
                    {
                        table2 = "<p>Gelecek hafta için planlanmış herhangi bir ziyaret yoktur.</p>";
                    }
                    else
                    {
                        table2 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Planlanan Tarih </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Kayıt Tarihi </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Randevu Notu </th>" + icerik1 + "</table>";
                    }
                    if (icerik2 == "")
                    {
                        table3 = "<p>Geçtiğimiz hafta için gerçekleşmiş herhangi bir ziyaret yoktur.</p>";
                    }
                    else
                    {
                        table3 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Planlanan Tarih </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Gerçekleşme Tarihi </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Randevu Notu </th>" + icerik2 + "</table>";
                    }
                    if (icerik3 == "")
                    {
                        table4 = "<p>Geçtiğimiz hafta için eklenmiş veya düzenlenmiş herhangi bir ürün yoktur.</p>";
                    }
                    else
                    {
                        table4 = "<table style='border: 1px solid #CCCCCC;border-collapse: collapse;'>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Müşteri Adı </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Ürün Grubu </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Yıllık Kullanım </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Kayıt Tarihi </th>" +
                        "<th style='border: 1px solid #CCCCCC;border-collapse: collapse;'> Düzenleme Tarihi </th>" + icerik3 + "</table>";
                    }

                    string body = "<h2>Geçtiğimiz Hafta Kaydedilen Müşteriler</h2>" +
                             table1 +
                            "<hr>" +
                            "<h2>Geçtiğimiz Hafta Gerçekleşen Müşteri Ziyaretleri</h2>" +
                            table3 +
                            "<hr><h2>Geçtiğimiz Hafta Eklenen veya Düzenlenen Ürünler</h2>" +
                            table4 +
                            "<hr><h2>Gelecek Haftaya Planlanan Müşteri Ziyaretleri</h2>" +
                            table2 +
                            "</br></br></br>" + img;



                    string den = exec2[0].BCC.Substring(0, exec2[0].BCC.Length - 1);
                    mail.Bcc.Add(den);
                    //mail.Bcc.Add("surecgelistirme@efecegalvaniz.com");
                    //mail.Bcc.Add("ugurkonakci@efecegalvaniz.com");
                    //mail.Bcc.Add("ergunozbudakli@efecegalvaniz.com");
                    mail.From = new MailAddress("sistem@efecegalvaniz.com");
                    mail.Body = body;
                    mail.Subject = subject;


                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = "192.168.2.13";
                    smtp.UseDefaultCredentials = true;
                    if (icerik == "" && icerik1 == "" && icerik2 == "" && icerik3 == "")
                    {

                    }
                    else
                    {
                        smtp.Send(mail);
                    }

                };
            }
            catch (Exception e)
            {
                MailMessage mail1 = new MailMessage();
                mail1.Bcc.Add("surecgelistirme@efecegalvaniz.com");
                mail1.From = new MailAddress("sistem@efecegalvaniz.com");
                mail1.Body = e.Message;
                mail1.Subject = "HATA";


                mail1.IsBodyHtml = true;

                SmtpClient smtp1 = new System.Net.Mail.SmtpClient();
                smtp1.Host = "192.168.2.13";
                smtp1.UseDefaultCredentials = true;
                smtp1.Send(mail1);


            }






        }
    }

}

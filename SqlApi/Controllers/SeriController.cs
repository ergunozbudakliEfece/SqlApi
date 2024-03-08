using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection;
using SqlApi.Helpers;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SeriController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{tip}")]
        public JsonResult GET(string tip)
        {


            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_SERISIRA WITH(NOLOCK) WHERE SERI_TIP='"+tip+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("hi/{depo}")]
        public JsonResult Hesapla(string depo)
        {


            DataTable table = new DataTable();


            string query = @"SP_SERI_REHBER_HI '" + depo+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("hesapla/{seri}/{miktar}")]
        public JsonResult Hesapla(string seri,int miktar)
        {


            DataTable table = new DataTable();


            string query = @"SP_DL_DAGITIM '"+seri+"',"+miktar;

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("exec/{tip}")]
        public JsonResult GETAll(string tip)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_GET_SERINUM '"+tip+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{tip}/{num}")]
        public IActionResult Update(string tip,int num)
        {

            string query = @"EXEC SP_UPD_SERINUM '"+tip+"',"+num;

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return Ok();
        }
        [HttpGet("receteden/{stok}/{genislik}/{tip}")]
        public IActionResult GetRecetedenGetir(string stok,string genislik, string tip)
        {
            DataTable table = new DataTable();

            string query = @"exec SP_RECETEDEN_GETIR '"+stok+"','"+genislik+"','"+tip+"'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("serirehber")]
        public JsonResult GetSeriRehber()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SERI_REHBER";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("serirehbertumu")]
        public JsonResult GetSeriRehberTumu()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SERI_REHBER_TUMU";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("manuelhat/{hat}")]
        public JsonResult GetHatMan(string hat)
        {


            DataTable table = new DataTable();
            string query = "";
            if (hat == "TP01")
            {
                query = @"EXEC MANUEL_HAM_GIRDI_KONTROL '" + hat + "'";
            }
            else
            {
                query = @"EXEC MANUEL_HAM_GIRDI_KONTROL '" + hat + "'";
            }
           

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("manuelgir/{hat}/{seri}/{id}")]
        public string SeriGir(string hat,string seri,int id)
        {

            try
            {
                string query = @"INSERT INTO TBL_HAT_GIRDI(HAT_KODU,SERI_NO,USER_ID) VALUES('" + hat + "','" + seri + "'," + id + ")";

                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (System.Exception e)
            {

                return "Hata:" + e.Message;
            }
            

            return "Başarılı";
        }
        [HttpGet("manuelsil/{seri}")]
        public string SeriSil(string seri)
        {

            try
            {
                string query = @"DELETE FROM TBL_HAT_GIRDI WHERE SERI_NO='"+seri+"'";

                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (System.Exception e)
            {

                return "Hata:" + e.Message;
            }


            return "Başarılı";
        }
        [HttpGet("bakiye/{seri}/{depo}")]
        public JsonResult GETBAK(string seri,int depo)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_SERI_BAKIYE '"+seri+"',"+depo;

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("rehber/{stokkodu}/{depo}")]
        public JsonResult GETREHBER(string stokkodu,int depo)
        {


            DataTable table = new DataTable();
            string query = @"EXEC SP_SERI_REHBER '" + stokkodu + "',"+depo;
            
            

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("kontrol/{seri}")]
        public JsonResult GETSERI(string seri)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SERITRA_KONTROL '" + seri + "'";

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    table.Load(sqlreader);
                    sqlreader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("MAIL")]
        public string SENDMAIL()
        {
            try
            {
                string query = @"EXEC EXPORT_MAIL_STOCK";
                List<ExportMailModel> exec;
                string sqldataSource = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader;
                using (SqlConnection mycon = new SqlConnection(sqldataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        exec = DataReaderMapToList<ExportMailModel>(sqlreader);
                        sqlreader.Close();
                        mycon.Close();
                    }
                }

                byte[] file = GeneratePdf(exec);
                MailMessage mail = new MailMessage();
                mail.Bcc.Add("surecgelistirme@efecegalvaniz.com");
                //mail.Bcc.Add("EXPORT@efecegalvaniz.com");
                mail.From = new MailAddress("sistem@efecegalvaniz.com");
                Attachment att = new Attachment(new MemoryStream(file), "EFECE_STOCK_LIST_" + DateTime.Now.ToShortDateString() + ".pdf");
                mail.Attachments.Add(att);
                mail.Subject = "EFECE STOCK LIST";
                mail.Body = "<p>Stock list is dynamic and is subject to confirmation.</p><p>Please contact for special dimension inquiries. <p>Zinc spray on welding line is NOT included for stock items.</p><p>Minus thickness tolerances are utilized for stock list items.</p><p>Invoicing is done according to actual weight upon selection of your requested items.</p></br><p>Dear Partners;</p><p>Attached you can find our weekly stock list. Have a special inquiry? Please contact us.</p><p>If you no longer want to receive this list, please reply to <a href=\"mailto:export@efecegalvaniz.com\">export@efecegalvaniz.com</a>.</p><p>Kind Regards,</p>" + NovaImzaModel.ExportImza;

                mail.IsBodyHtml = true;

                SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "192.168.2.13";
                smtp.UseDefaultCredentials = true;
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                return e.Message;
            }



            return "BAŞARILI";
        }
        public byte[] GeneratePdf(List<ExportMailModel> data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 20f, 20f, 80f, 40f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.CloseStream = false;
                    document.Open();
                    PdfPTable table = new PdfPTable(6);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;

                    PdfPCell header = new PdfPCell(new Phrase("STOCK NAME"));
                    header.FixedHeight = 40.0f;
                    header.HorizontalAlignment = 1;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.UseVariableBorders = true;
                    header.BorderColor = BaseColor.LIGHT_GRAY;
                    //0=Left, 1=Centre, 2=Right
                    table.AddCell(header);
                    header = new PdfPCell(new Phrase("MATERIAL"));
                    header.FixedHeight = 40.0f;
                    header.HorizontalAlignment = 1;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.BorderColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(header);
                    header = new PdfPCell(new Phrase("LENGTH (MM)"));
                    header.FixedHeight = 40.0f;
                    header.HorizontalAlignment = 1;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.BorderColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(header);
                    header = new PdfPCell(new Phrase("BUNDLE SIZE (PCS)"));
                    header.FixedHeight = 40.0f;
                    header.HorizontalAlignment = 1;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.BorderColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(header);
                    header = new PdfPCell(new Phrase("STOCK QTY (KG)"));
                    header.FixedHeight = 40.0f;
                    header.HorizontalAlignment = 1;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.BorderColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(header);
                    header = new PdfPCell(new Phrase("STOCK QTY (PCS)"));
                    header.FixedHeight = 40.0f;
                    header.HorizontalAlignment = 1;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.BorderColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(header);
                    for (int i = 0; i < data.Count; i++)
                    {
                        header = new PdfPCell(new Phrase(data[i].STOCK_NAME));
                        header.HorizontalAlignment = 0;
                        header.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(header);
                        header = new PdfPCell(new Phrase(data[i].MATERIAL));
                        header.HorizontalAlignment = 1;
                        header.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(header);
                        header = new PdfPCell(new Phrase(data[i].LENGTH_MM.ToString()));
                        header.HorizontalAlignment = 1;
                        header.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(header);
                        header = new PdfPCell(new Phrase(data[i].BUNDLE_SIZE_PCS.ToString("N0", CultureInfo.GetCultureInfo("tr-TR"))));
                        header.HorizontalAlignment = 1;
                        header.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(header);
                        header = new PdfPCell(new Phrase(data[i].STOCK_QTY_KG.ToString("N0", CultureInfo.GetCultureInfo("tr-TR"))));
                        header.HorizontalAlignment = 2;
                        header.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(header);
                        header = new PdfPCell(new Phrase(data[i].STOCK_QTY_PCS.ToString("N0", CultureInfo.GetCultureInfo("tr-TR"))));
                        header.HorizontalAlignment = 2;
                        header.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(header);
                    }
                    table.SetWidths(new float[] { 8.5f, 7.5f, 3.5f, 4.5f, 3.5f, 4.5f });
                    var s = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\ExportMailPdf\\export_mail.png");
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(s);
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING;
                    jpg.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                    jpg.SetAbsolutePosition(0, 0);
                    writer.PageEvent = new ImageBackgroundHelper(jpg);

                    document.Add(table);
                    document.Add(new iTextSharp.text.Paragraph("* Stock list is dynamic and is subject to confirmation."));
                    document.Add(new iTextSharp.text.Paragraph("* Please contact for special dimension inquiries. "));
                    document.Add(new iTextSharp.text.Paragraph("* Zinc spray on welding line is NOT included for stock items."));
                    document.Add(new iTextSharp.text.Paragraph("* Minus thickness tolerances are utilized for stock list items."));
                    document.Add(new iTextSharp.text.Paragraph("* Invoicing is done according to actual weight upon selection of your requested items."));
                    document.Close();
                    byte[] docArray = memoryStream.ToArray();
                    return docArray;
                }

            }

        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public class ExportMailModel
        {
            public string STOCK_NAME { get; set; }
            public string MATERIAL { get; set; }
            public int LENGTH_MM { get; set; }
            public int BUNDLE_SIZE_PCS { get; set; }
            public int STOCK_QTY_KG { get; set; }
            public int STOCK_QTY_PCS { get; set; }
        }
    }
}

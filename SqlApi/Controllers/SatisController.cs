using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using static SqlApi.Controllers.AttendanceController;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System;
using System.Collections;
using static SqlApi.Task.MailTask;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatisController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SatisController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet("{c}")]
        public IEnumerable GET(string c)
        {
            string query1 = @" EXEC SP_TEKLIFNO "+c; 
            List<teklifModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<teklifModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        [HttpGet("teklifler/{teklifno}")]
        public IEnumerable GETTEKLIFLER(string teklifno)
        {
            string query1 = @" EXEC NOVA_TEKLIF_KALEM "+teklifno ;
            List<tekliflerModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Con");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<tekliflerModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        [HttpGet("kur/{kur}")]
        public IEnumerable KurGetir(string kur)
        {
            string query1 = @" EXEC NOVA_SP_KUR " + kur;
            List<kurmodel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<kurmodel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        [HttpGet("msip")]
        public IEnumerable GETSIP()
        {
            string query1 = @"EXEC NOVA_SP_MSIP_KONTROL";
            List<msipModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Conn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<msipModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        [HttpGet("ustteklifler/{teklifno}")]
        public IEnumerable USTGETTEKLIFLER(string teklifno)
        {
            string query1 = @" EXEC NOVA_TEKLIF_UST " + teklifno;
            List<tekliflerUstModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Con");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<tekliflerUstModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        [HttpGet("ustteklifler")]
        public IEnumerable USTGETTEKLIFLERAll()
        {
            string query1 = @" EXEC NOVA_TEKLIF_UST";
            List<tekliflerUstModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Con");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<tekliflerUstModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        
        [HttpGet("plasiyerler/{plasiyerno}")]
        public JsonResult GETPLASIYER(string plasiyerno)
        {

            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBLCARIPLASIYER WITH(NOLOCK) WHERE PLASIYER_KODU=" + plasiyerno;

            string sqldataSource = _configuration.GetConnectionString("Conn");
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
        public class plasiyerlerModel
        {
            public string PLASIYER_KODU { get; set; }
            public string PLASIYER_ACIKLAMA { get; set; }
        } 
        public class teklifModel
        {
            public int LAST_NUMBER { get; set; }

        }
        public class tekliflerModel
        {
            public string FISNO { get; set; }
            public string STOK_KODU { get; set; }
            public string STOK_ADI { get; set; }
            public decimal STHAR_GCMIK { get; set; }
            public decimal STHAR_GCMIK2 { get; set; }
            public string OLCU_BR1 { get; set; }
            public string OLCU_BR2 { get; set; }
            public decimal STHAR_SATISK { get; set; }
            public decimal STHAR_BF { get; set; }
            public decimal STHAR_NF { get; set; }
            public string TESLIM_TARIHI { get; set; }
            public string TESLIM_YERI { get; set; }
            public decimal STHAR_DOVFIAT { get; set; }
            public decimal FIRMA_DOVTUT { get; set; }
            public int STHAR_DOVTIP { get; set; }
            public int DEPO_KODU { get; set; }

        }
        public class tekliflerUstModel
        {
            public string TEKLIF_NO { get; set; }
            public string CARI_KODU { get; set; }
            public string CARI_ISIM { get; set; }
            public string DOVIZ_ISMI { get; set; }
            public string KOD1 { get; set; }
            public string KOD2 { get; set; }
            public string ACIKLAMA { get; set; }
            public string PLASIYER_KODU { get; set; }
            public string PROJE_KODU { get; set; }
            public string SEVK_YERI { get; set; }
            public string TESLIM_YERI { get; set; }
            public string GECERLILIK_SURESI { get; set; }
            public string TAHSILAT_SEKLI { get; set; }
            public string CARI_ADRES { get; set; }
            public string GSMNO { get; set; }
            public string EPOSTA { get; set; }
            public decimal TEVKIFAT { get; set; }
            public decimal GENELTOPLAM { get; set; }
            public decimal BRUTTUTAR { get; set; }
            public decimal KDV { get; set; }
            public decimal SAT_ISKT { get; set; }
            public decimal DOVIZTUT { get; set; }
        }
        public class kurmodel
        {
            public string DOV_SATIS { get; set; }
        }
        public class msipModel
        {   
            public string? TARIH { get; set; }
            public string? KAYIT_ZAMANI { get; set; }
            public string? FISNO { get; set; }
            public string? PLASIYER_ADI { get; set; }
            public string? CARI_ISIM { get; set; }
            public string? STOK_ADI { get; set; }
            public decimal? MIKTAR { get; set; }
            public string? SP_OLCUBR { get; set; }
            public double? NETSIS_KUR { get; set; }
            public string? BAZ_KUR { get; set; }
            public decimal? MIKTAR2 { get; set; }
            public string? SP_OLCUBR2 { get; set; }
            public decimal? SP_BRUT_FIYAT { get; set; }
            public decimal? SP_ISKONTO { get; set; }
            public decimal? SP_NET_FIYAT { get; set; }
            public decimal? SP_DOVIZ_FIYATI { get; set; }
            public string? SP_DOVIZ_TIPI { get; set; }
            public decimal? SP_KUR { get; set; }
            public string? FIYATKODU { get; set; }
            public string? LISTE_BRUT_FIYAT { get; set; }
            public decimal? LISTE_ISKONTO { get; set; }
            public string? LISTE_NET_TOPTAN_FIYAT { get; set; }
            public string? FIYATDOVIZTIPI { get; set; }
            public decimal? HES_TL_FIYAT { get; set; }
            public decimal? FIYAT_KONTROL { get; set; }
            public int DEPO_KODU { get; set; }
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
    }
}

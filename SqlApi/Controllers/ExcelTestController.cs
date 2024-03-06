using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlApi.Models;
using System.IO;
using System;
using System.Collections;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Nancy.Json;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelTestController : ControllerBase
    {
        private readonly StokContext _context;
        private readonly IConfiguration _configuration;
        public ExcelTestController(StokContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_FABRIKA_STOK";

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
        [HttpGet]
        [Route("storage/{DEPO}")]
        public JsonResult GetAllWithStorage(int DEPO)
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_FABRIKA_STOK " + DEPO;

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
        //[HttpGet]
        //public IEnumerable GetAll()
        //{
        //    OleDbConnection con;
        //    OleDbCommand cmd;
        //    OleDbDataReader dr;
        //    List<ExcelModel> list= new List<ExcelModel>();
        //    List<string> tckn= new List<string>();

        //    try
        //    {
        //        con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\192.168.2.3\ortak\EFECE_LISTELER\NOVA\NOVA_EFECE_HAMMADDE_LISTESI.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;READONLY=TRUE'");

        //        cmd = new OleDbCommand("Select * FROM [RULO - BANT$A8:AE] WHERE (KURAL<> 2 AND KURAL<> 4) AND [RULO SAC KODU] is not null ", con);
        //        con.Open();
        //        dr = cmd.ExecuteReader();
        //        List<UretSeriNoModel> view = GetSeriNo();

        //        while (dr.Read())
        //        {

        //                ExcelModel excel = new ExcelModel();
        //                excel.RULOKODU = dr["RULO SAC KODU"].ToString();
        //                var rulokodu = "";
        //                if(dr["RULO SAC KODU"].ToString().Length == 7)
        //                {
        //                    rulokodu=dr["RULO SAC KODU"].ToString().Substring(0, 5);
        //                }
        //                else if(dr["RULO SAC KODU"].ToString().Length == 6)
        //                {
        //                    rulokodu="0"+dr["RULO SAC KODU"].ToString().Substring(0, 4);
        //                }
        //                else
        //                {
        //                    rulokodu = "0";
        //                }
        //                var sorgu = view.FirstOrDefault(x => x.SERI_NO.Substring(0,5) == rulokodu);

        //            //var gun = int.Parse(dr.GetValue(14).ToString().Split(' ')[0].Split('.')[0]).ToString("00");
        //            //var ay = int.Parse(dr.GetValue(14).ToString().Split(' ')[0].Split('.')[1]).ToString("00");
        //            //var yil = int.Parse(dr.GetValue(14).ToString().Split(' ')[0].Split('.')[2]).ToString("0000");


        //                excel.RULOKODU = dr.GetValue(0).ToString();
        //                excel.KONUM = dr.GetValue(2).ToString();
        //                excel.RENKKODU = dr.GetValue(3).ToString();
        //                excel.KALINLIK = dr.GetValue(6).ToString(); 
        //                excel.GENISLIK = dr.GetValue(7).ToString(); 
        //                excel.KALITE = dr.GetValue(4).ToString();
        //                excel.KAPLAMA = dr.GetValue(5).ToString();
        //                excel.MENSEI = dr.GetValue(12).ToString();
        //                excel.STOKSAHASI = dr.GetValue(13).ToString();
        //                excel.RULOBANTGIRISTARIH = dr.GetValue(14).ToString().Split(' ')[0];
        //                excel.ISEMRITARIHI = dr.GetValue(17).ToString().Split(' ')[0];
        //                excel.URETILMETARIHI = dr.GetValue(18).ToString().Split(' ')[0];
        //                excel.IRSALIYENO = dr.GetValue(19).ToString();
        //                excel.ISEMRINO = dr.GetValue(20).ToString();
        //                excel.URETILECEKMALZEMEADI = dr.GetValue(21).ToString();
        //                excel.MALZEMEKALINLIK = dr.GetValue(22).ToString();
        //                excel.MALZEMEBOYUZUNLUK = dr.GetValue(23).ToString();
        //                excel.BOBINNO = dr.GetValue(1).ToString(); 
        //                excel.RESERVETARIH = dr.GetValue(15).ToString().Split(' ')[0];
        //                excel.RESERVEACIKLAMA = dr.GetValue(16).ToString();
        //                excel.ADET = dr.GetValue(8).ToString();
        //                excel.TONAJ = dr.GetValue(9).ToString();
        //                excel.KURAL = dr.GetValue(25).ToString();
        //                excel.ISEMRIKOD = dr.GetValue(28).ToString();
        //                if (sorgu!=null)
        //                {

        //                excel.TUTAR = sorgu.TUTAR;
        //                excel.DOV_TIP = sorgu.DOV_TIP;
        //                if (sorgu.GIRILEN_USD_KUR != null)
        //                {
        //                    excel.GIRILEN_USD_KUR = (decimal)sorgu.GIRILEN_USD_KUR;
        //                }
        //                if (sorgu.GIR_KUR_HES_USD_FIYAT != null)
        //                {
        //                    excel.GIR_KUR_HES_USD_FIYAT = sorgu.GIR_KUR_HES_USD_FIYAT;
        //                }
        //                if (sorgu.TCMB_DOV_SATIS != null)
        //                {
        //                    excel.TCMB_DOV_SATIS = sorgu.TCMB_DOV_SATIS;
        //                }
        //                if (sorgu.TL_USD_HES_FIYAT != null)
        //                {
        //                    excel.TL_USD_HES_FIYAT = sorgu.TL_USD_HES_FIYAT;
        //                }



        //            }



        //                list.Add(excel);




        //        }
        //        con.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    return list;
        //}
        [HttpGet("stok")]
        public IEnumerable GetAllStok()
        {
            OleDbConnection con;
            OleDbCommand cmd;
            OleDbDataReader dr;
            List<ExcelModel> list = new List<ExcelModel>();
            List<string> tckn = new List<string>();

            try
            {
                con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\192.168.2.3\ortak\EFECE_LISTELER\NOVA\NOVA_EFECE_HAMMADDE_LISTESI.xlsx; Extended Properties='Excel 12.0 xml;HDR=YES;READONLY=TRUE'");

                cmd = new OleDbCommand("Select * FROM [RULO - BANT$A8:AE] WHERE (KURAL<> 3 AND KURAL<> 2 AND KURAL<> 4) AND [RULO SAC KODU] is not null ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                List<UretSeriNoModel> view = GetSeriNo();

                while (dr.Read())
                {

                    ExcelModel excel = new ExcelModel();
                    excel.RULOKODU = dr["RULO SAC KODU"].ToString();
                    var rulokodu = "";
                    if (dr["RULO SAC KODU"].ToString().Length == 7)
                    {
                        rulokodu = dr["RULO SAC KODU"].ToString().Substring(0, 5);
                    }
                    else if (dr["RULO SAC KODU"].ToString().Length == 6)
                    {
                        rulokodu = "0" + dr["RULO SAC KODU"].ToString().Substring(0, 4);
                    }
                    else
                    {
                        rulokodu = "0";
                    }
                    var sorgu = view.FirstOrDefault(x => x.SERI_NO.Substring(0, 5) == rulokodu);



                    excel.RULOKODU = dr.GetValue(0).ToString();
                    excel.KONUM = dr.GetValue(2).ToString();
                    excel.RENKKODU = dr.GetValue(3).ToString();
                    excel.KALINLIK = dr.GetValue(6).ToString();
                    excel.GENISLIK = dr.GetValue(7).ToString();
                    excel.KALITE = dr.GetValue(4).ToString();
                    excel.KAPLAMA = dr.GetValue(5).ToString();
                    excel.MENSEI = dr.GetValue(12).ToString();
                    excel.STOKSAHASI = dr.GetValue(13).ToString();
                    excel.RULOBANTGIRISTARIH = dr.GetValue(14).ToString().Split(' ')[0];
                    excel.ISEMRITARIHI = dr.GetValue(17).ToString().Split(' ')[0];
                    excel.URETILMETARIHI = dr.GetValue(18).ToString().Split(' ')[0];
                    excel.IRSALIYENO = dr.GetValue(19).ToString();
                    excel.ISEMRINO = dr.GetValue(20).ToString();
                    excel.URETILECEKMALZEMEADI = dr.GetValue(21).ToString();
                    excel.MALZEMEKALINLIK = dr.GetValue(22).ToString();
                    excel.MALZEMEBOYUZUNLUK = dr.GetValue(23).ToString();
                    excel.BOBINNO = dr.GetValue(1).ToString();
                    excel.RESERVETARIH = dr.GetValue(15).ToString().Split(' ')[0];
                    excel.RESERVEACIKLAMA = dr.GetValue(16).ToString();
                    excel.ADET = dr.GetValue(8).ToString();
                    excel.TONAJ = dr.GetValue(9).ToString();
                    excel.KURAL = dr.GetValue(25).ToString();
                    excel.ISEMRIKOD = dr.GetValue(28).ToString();
                    if (sorgu != null)
                    {
                        if (sorgu.DOV_TIP == "TRY")
                        {
                            excel.TUTAR = (decimal)sorgu.GIR_KUR_HES_USD_FIYAT;
                        }
                        else
                        {
                            excel.TUTAR = sorgu.TUTAR;
                        }
                       
                        excel.DOV_TIP = sorgu.DOV_TIP;
                        if (sorgu.GIRILEN_USD_KUR != null)
                        {
                            excel.GIRILEN_USD_KUR = (decimal)sorgu.GIRILEN_USD_KUR;
                        }
                        if (sorgu.GIR_KUR_HES_USD_FIYAT != null)
                        {
                            excel.GIR_KUR_HES_USD_FIYAT = sorgu.GIR_KUR_HES_USD_FIYAT;
                        }
                        if (sorgu.TCMB_DOV_SATIS != null)
                        {
                            excel.TCMB_DOV_SATIS = sorgu.TCMB_DOV_SATIS;
                        }
                        if (sorgu.TL_USD_HES_FIYAT != null)
                        {
                            excel.TL_USD_HES_FIYAT = sorgu.TL_USD_HES_FIYAT;
                        }



                    }



                    list.Add(excel);




                }
                con.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }
        [HttpGet("LastDate")]
        public string GetDate()
        {
            FileInfo fi = new FileInfo("\\\\192.168.2.3\\ortak\\EFECE_LISTELER\\NOVA\\NOVA_EFECE_HAMMADDE_LISTESI.xlsx");
            var created = fi.CreationTime;
            var lastmodified = fi.LastWriteTime;
            return lastmodified.ToString();
        }
        public List<UretSeriNoModel> GetSeriNo()
        {

            return _context.NOVA_VW_SERI_BILGI.ToList();
        }
        public class ExcelModel
        {
            
            public string RULOKODU { get; set; }
           
            public string BOBINNO { get; set; }
            
            public string KONUM { get; set; }
            
            public string RENKKODU { get; set; }
        
            public string KALINLIK { get; set; }
           
            public string GENISLIK { get; set; }
           
            public string KALITE { get; set; }
           
            public string KAPLAMA { get; set; }
           
            public string MENSEI { get; set; }
          
            public string RESERVETARIH { get; set; }
            public string RESERVEACIKLAMA { get; set; }
            public string TONAJ { get; set; }
            
            public string STOKSAHASI { get; set; }

            public string RULOBANTGIRISTARIH { get; set; }

            public string ISEMRITARIHI { get; set; }

            public string URETILMETARIHI { get; set; }

            public string IRSALIYENO { get; set; }

            public string ISEMRINO { get; set; }
            public string URETILECEKMALZEMEADI { get; set; }
            public string MALZEMEKALINLIK { get; set; }

            public string MALZEMEBOYUZUNLUK { get; set; }
           
            public string ADET { get; set; }
            public decimal TUTAR { get; set; }
            public string DOV_TIP { get; set; }
            public string KURAL { get; set; }
            public string ISEMRIKOD { get; set; }
            public decimal? GIRILEN_USD_KUR { get; set; }
            public decimal? GIR_KUR_HES_USD_FIYAT { get; set; }
            public decimal? TCMB_DOV_SATIS { get; set; } 
            public decimal? TL_USD_HES_FIYAT { get; set; }
        }
    }
}

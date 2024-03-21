using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Json;
using SqlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ShippingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            

            string query = @"EXEC SHIPPING_FORM_SEL @sipno=NULL";

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
        [HttpGet("hzrsl/{s}")]
        public JsonResult GetHZR(string s)
        {
            DataTable table = new DataTable();
            string query = "";
            if (s == "0")
            {
                query = @"EXEC SHIPPING_FORM_HZR_SEL";
            }
            else
            {
                query = @"EXEC SHIPPING_FORM_HZR_SEL '" + s + "'";
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

        [HttpGet("hzrsl/durum/{INCKEY}")]
        public JsonResult HazirListeDurumDegistir(int INCKEY)
        {
            DataTable table = new DataTable();
            string query;

            query = $@"EXEC SP_SH_FRM_HZR_AP '{INCKEY}'";

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

        [HttpPost("hzrsl")]
        public IActionResult ShippingHzrInsert(List<ShippingModel> shippingList)
        {
            DataTable table = new DataTable();

            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();

                foreach(ShippingModel shipping in shippingList)
                {
                    string query = @"EXEC SHIPPING_FORM_HZR_INS '" + shipping.TYPE + "','" + shipping.BELGE_NO + "','" + shipping.SIPARIS_NO + "','" + shipping.IRS_NO + "','" + shipping.SERI_NO + "','" + shipping.STOK_KODU + "','" + shipping.MIKTAR1 + "','" + shipping.OLCU_BR1 + "','" + shipping.MIKTAR2 + "','" + shipping.OLCU_BR2 + "','" + shipping.ACIK2 + "','" + shipping.ACIK1 + "','" + shipping.ACIK3 + "','" + shipping.SERI_NO_3 + "','" + shipping.SERI_NO_4 + "','" + shipping.ACIKLAMA_4 + "','" + shipping.ACIKLAMA_5 + "'," + shipping.INS_USER_ID + ",'" + shipping.UPD_USER_ID + "','" + shipping.EXP_1 + "','" + shipping.EXP_2 + "','" + shipping.EXP_3 + "','" + shipping.GIRIS_DEPO + "','" + shipping.CIKIS_DEPO + "','" + shipping.PLAKA + "','" + shipping.SOFOR + "'";

                    using (SqlCommand myCommand = new SqlCommand(query, mycon))
                    {
                        sqlreader = myCommand.ExecuteReader();
                        sqlreader.Close();
                    }
                }

                mycon.Close();
            }

            return Ok();
        }

        [HttpGet("hzrbn/{type}")]
        public JsonResult GetNumber(int type)
        {
            DataTable table = new DataTable();


            string query = @"EXEC SHIPPING_FORM_HZR_BN " + type;

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

        [HttpGet("all")]
        public JsonResult GetAll1()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SHIPPING_FORM_SEL";

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
        [HttpGet("arac/{plaka}/{yuk}")]
        public string AracIns(string plaka,int yuk)
        {




            try
            {
                string query = @"EXEC SP_ARAC_INS '" + plaka + "'," + yuk;

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
            catch (Exception ex)
            {
                return ex.Message;
            }
           

            return "BAŞARILI";
        }
        [HttpGet("sofor/{ad}")]
        public string SoforIns(string ad)
        {




            try
            {
                string query = @"EXEC SP_SOFOR_INS '" + ad + "'";

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
            catch (Exception ex)
            {
                return ex.Message;
            }


            return "BAŞARILI";
        }
        [HttpGet("sofor")]
        public JsonResult GetSofor()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SOFOR_SEL";

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
        [HttpGet("arac")]
        public JsonResult GetArac()
        {

            DataTable table = new DataTable();
            string query = @"EXEC SP_ARAC_SEL";
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
        [HttpGet("sip/{sipno}")]
        public JsonResult GetBySipNo(string sipno)
        {


            DataTable table = new DataTable();
            string query = @"EXEC SHIPPING_FORM_SEL '" + sipno + "'";
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
        [HttpGet("{type}")]
        public JsonResult GET(int type)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SHIPPING_FORM_BN "+type;

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
        [HttpPost]
        public IActionResult ShippingInsert(ShippingModel shipping)
        {

           
            DataTable table = new DataTable();


            string query = @"EXEC SHIPPING_FORM_INS '" + shipping.TYPE + "','" + shipping.BELGE_NO + "','" + shipping.SIPARIS_NO + "','"+shipping.IRS_NO+"','" +shipping.SERI_NO + "','" + shipping.STOK_KODU + "','" + shipping.MIKTAR1 + "','" + shipping.OLCU_BR1 + "','" + shipping.MIKTAR2 + "','" + shipping.OLCU_BR2 + "','" + shipping.ACIK2 + "','" + shipping.ACIK1 + "','" + shipping.ACIK3 + "','" + shipping.SERI_NO_3 + "','" + shipping.SERI_NO_4 + "','" + shipping.ACIKLAMA_4 + "','" + shipping.ACIKLAMA_5 + "'," + shipping.INS_USER_ID + ",'" + shipping.UPD_USER_ID + "','" + shipping.EXP_1 + "','" + shipping.EXP_2 + "','" + shipping.EXP_3 + "','"+shipping.GIRIS_DEPO + "','"+ shipping.CIKIS_DEPO + "','" + shipping.PLAKA + "','" + shipping.SOFOR + "'";

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
        [HttpPut("{inckey}")]
        public IActionResult ShippingUpdate(ShippingModelU shipping,int inckey)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SHIPPING_FORM_UPD '" + shipping.TYPE + "','" + shipping.BELGE_NO + "','" + shipping.SIPARIS_NO + "','" + shipping.IRS_NO + "','" + shipping.SERI_NO + "','" + shipping.STOK_KODU + "','" + shipping.MIKTAR1 + "','" + shipping.OLCU_BR1 + "','" + shipping.MIKTAR2 + "','" + shipping.OLCU_BR2 + "','" + shipping.ACIK2 + "','" + shipping.ACIK1 + "','" + shipping.ACIK3 + "','" + shipping.SERI_NO_3 + "','" + shipping.SERI_NO_4 + "','" + shipping.ACIKLAMA_4 + "','" + shipping.ACIKLAMA_5 + "','" + shipping.UPD_USER_ID + "','" + shipping.EXP_1 + "','" + shipping.EXP_2 + "','" + shipping.EXP_3 + "'," + inckey + ",'" + shipping.GIRIS_DEPO + "','" + shipping.CIKIS_DEPO + "','" + shipping.PLAKA + "','" + shipping.SOFOR + "'";

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
            if (table.Columns[0].ColumnName !="Column1")
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            else
            {
                return Ok();
            }
        }
        [HttpPut("durum/{acik}/{inckey}")]
        public IActionResult ShippingUpdateDurum(bool acik,int inckey)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SHIPPING_FORM_UPD_DURUM "+acik+","+ inckey;

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

            return Ok();
        }
        [HttpGet("kalembakiye/{sipno}/{stokkodu}/{miktar}/{belgeno}/{belgetip}")]
        public IActionResult GetKalemBakiye(string sipno, string stokkodu, int miktar,string belgeno,string belgetip)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SIP_KALEM_BAKIYE @SIPARIS_NO='" + sipno + "',@STOK_KODU='" + stokkodu+ "',@MIKTAR=" + miktar+ ",@BELGE_NO='" + belgeno+ "',@BELGE_TIP='" + belgetip+"'";

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
        [HttpDelete("{belgeno}/{barkod}")]
        public IActionResult ShippingDelete(string belgeno,string barkod)
        {


            DataTable table = new DataTable();
            string query = "";
            if (barkod != "0")
            {
                query = @"EXEC SHIPPING_FORM_DEL '" + belgeno + "','"+barkod+"'";
            }
            else
            {
                 query = @"EXEC SHIPPING_FORM_DEL '" + belgeno + "'";
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

            return Ok();
        }
        public class ShippingModel
        {
            public int? TYPE { get; set; }
            public string? BELGE_NO { get; set; }
            public string? SIPARIS_NO { get; set; }
            public string? SERI_NO { get; set; }
            public string? IRS_NO { get; set; }
            public string? STOK_KODU { get; set; }
            public string? MIKTAR1 { get; set; }
            public string? OLCU_BR1 { get; set; }
            public string? MIKTAR2 { get; set; }
            public string? OLCU_BR2 { get; set; }
            public string? ACIK1 { get; set; }
            public string? ACIK2 { get; set; }
            public string? ACIK3 { get; set; }
            public string? SERI_NO_3 { get; set; }
            public string? SERI_NO_4 { get; set; }
            public string? ACIKLAMA_4 { get; set; }
            public string? ACIKLAMA_5 { get; set; }
            public int? INS_USER_ID { get; set; }
            public int? UPD_USER_ID { get; set; }
            public string? EXP_1 { get; set; }
            public string? EXP_2 { get; set; }
            public string? EXP_3 { get; set; }
            public string? GIRIS_DEPO { get; set; }
            public string? CIKIS_DEPO { get; set; }
            public string? PLAKA { get; set; }
            public string? SOFOR { get; set; }
        }
       
        public class ShippingModelU
        {
            public int? TYPE { get; set; }
            public string? BELGE_NO { get; set; }
            public string? SIPARIS_NO { get; set; }
            public string? IRS_NO { get; set; }
            public string? SERI_NO { get; set; }
            public string? STOK_KODU { get; set; }
            public string? MIKTAR1 { get; set; }
            public string? OLCU_BR1 { get; set; }
            public string? MIKTAR2 { get; set; }
            public string? OLCU_BR2 { get; set; }
            public string? ACIK1 { get; set; }
            public string? ACIK2 { get; set; }
            public string? ACIK3 { get; set; }
            public string? SERI_NO_3 { get; set; }
            public string? SERI_NO_4 { get; set; }
            public string? ACIKLAMA_4 { get; set; }
            public string? ACIKLAMA_5 { get; set; }
            public int? UPD_USER_ID { get; set; }
            public string? EXP_1 { get; set; }
            public string? EXP_2 { get; set; }
            public string? EXP_3 { get; set; }
            public string? GIRIS_DEPO { get; set; }
            public string? CIKIS_DEPO { get; set; }
            public string? PLAKA { get; set; }
            public string? SOFOR {  get; set; }

        }
    }
}

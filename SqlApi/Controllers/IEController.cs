using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IEController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public IEController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("TOP/{makinekodu}")]
        public JsonResult GETIETOP(string makinekodu)
        {


            DataTable table = new DataTable();


            string query = "EXEC SP_HAT_GIRDI_IE_"+ makinekodu.Substring(0, 2)+" '"+ makinekodu+"'";

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
        [HttpGet("TOPDL/{makinekodu}")]
        public JsonResult GETIEDLTOP(string makinekodu)
        {


            DataTable table = new DataTable();


            string query = "EXEC SP_HAT_GIRDI_IE_" + makinekodu.Substring(0, 2) + "2 '" + makinekodu + "'";

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
        [HttpGet("acikisemirleri")]
        public JsonResult GETACIKLAR(string makinekodu)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_ACIK_ISEMRI";

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
        [HttpGet("USKDL/{makkodu}/{seri}/{miktar}/{miktar2}")]
        public JsonResult USKDL(string makkodu, string seri,string miktar,string miktar2)
        {

            DataTable table = new DataTable();
            string query = @"SP_URETIM_" + makkodu.Substring(0, 2) + "2 '" + makkodu + "','" + seri + "','"+miktar+"','"+miktar2+"'";


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
        [HttpGet("hatgirdi/{s}")]
        public JsonResult GETHat(string s)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_HAT_GIRDI '" + s+"'";
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
        [HttpGet("planlama")]
        public JsonResult GETPlan()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_NOVA_ISEMRI_PLAN_SEL";
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
        [HttpGet("hatgirdisira/{hk}/{cs}/{us}")]
        public JsonResult GETSira(string hk,string cs,string us)
        {


            DataTable table = new DataTable();


            string query = $"EXEC SP_HATGRD_SIRA_DEGIS '{hk}','{cs}','{us}'";
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
        [HttpGet("hatgirdi")]
        public JsonResult GETHatALL()
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_HAT_GIRDI";
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
        [HttpGet("hatgirdidetay/{s}")]
        public JsonResult GETDetayALL(string s)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_HAT_GIRDI_DETAY '"+s+"'";
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
        [HttpGet("mes3/{s}")]
        public JsonResult GETMES3(string s)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_MES3 '" + s + "'";
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
        [HttpGet("SARF/{seri}/{miktar}")]
        public JsonResult GETSARF(string seri,int miktar)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_SARF_DAGITIM '"+seri+"',"+miktar;

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
        [HttpGet("besleme")]
        public JsonResult GETBESLEME()
        {


            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_HAT_BESLEME WITH(NOLOCK)";

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
        [HttpGet("SARFHESAP/{seri}")]
        public JsonResult GETSARFHESAP(string seri)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_SARF_HESAP '" + seri + "'";

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
        [HttpGet("TRPZ/{isemri}/{stok}/{miktar}/{miktar2}")]
        public JsonResult GETIETOP(string isemri,string stok,string miktar,string miktar2)
        {


            DataTable table = new DataTable();


            string query = @"EXEC DENEME3 '" + isemri + "','"+stok+"','"+miktar+"','"+miktar2+"'";

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
        [HttpGet("isemrileri/{seri}")]
        public JsonResult GETISEMIRLERI(string seri)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_ISEMRI_BARKOD '" + seri + "'";

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
        [HttpGet("top/isemrileri/{seri}")]
        public JsonResult GETISEMIRLERITOP(string seri)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_ISEMRI_BARKOD_TOPLAM_SARF '" + seri + "'";

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
        [HttpGet("USK/{makkodu}/{stokkodu}/{isemri}/{miktar}/{miktar2}/{genislik}")]
        public JsonResult SETUSK(string makkodu,string isemri, string stokkodu,int miktar,string genislik,int miktar2)
        {


            DataTable table = new DataTable();
            string query = "";
            if (  makkodu.Substring(0, 2) != "MH" && makkodu.Substring(0, 2) != "TP")
            {
                
                query = @"SP_URETIM_"+ makkodu.Substring(0, 2) + " '" + makkodu + "','" + stokkodu + "','" + genislik + "','" + miktar + "','" + miktar2 + "'";
            }
            else
            {
                if(makkodu.Substring(0, 2) == "MH")
                {
                    query = @"SP_URETIM_" + makkodu.Substring(0, 2) + " '" + makkodu + "','" + isemri + "','" + miktar + "','" + miktar2 + "'";
                    
                }
                else
                {
                    query = @"SP_URETIM_" + makkodu.Substring(0, 2) + " '" + makkodu + "','" + isemri + "','0','" + miktar + "','" + miktar2 + "'";
                }
            
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
        [HttpGet("ikinci/{makkodu}/{stokkodu}/{miktar}/{miktar2}/{genislik}")]
        public JsonResult SETIKINCI(string makkodu, string stokkodu, int miktar, string genislik, int miktar2)
        {


            DataTable table = new DataTable();
            string  query = @"SP_URETIM_DNMPB'" + makkodu + "','" + stokkodu + "','" + genislik + "','" + miktar + "','" + miktar2 + "'";
          


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
        public class deneme2
        {
            public string SERI_NO { get; set; }
            public int KULL_MIKTAR { get; set; }
        }
        public class deneme1
        {
            public string ISEMRINO { get; set; }
            public int KULL_MIKTAR { get; set; }
        }
        [HttpGet("ALL/{makkodu}/{stokkodu}/{genislik}/{kapali}")]
        public JsonResult SETUSK(string makkodu, string stokkodu, string genislik,string kapali)
        {
            DataTable table = new DataTable();
            if (kapali == "null")
            {
                string query = @"EXEC NOVA_SP_HAT_GIRDI_IE_RCT_ALL '" + makkodu + "','" + stokkodu + "','" + genislik + "','H'";

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
                    string query2 = @"EXEC NOVA_SP_HAT_GIRDI_IE_RCT_ALL '" + makkodu + "','" + stokkodu + "','" + genislik + "','E'";
                    SqlDataReader sqlreader2;
                    using (SqlConnection mycon1 = new SqlConnection(sqldataSource))
                    {
                        mycon1.Open();
                        using (SqlCommand myCommand = new SqlCommand(query2, mycon1))
                        {
                            sqlreader2 = myCommand.ExecuteReader();
                            table.Load(sqlreader2);
                            sqlreader2.Close();
                            mycon1.Close();
                        }
                    }
                }
            }
            else
            {
                string query = @"EXEC NOVA_SP_HAT_GIRDI_IE_RCT_ALL '" + makkodu + "','" + stokkodu + "','" + genislik + "','" + kapali + "'";

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
            }

            

            return new JsonResult(table);
        }
        [HttpGet("ALL/{makkodu}/{kapali}")]
        public JsonResult SETALLMAK(string makkodu ,string kapali)
        {
            DataTable table = new DataTable();
            if (kapali == "null")
            {
                string query = @"EXEC NOVA_SP_HAT_GIRDI_IE_RCT_ALL_MAKKODU '" + makkodu + "','H'";

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
                    string query2 = @"EXEC NOVA_SP_HAT_GIRDI_IE_RCT_ALL_MAKKODU '" + makkodu + "','E'";
                    SqlDataReader sqlreader2;
                    using (SqlConnection mycon1 = new SqlConnection(sqldataSource))
                    {
                        mycon1.Open();
                        using (SqlCommand myCommand = new SqlCommand(query2, mycon1))
                        {
                            sqlreader2 = myCommand.ExecuteReader();
                            table.Load(sqlreader2);
                            sqlreader2.Close();
                            mycon1.Close();
                        }
                    }
                }
            }
            else
            {
                string query = @"EXEC NOVA_SP_HAT_GIRDI_IE_RCT_ALL_MAKKODU '" + makkodu + "','"+ kapali + "'";

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
            }



            return new JsonResult(table);
        }
        [HttpGet("control/{iekodu}")]
        public JsonResult Control(string iekodu)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_HAT_GIRDI_CTRL " + iekodu;

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
        [HttpGet("tpcontrol/{iekodu}")]
        public JsonResult TPControl(string iekodu)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_HAT_GIRDI_CTRL_TP " + iekodu;

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
        [HttpGet("sp/{hatkodu}/{kapali}")]
        public JsonResult HAT(string hatkodu, string kapali)
        {
            DataTable table = new DataTable();
            string query = "";
            if (kapali == "0")
            {
                query = @"EXEC SP_HAT_IE '" + hatkodu + "'";
            }
            else
            {
                query = @"EXEC SP_HAT_IE '" + hatkodu + "','" + kapali + "'";
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
        [HttpGet("hat/{hat}/{seri}/{userid}")]
        public ActionResult HatEkle(string hat, string seri, int userid)
        {
            string query2 = @"INSERT INTO TBL_HAT_GIRDI (HAT_KODU,SERI_NO,USER_ID) VALUES('" + hat + "','" + seri + "'," + userid + ")";


            string sqldataSource2 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader2;
            using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
            {
                mycon2.Open();
                using (SqlCommand myCommand2 = new SqlCommand(query2, mycon2))
                {
                    sqlreader2 = myCommand2.ExecuteReader();
                    sqlreader2.Close();
                    mycon2.Close();
                }
            }
            return new NoContentResult();
        }
        [HttpGet("hatprm/{hat}")]
        public ActionResult Hat(string hat)
        {
            string query2 = @"SELECT * FROM TBL_HAT_PRM WHERE HAT_KODU='"+hat+"'";
            DataTable table = new DataTable();

            string sqldataSource2 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader2;
            using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
            {
                mycon2.Open();
                using (SqlCommand myCommand2 = new SqlCommand(query2, mycon2))
                {
                    sqlreader2 = myCommand2.ExecuteReader();
                    table.Load(sqlreader2);
                    sqlreader2.Close();
                    mycon2.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpGet("{makinekodu}")]
        public JsonResult GETIE(string makinekodu)
        {


            DataTable table = new DataTable();


            string query = @"EXEC NOVA_SP_HAT_GIRDI_IE " + makinekodu;

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
        [HttpGet("bakiye/{hat}")]
        public JsonResult GETBAK(string hat)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_HAT_GIRDI_BAKIYE " + hat;

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
    }
}

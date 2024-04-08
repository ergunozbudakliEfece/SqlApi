using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
//using NetOpenX50;

using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiyatController : ControllerBase
    {
        private readonly StokContext _context;
        private readonly UserContext _context1;
        private readonly IConfiguration _configuration;
        public FiyatController(StokContext context, IConfiguration configuration, UserContext context1)
        {
            _context = context;
            _configuration = configuration;
            _context1 = context1;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context1.NOVA_VW_FIYATGIRISI2.ToList();
        }
        public void FIATKARSILASTIR()
        {

            DataTable table = new DataTable();


            string query = @"SELECT FIYATKODU FROM TBLSTOKFIATKOD WITH(NOLOCK) WHERE FIYATKODU<>'TANIMSIZ'";

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
            var t= JsonConvert.DeserializeObject<List<dynamic>>(new JsonResult(table).ToString());
            string query2 = @"SELECT FIYATKODU FROM TBL_NOVA_FIYAT_SIRA WITH(NOLOCK)";
            DataTable table2 = new DataTable();
            string sqldataSource2 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader2;
            using (SqlConnection mycon2 = new SqlConnection(sqldataSource2))
            {
                mycon2.Open();
                using (SqlCommand myCommand2 = new SqlCommand(query2, mycon2))
                {
                    sqlreader2 = myCommand2.ExecuteReader();
                    table2.Load(sqlreader2);
                    sqlreader2.Close();
                    mycon2.Close();
                }
            }
            var t2 = JsonConvert.DeserializeObject <List<dynamic>> (new JsonResult(table2).ToString());
            var fark=t.Except(t2).ToList();
           


        }
        [HttpGet("kur")]
        public IEnumerable GetKur()
        {
            return _context1.TBL_NOVA_KUR_YONETIMI.ToList();
        }
        [HttpGet("siraekle/{fiyatkodu}/{type}")]
        public JsonResult SIRAEKLE(string fiyatkodu,string type)
        {
            DataTable table = new DataTable();


            string query = @"EXEC SP_FIYATKODU_EKLE '" + fiyatkodu + "','"+type+"'";

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
        [HttpGet("kuroran")]
        public IEnumerable GetKurOran()
        {
            return _context1.TBL_NOVA_KUR_FAIZ_ORANI.OrderByDescending(x=>x.KAYIT_TARIHI).ToList();
        }
        [HttpGet("kuroran/{kur}/{oran}/{id}")]
        public ActionResult KurEkle(string kur,string oran,int id)
        {
            string query2 = @"INSERT INTO TBL_NOVA_KUR_FAIZ_ORANI (KUR,FAIZ_ORANI,KAYIT_YAPAN_KULLANICI_ID) VALUES(" + kur + "," + oran + ","+id+")";


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
        [HttpGet("kur/guncelle/{inckey}/{altlimit}/{ustlimit}/{bazpara}/{karsitpara}/{kur}/{vade}/{userid}")]
        public IEnumerable UpdateFiyat(int inckey, decimal altlimit, decimal ustlimit,int bazpara,int karsitpara,string kur,decimal vade,int userid)
        {
           
            string query1 = @"UPDATE TBL_NOVA_KUR_YONETIMI SET ALT_LIMIT=" + altlimit + ",UST_LIMIT=" + ustlimit + ",BAZ_PARA_BR=" + bazpara + ",KARSIT_PARA_BR=" + karsitpara + ",KUR=" + kur + ",VADE=" + vade + ",VADE_BR='GUN',DEGISIKLIK_YAPAN_ID="+ userid+" WHERE ID="+inckey;
                

                string sqldataSource1 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader1;
                using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                {
                    mycon1.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                    {
                        sqlreader1 = myCommand1.ExecuteReader();
                        sqlreader1.Close();
                        mycon1.Close();
                    }
                }
            string query2 = @"INSERT INTO TBL_NOVA_KUR_GECMISI (ID,ALT_LIMIT,UST_LIMIT,BAZ_PARA_BR,KARSIT_PARA_BR,KUR,VADE,VADE_BR,DEGISIKLIK_YAPAN_ID) VALUES("+inckey+"," + altlimit + "," + ustlimit + "," + bazpara + "," + karsitpara + "," + kur + "," + vade + ",'GUN'," + userid + ")";


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
            return null;
        }
        [HttpGet("exec/fiyatdurum")]
        public IEnumerable GetAllDurum()
        {
            string query1 = @"EXEC FIYAT_DURUM";
            List<FiyatDurumModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<FiyatDurumModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec;
        }
        public class FiyatDurumModel
        {
            public string FIYATKODU { get; set; }
            public string FIYAT2 { get; set; }
            public string FIYAT3 { get; set; }
        }
        [HttpGet("kur/ekle/{altlimit}/{ustlimit}/{bazpara}/{karsitpara}/{kur}/{vade}/{userid}")]
        public IEnumerable AddFiyat(decimal altlimit, decimal ustlimit, int bazpara, int karsitpara, string kur, decimal vade, int userid)
        {

            string query1 = @"INSERT INTO TBL_NOVA_KUR_YONETIMI (ALT_LIMIT,UST_LIMIT,BAZ_PARA_BR,KARSIT_PARA_BR,KUR,VADE,VADE_BR,KAYIT_YAPAN_ID) VALUES(" + altlimit + "," + ustlimit + "," + bazpara + "," + karsitpara + "," + kur + "," + vade + ",'GUN',"+ userid + ")";


            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
           
            return null;
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
        [HttpGet("exec/{fiyatkodu}")]
        public IEnumerable GetExec(string fiyatkodu)
        {
            string query = @"EXEC FIYATGIRISI '" + fiyatkodu + "'";
            List<FiatModel> stoklist = null;
            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                     stoklist = DataReaderMapToList<FiatModel>(sqlreader);
                    
                    sqlreader.Close();
                    mycon.Close();
                }
            }
            return stoklist;
        }
        [HttpPost]
        public IActionResult Create([FromBody] List<FiyatModel> item)
        {
            
            using (SqlConnection connection = new SqlConnection(
            _configuration.GetConnectionString("Connn")))
            {
                SqlCommand command = new SqlCommand("DELETE FROM TBL_NOVA_FIYAT_SIRA", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                for (int i = 0; i < item.Count(); i++)
                {
                    SqlCommand command1 = new SqlCommand("insert into TBL_NOVA_FIYAT_SIRA (SIRA_NO,FIYATKODU) VALUES(" + item[i].SIRA_NO +",'"+ item[i].FIYATKODU + "')", connection);
                    command1.Connection.Open();
                    command1.ExecuteNonQuery();
                    command1.Connection.Close();

                }
            }
          
            

            return new NoContentResult();
        }
        
        [HttpGet("{fiyatkodu}/{fiyat2}/{fiyat3}/{listekodu}/{userid}")]
        public IEnumerable Update(string fiyatkodu,string fiyat2, string fiyat3,string listekodu,int userid)
        {
           


            
                string query1 = @"EXEC UPDATESTOKFIAT '" + fiyatkodu + "','" + fiyat2 + "','" + fiyat3+"','"+listekodu+ "',"+userid;

                string sqldataSource1 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader1;
                using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                {
                    mycon1.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                    {
                        sqlreader1 = myCommand1.ExecuteReader();
                        sqlreader1.Close();
                        mycon1.Close();
                    }
                }
          


            return null;
        }
        [HttpGet("kur/sil/{inckey}/{userid}")]
        public IEnumerable Update(int inckey, int userid)
        {

            string query1 = @"EXEC SILSTOKFIAT '" + inckey + "'," + userid;

            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return null;
        }

        [HttpGet("aciklama/{fiyatkodu}/{aciklama}")]
        public IEnumerable UpdateAciklama(string fiyatkodu, string aciklama)
        {
            string query = @"SELECT * FROM TBL_NOVA_FIYAT_OZEL_KOSUL WHERE FIYATKODU='"+ fiyatkodu + "'";
            List<FiatAciklamaModel> stoklist = null;
            string sqldataSource = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader;
            using (SqlConnection mycon = new SqlConnection(sqldataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    sqlreader = myCommand.ExecuteReader();
                    stoklist = DataReaderMapToList<FiatAciklamaModel>(sqlreader);

                    sqlreader.Close();
                    mycon.Close();
                }
            }
            string query1 = "";
            if (stoklist.Count==0)
            {
                
                if (aciklama == "null")
                {
                   query1 = @"INSERT INTO TBL_NOVA_FIYAT_OZEL_KOSUL VALUES('" + fiyatkodu + "')";

                }
                else
                {
                    query1 = @"INSERT INTO TBL_NOVA_FIYAT_OZEL_KOSUL VALUES('" + fiyatkodu + "','" + aciklama + "')";
                }

                string sqldataSource1 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader1;
                using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                {
                    mycon1.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                    {
                        sqlreader1 = myCommand1.ExecuteReader();
                        sqlreader1.Close();
                        mycon1.Close();
                    }
                }
            }
            else
            {
                if (aciklama == "null")
                {
                  query1 = @"UPDATE TBL_NOVA_FIYAT_OZEL_KOSUL SET OZELKOSULLAR = NULL WHERE FIYATKODU='" + fiyatkodu + "'";

                }
                else
                {
                    query1 = @"UPDATE TBL_NOVA_FIYAT_OZEL_KOSUL SET OZELKOSULLAR='" + aciklama + "' WHERE FIYATKODU='" + fiyatkodu + "'";
                }
               
                string sqldataSource1 = _configuration.GetConnectionString("Connn");
                SqlDataReader sqlreader1;
                using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
                {
                    mycon1.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                    {
                        sqlreader1 = myCommand1.ExecuteReader();
                        sqlreader1.Close();
                        mycon1.Close();
                    }
                }
            }

           



            return null;
        }


        public class FiatAciklamaModel
        {
            public string FIYATKODU { set; get; }
            public string OZELKOSULLAR { set; get; }
        }
    }
}

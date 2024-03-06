using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public UserLoginController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration= configuration;
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _context.TBL_NOVA_LOGIN.ToList();
        }

        [HttpGet("{id}")]
        public LoginModel GetByID(int id)
        {
            var l = _context.TBL_NOVA_LOGIN.Where(x => x.USER_ID == id).ToList();
             return l[l.Count-1];
        }

        [HttpPut]
        public IActionResult Update([FromBody] LoginModel item)
        {
            var log = _context.TBL_NOVA_LOGIN.FirstOrDefault(t => t.LOG_ID==item.LOG_ID);
            log.LAST_ACTIVITY = item.LAST_ACTIVITY;
            _context.TBL_NOVA_LOGIN.Update(log);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpGet("exec/logoff/{logid}")]
        public IActionResult UpdateLogoff(int logid)
        {
            var log = _context.TBL_NOVA_LOGIN.FirstOrDefault(t => t.LOG_ID == logid);
            log.LAST_ACTIVITY = -2;
            _context.TBL_NOVA_LOGIN.Update(log);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpGet("exec/{id}")]
        public IEnumerable GetAllProc(int id)
        {
            string query1 = @"EXEC LOG_SITUATION "+id;
            List<ExecModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<ExecModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec;
        }
        
        [HttpGet("exec/lastseen")]
        public IEnumerable GetAllLast()
        {
            string query1 = @"EXEC CHECK_LAST_SEEN";
            List<LastSeenModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<LastSeenModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec.OrderByDescending(x=>x.USER_STATUS).ThenByDescending(x=>x.LOG_ID);
        }
        [HttpGet("exec/rank")]
        public IEnumerable GetAllRanks()
        {
            string query1 = @"EXEC MONTHLY_RANKING";
            List<rank> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<rank>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec;
        }
        [HttpGet("exec/ranks/{ay}/{yil}")]
        public IEnumerable GetR(int ay,int yil)
        {

            string query1 = @"EXEC MONTHLY_RANKINGS "+ay+","+yil;
            List<ranks> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<ranks>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec;
        }
        [HttpGet("exec/ranks")]
        public IEnumerable GetRR()
        {
            string query1 = @"EXEC MONTHLY_RANKINGS";
            List<ranks> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<ranks>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec;
        }
        public class rank
        {
            public string USER_NAME { get; set; }
            public int USER_ID { get; set; }
            public int SESSION_DURATION { get; set; }
            public decimal RATE { get; set; }
        }
        public class ranks
        {
            public string USER_NAME { get; set; }
            public int USER_ID { get; set; }
            public int SESSION_DURATION { get; set; }
            public decimal RATE { get; set; }
            public int MONTH { get; set; }
            public int YEAR { get; set; }
            public string DONEM { get; set; }
        }
        [HttpGet("exec/situation")]
        public IEnumerable GetAllSit()
        {
            string query1 = @"EXEC USER_SITUATION";
            List<UserSituation> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<UserSituation>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }

            return exec;
        }
        public class LastSeenModel
        {
            public int LOG_ID { get; set; }
            public int USER_ID { get; set; }
            public string USER_NAME { get; set; }
            public string USER_STATUS { get; set; }
            public string LAST_SEEN { get; set; }
            public string PLATFORM { get; set; }
            public DateTime LOGIN_DATE { get; set; }
            public DateTime LOGOUT_DATE { get; set; }
        }
        public class UserSituation
        {
            public int LOG_ID { get; set; }
            public string USER_NAME { get; set; }
            public string SITUATION { get; set; }
            public string LAST_ACTIVITY { get; set; }
            public DateTime LOGIN_DATE { get; set; }
        }
        public class ExecModel
        {
            public bool SITUATION { get; set; }
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
        [HttpPost]
        public IActionResult Insert([FromBody] LoginModel item)
        {
            LoginModel login=new LoginModel();
            login.USER_ID = item.USER_ID;
            login.PLATFORM = item.PLATFORM;

            _context.TBL_NOVA_LOGIN.Add(login);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}

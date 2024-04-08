using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static SqlApi.Controllers.FiyatController;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public AttendanceController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
          
        }
        [HttpGet("{userid}")]
        public IEnumerable GET(int userid)
        {

            string query1 = @"EXEC LAST_ATTENDANCE " + userid;
            List<AttendanceDurum> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<AttendanceDurum>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        [HttpGet("addcontrol/{userid}/{date}/{min}/{max}")]
        public IEnumerable GETControl(int userid,string date,int min,int max)
        {

            string query1 = @"EXEC RANGE_CONTROL " + userid+",'"+date+"',"+min+","+max;
            List<ControlModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<ControlModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        [HttpGet("addcontrol/{userid}/{date}/{min}/{max}/{inc}/{end}")]
        public IEnumerable GETControl(int userid, string date, int min, int max,int inc,int end)
        {

            string query1 = @"EXEC RANGE_CONTROL " + userid + ",'" + date + "'," + min + "," + max+","+inc+","+end;
            List<ControlModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<ControlModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        public class ControlModel
        {
            public string RESULT { get; set; }
        }
        [HttpGet("name/{userid}")]
        public IEnumerable GETNAME(int userid)
        {

            string query1 = @"EXEC USER_NAME_BY_ID " + userid;
            List<AttendanceName> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<AttendanceName>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        [HttpGet("{userid}/{devid}")]
        public ActionResult INSERT(int userid,int devid)
        {
            int dev = devid;
            string query1 = @"EXEC LAST_ATTENDANCE_INSERT " + userid+","+0;
            
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



            return new NoContentResult();
        }
        [HttpGet("manual/{userid}/{date}/{startend}")]
        public ActionResult MANUALINSERT(int userid, string date,string startend)
        {
            var s=date.Split(" ")[0];
            var ss = date.Split(" ")[0] + " "+date.Split(" ")[1];
            string query1 = @"EXEC LAST_ATTENDANCE_MANUAL_INSERT_T " + userid + ",'" + ss + "',0,'"+startend+"'";

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
            //var one=_context.TBL_PERSONAL_ATTENDANCE.FirstOrDefault(x => x.DATE == DateTime.Parse(date));
            //if(startend=="S" && one != null)
            //{var two= _context.TBL_PERSONAL_ATTENDANCE.FirstOrDefault(x => x.DATE > DateTime.Parse(date) && x.STARTEND=="E");
            //    var sss = false;
            //    if (two != null)
            //    {
            //        sss = two.DATE.ToString().Contains(ss);
            //    }
               
            //    if (two != null && sss==true)
            //    {
            //        two.START_INCKEY = one.INCKEY;
            //        _context.TBL_PERSONAL_ATTENDANCE.Update(two);
            //        _context.SaveChanges();
            //    }
               
            //}

            return new NoContentResult();
        }
        public class Personal
        {
            public string DATE { get; set; }
            public string USER_ID { get; set; }
            public string START_INCKEY { get; set; }
            public string STARTEND { get; set; }
            public string LASTINC { get; set; }
        }
        [HttpPost("manualend")]
        public ActionResult MANUALINSERTEND(Personal p)
        {
            var s = p.DATE.Split(" ")[0];
            var ss = p.DATE.Split(" ")[0] + " " + p.DATE.Split(" ")[1];
            string query1;
            if (p.START_INCKEY != "0")
            {
                query1 = @"EXEC LAST_ATTENDANCE_MANUAL_INSERT " + p.USER_ID + ",'" + ss + "',0,'" + p.STARTEND + "'," + p.START_INCKEY;
            }
            else
            {
                if (p.LASTINC != null)
                {
                    query1 = @"EXEC LAST_ATTENDANCE_MANUAL_INSERT " + p.USER_ID + ",'" + ss + "',0,'" + p.STARTEND + "',@LASTINC="+p.LASTINC;

                }
                else
                {
                    query1 = @"EXEC LAST_ATTENDANCE_MANUAL_INSERT " + p.USER_ID + ",'" + ss + "',0,'" + p.STARTEND + "'";
                }
                
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
            //var one=_context.TBL_PERSONAL_ATTENDANCE.FirstOrDefault(x => x.DATE == DateTime.Parse(date));
            //if(startend=="S" && one != null)
            //{var two= _context.TBL_PERSONAL_ATTENDANCE.FirstOrDefault(x => x.DATE > DateTime.Parse(date) && x.STARTEND=="E");
            //    var sss = false;
            //    if (two != null)
            //    {
            //        sss = two.DATE.ToString().Contains(ss);
            //    }

            //    if (two != null && sss==true)
            //    {
            //        two.START_INCKEY = one.INCKEY;
            //        _context.TBL_PERSONAL_ATTENDANCE.Update(two);
            //        _context.SaveChanges();
            //    }

            //}

            return new NoContentResult();
        }
        [HttpGet("delete/{userid}/{date}")]
        public ActionResult MANUALDELETE(int userid, string date)
        {
            
                
                

                
                        string query1 = @"DELETE FROM TBL_PERSONAL_ATTENDANCE WHERE USER_ID="+userid+ " AND CONVERT(varchar,DATE,23)='" + date+"'";
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
            string query2 = @"DELETE FROM TBL_PERSONAL_OFFDAY WHERE USER_ID=" + userid + " AND CONVERT(varchar,DATE,23)='" + date + "'";
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
        [HttpGet("deletemanual/{inckey}")]
        public ActionResult MANUALDELETE(int inckey)
        {

            string query1 = @"DELETE FROM TBL_PERSONAL_ATTENDANCE WHERE INCKEY=" + inckey;
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
           



            return new NoContentResult();
        }
        [HttpGet("find/{userid}")]
        public IEnumerable GetGraphData(int userid)
        {

            string query1 = @"EXEC FIND_PERSONAL_ATTENDANCE " + userid ;
            List<AttendanceGraph> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<AttendanceGraph>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec.OrderBy(x=>x.CODE).ThenBy(x=>x.START_DATE);
        }
        [HttpGet("exec/{detay}")]
        public JsonResult GetAttExec(int detay)
        {


            DataTable table = new DataTable();


            string query = @"EXEC SP_IK '"+detay+"'";

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
        public IActionResult Create([FromBody] PersonalOffdateModel off)
        {
            if (off == null)
            {
                return BadRequest();
            }
            if (off.START_DATE != null)
            {
                off.START_DATE = DateTime.Parse(off.START_DATE.ToString()).ToLocalTime();
            }
            if (off.END_DATE != null)
            {
                off.END_DATE = DateTime.Parse(off.END_DATE.ToString()).ToLocalTime();
            }
            _context.TBL_PERSONAL_OFFDAY.Add(off);
            _context.SaveChanges();

            return new NoContentResult();
        }

        public class PersonalOffdateModel
        {
            [Key]
            public int? INCKEY { get; set; }
            public int USER_ID { get; set; }
            public string DATE { get; set; }
            [Column(TypeName = "datetime")]
            public DateTime? START_DATE { get; set; }
            [Column(TypeName = "datetime")]
            public DateTime? END_DATE { get; set; }
            public int OFFDAY_CODE { get; set; }
            public int CONFIRM_CODE { get; set; }

        }
        [HttpGet("off/{userid}")]
        public IEnumerable GetAll(int userid)
        {
            return _context.TBL_PERSONAL_OFFDAY.Where(x=>x.USER_ID==userid).ToList();
        }
        [HttpGet("off/delete/{inckey}")]
        public IActionResult Delete(int inckey)
        {
            

            var item = _context.TBL_PERSONAL_OFFDAY.FirstOrDefault(t => t.INCKEY == inckey);
            if (item == null)
            {
                return NotFound();
            }
            


            _context.TBL_PERSONAL_OFFDAY.Remove(item);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpGet("off/deletedate/{userid}/{date}")]
        public IActionResult Delete(int userid,string date)
        {

            string query1 = @"DELETE FROM TBL_PERSONAL_OFFDAY WHERE USER_ID=" + userid + " AND CONVERT(varchar,DATE,23)='" + date + "'";
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

            return new NoContentResult();
        }
        [HttpPut]
        public IActionResult Update([FromBody] PersonalOffdateModel off)
        {


            
            if (off == null)
            {
                return NotFound();
            }



            _context.TBL_PERSONAL_OFFDAY.Update(off);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpGet("working")]
        public IEnumerable GetGraphDataWorking() { 

            string query1 = @"EXEC WORKING_TIME";
            List<AttendanceWorking> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<AttendanceWorking>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        [HttpGet("timing/{userid}")]
        public IEnumerable GetWorking(int userid)
        {
            string query1 = @"EXEC PERSONAL_ATT_CTRL "+userid;
            List<TimingModel> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<TimingModel>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }
            return exec;
        }
        public class TimingModel
        {
            public string SITUATION { get; set; }
        }
        [HttpGet("color")]
        public IEnumerable GetGraphColorg()
        {

            string query1 = @"SELECT * FROM TBL_ATT_COLOR_DATA";
            List<AttendanceGraphColor> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<AttendanceGraphColor>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        [HttpGet("offday")]
        public IEnumerable GetGraphOff()
        {

            string query1 = @"SELECT * FROM TBL_OFFDAY_DATA";
            List<AttendanceOff> exec = null;
            string sqldataSource1 = _configuration.GetConnectionString("Connn");
            SqlDataReader sqlreader1;
            using (SqlConnection mycon1 = new SqlConnection(sqldataSource1))
            {
                mycon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query1, mycon1))
                {
                    sqlreader1 = myCommand1.ExecuteReader();
                    exec = DataReaderMapToList<AttendanceOff>(sqlreader1);
                    sqlreader1.Close();
                    mycon1.Close();
                }
            }



            return exec;
        }
        [HttpPut("mesai")]
        public IActionResult UpdateMesai([FromBody] PersonalMesaiModel off)
        {
            DataTable table = new DataTable();
            var date = off.DATE.Value.ToLocalTime();
            string query = "";
            if (date.Hour==0 && date.Minute == 0)
            {
                query = @"DELETE FROM TBL_PERSONAL_ATTENDANCE WHERE INCKEY=" + off.INCKEY;
               
            }
            else
            {
                query = @"EXEC SP_NOVA_ATTENDANCE_UPD " + off.INCKEY + ",'" + date.Year + "-" + date.Month + "-" + date.Day + " " + date.Hour + ":" + date.Minute + "'";
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
            if (table.Columns.Count > 0)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            else
            {
                return Ok();
            }
        }
        public class AttendanceOff
        {
            public int OFFDAY_CODE { get; set; }
            public string OFFDAY_EXP1 { get; set; }
            public int DETAIL_CODE { get; set; }
           
        }
        public class AttendanceGraph
        {
            public int START_INCKEY { get; set; }
            public int END_INCKEY { get; set; }
            public int USER_ID { get; set; }
            public string DATE { get; set; }
            public DateTime? START_DATE { get; set; }
            public DateTime? END_DATE { get; set; }
            public int? TOTAL_MIN { get; set; }
            public string CODE { get; set; }
            public int? MAX { get; set; }
            public int? MIN { get; set; }
        }
        public class AttendanceGraphColor
        {
            public int INCKEY { get; set; }
            public int DETAIL_CODE { get; set; }
            public string HEX_CODE { get; set; }
            public string EXP_1 { get; set; }
        }
        public class AttendanceWorking
        {
            public string DATE { get; set; }
            public string EXP_1 { get; set; }
            public int CODE { get; set; }
        }
        public class AttendanceDurum
        {
            public int USER_ID { get; set; }
            public string STARTEND { get; set; }
            public DateTime DATE { get; set; }
        }
        public class AttendanceName
        {
          
            public string USER_FIRSTNAME { get; set; }
            public string USER_LASTNAME { get; set; }
        }
    }
}

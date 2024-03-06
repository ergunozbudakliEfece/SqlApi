using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SqlApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public UserController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable GetAll()
        {
            return _context.TBL_USER_DATA.ToList();
        }
        [HttpGet("images")]
        public JsonResult GETIMAGES()
        {

            DataTable table = new DataTable();


            string query = @"SELECT * FROM TBL_USER_IMAGES WITH(NOLOCK)";

            string sqldataSource = _configuration.GetConnectionString("Con");
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
        [HttpGet("name/{name}")]
        public IEnumerable GetAUserByName(string name)
        {
            return _context.TBL_USER_DATA.Where(x=>x.USER_NAME==name).ToList();
        }
        [HttpGet("link")]
        public IEnumerable GetAllLinks()
        {
            return _context.TBL_NOVA_LINK.ToList();
        }
        [HttpGet("auth")]
        public IEnumerable Get()
        {
            List<int> l = _context.TBL_USER_DATA.Where(x=>x.ACTIVE==true).Select(x => x.USER_ID).ToList();
            return _context.TBL_USER_AUTH.Where(x=>l.Contains(x.USER_ID)).OrderBy(c => c.USER_ID).ThenBy(c => c.MODULE_INCKEY).ToList();
        }
        [HttpGet("auth/{id}")]
        public async Task<IEnumerable> Get(int id)
        {
           
            return await _context.TBL_USER_AUTH.Where(x=>x.USER_ID==id).ToListAsync();
        }


        [HttpPut("auth/{id}/{module}")]
        public ActionResult UptAuth(int id,int module, [FromBody] UserAuth userauth)
        {
            var user = _context.TBL_USER_AUTH.FirstOrDefault(t => t.USER_ID == id && t.MODULE_INCKEY == module);
            if (user == null)
            {
                return NotFound();
            }

            user.USER_AUTH= userauth.USER_AUTH;
            user.SELECT_AUTH = userauth.SELECT_AUTH;
            user.UPDATE_AUTH = userauth.UPDATE_AUTH;
            user.INSERT_AUTH = userauth.INSERT_AUTH;
            user.DELETE_AUTH = userauth.DELETE_AUTH;

            _context.TBL_USER_AUTH.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
            
        }
        [HttpGet("exec/{name}/{password}")]
        public IActionResult UserExec(string name, string password)
        {
            
            DataTable table = new DataTable();


            string query = @"EXEC USER_AUTHENTICATION '" + name + "','" + password + "'";

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

            return new ObjectResult(table);
        }

        [HttpGet("{id:int}",Name = "GetUserById")]
        public IActionResult GetById(int id)
        {
            var item = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("active")]
        [Authorize]
        public IActionResult GetActives()
        {
            var item = _context.TBL_USER_DATA.Where(t => t.ACTIVE == true);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [Authorize]
        [HttpGet("{name}/{password}", Name = "GetUserByIdandPassword")]
        public IActionResult UserCheck (string name,string password)
        {
            var item = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_NAME == name && t.USER_PASSWORD==password);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("mail:{mail}", Name = "GetUserByMail")]
        public IActionResult GetById(string mail)
        {
            var item = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_MAIL == mail);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("{name}", Name = "GetUserByName")]
        public IActionResult GetByName(string name)
        {
            var item = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_NAME == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TBL_USER_DATA.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUserByName", new { name = item.USER_NAME }, item);
        }
        [HttpPost("link/{id}")]
        public IActionResult CreateLink(int id)
        {
            Link link=new Link();
            link.SITUATION = "F";
            link.DURATION = 5;
            link.VERIFICATION_ID = id;
            _context.TBL_NOVA_LINK.Add(link);
            _context.SaveChanges();

            return new NoContentResult();
        }
        [HttpPut("link/{id}")]
        public IActionResult UpdateLink(int id)
        {
            List<Link> link = _context.TBL_NOVA_LINK.Where(t => t.ID == id).ToList();
            link[0].SITUATION = "T";
            _context.TBL_NOVA_LINK.Update(link[0]);
            _context.SaveChanges();

            return new NoContentResult();
        }
        [HttpGet("link/{id}")]
        public IEnumerable GetLink(int id)
        {
            return _context.TBL_NOVA_LINK.Where(t=>t.ID==id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var user = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_ID == id);
            if (user == null)
            {
                return NotFound();
            }

            if(item.USER_PASSWORD!=null && item.USER_PASSWORD != "")
            {
                user.USER_PASSWORD = item.USER_PASSWORD;
            }
            if (item.USER_FIRSTNAME != null && item.USER_FIRSTNAME != "")
            {
                user.USER_FIRSTNAME = item.USER_FIRSTNAME;
            }
            if (item.USER_LASTNAME != null && item.USER_LASTNAME != "")
            {
                user.USER_LASTNAME = item.USER_LASTNAME;
            }
            if (item.USER_ROLE != null && item.USER_ROLE != "")
            {
                user.USER_ROLE = item.USER_ROLE;
            }
          
                user.ACTIVE = item.ACTIVE;
            
            if (item.USER_MAIL != null && item.USER_MAIL != "")
            {
                user.USER_MAIL = item.USER_MAIL;
            }



            

            _context.TBL_USER_DATA.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpPut("update/{name}")]
        public IActionResult UpdateUserInfe(string name, [FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var user = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_NAME == name);
            if (user == null)
            {
                return NotFound();
            }

            if (item.USER_PASSWORD != null && item.USER_PASSWORD != "")
            {
                user.USER_PASSWORD = item.USER_PASSWORD;
            }
            if (item.USER_FIRSTNAME != null && item.USER_FIRSTNAME != "")
            {
                user.USER_FIRSTNAME = item.USER_FIRSTNAME;
            }
            if (item.USER_LASTNAME != null && item.USER_LASTNAME != "")
            {
                user.USER_LASTNAME = item.USER_LASTNAME;
            }
            if (item.USER_ROLE != null && item.USER_ROLE != "")
            {
                user.USER_ROLE = item.USER_ROLE;
            }
            if (item.ACTIVE != null)
            {
                user.ACTIVE = item.ACTIVE;
            }
          
            

            if (item.USER_MAIL != null && item.USER_MAIL != "")
            {
                user.USER_MAIL = item.USER_MAIL;
            }





            _context.TBL_USER_DATA.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] User item)
        {
            if (item == null || item.USER_ID != id)
            {
                return BadRequest();
            }

            var user = _context.TBL_USER_DATA.FirstOrDefault(t => t.USER_ID == id);
            if (user == null)
            {
                return NotFound();
            }
            user.USER_ID = item.USER_ID;
            user.USER_NAME = item.USER_NAME;
            user.USER_PASSWORD = item.USER_PASSWORD;
            user.USER_FIRSTNAME = item.USER_FIRSTNAME;
            user.USER_LASTNAME = item.USER_LASTNAME;
            user.USER_ROLE = item.USER_ROLE;
            user.ACTIVE = item.ACTIVE;
            user.USER_MAIL = item.USER_MAIL;


            _context.TBL_USER_DATA.Remove(user);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}

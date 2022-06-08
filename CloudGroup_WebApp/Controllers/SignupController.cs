using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGroup_WebApp.Models;
using CloudGroup_WebApp.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudGroup_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly cloudgroupContext _cloudgroupcontext;
        public SignupController(cloudgroupContext context)
        {
            _cloudgroupcontext = context;
        }
        // GET: api/Signup
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_cloudgroupcontext.Users.ToList());
        }

        // GET: api/Signup/5
       /* [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Signup
        [HttpPost]
        public void Post([FromBody] UserRequest value)
        {
            Users obj = new Users();
            obj.Username = value.Username;
            obj.Password = value.Password;
            obj.CreatedAt = value.CreatedAt;
            _cloudgroupcontext.Users.Add(obj);
            _cloudgroupcontext.SaveChanges();
        }

        // PUT: api/Signup/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

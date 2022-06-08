using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudGroup_WebApp.Models;
using CloudGroup_WebApp.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CloudGroup_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static int userid;
        public static string username;
        private IConfiguration _config;
        private readonly cloudgroupContext _cloudgroupcontext;

        public LoginController(IConfiguration config, cloudgroupContext _cloud)
        {
            _config = config;
            _cloudgroupcontext = _cloud;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString,userid,username });
            }

            return response;
        }
        private string BuildToken(UserRequest user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserRequest Authenticate(LoginModel login)
        {
            UserRequest user = null;
            var ans = _cloudgroupcontext.Users.FirstOrDefault(o => o.Username == login.Username);
            if (ans != null)
            {
                if (ans.Username != null && ans.Password == login.Password)
                {
                    user = new UserRequest { Username = ans.Username, Password = ans.Password };
                    userid = ans.UId;
                    username = ans.Username;
                    return user;
                }
            }
           
           return null;
            
           

        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}

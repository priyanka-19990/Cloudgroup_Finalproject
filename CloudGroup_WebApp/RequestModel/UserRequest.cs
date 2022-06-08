using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGroup_WebApp.RequestModel
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CloudGroup_WebApp.Models
{
    public partial class Users
    {
        public Users()
        {
            Documents = new HashSet<Documents>();
            Folders = new HashSet<Folders>();
        }

        public int UId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ICollection<Documents> Documents { get; set; }
        public ICollection<Folders> Folders { get; set; }
    }
}

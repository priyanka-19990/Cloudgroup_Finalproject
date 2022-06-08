using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGroup_WebApp.RequestModel
{
    public class FolderRequest
    {
        public string FName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

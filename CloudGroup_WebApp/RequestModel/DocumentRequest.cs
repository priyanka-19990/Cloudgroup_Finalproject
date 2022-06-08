using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGroup_WebApp.RequestModel
{
    public class DocumentRequest
    {
        public string DName { get; set; }
        public string ContentType { get; set; }
        public int? Size { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        //public int? FolderId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

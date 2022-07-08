using System;
using System.Collections.Generic;

namespace CloudGroup_WebApp.Models
{
    public partial class Documents
    {
        public int DId { get; set; }
        public string DName { get; set; }
        public string ContentType { get; set; }
        public int? Size { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FolderId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFavourite { get; set; }
        public Users CreatedByNavigation { get; set; }
        public Folders Folder { get; set; }
        //public int F_Id { get; internal set; }
    }
}

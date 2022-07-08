using System;
using System.Collections.Generic;

namespace CloudGroup_WebApp.Models
{
    public partial class Folders
    {
        public Folders()
        {
            Documents = new HashSet<Documents>();
        }

        public int FId { get; set; }
        public string FName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFavourite { get; set; }
        public Users CreatedByNavigation { get; set; }
        public ICollection<Documents> Documents { get; set; }
    }
}

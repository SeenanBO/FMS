using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.Models
{
    public class Files : BaseModel
    {
        public string FileBarcode { get; set; }
        public int BoxId { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public bool IsDataEntry { get; set; }
        public Boxes Boxes { get; set; }
        public virtual ICollection<FilesImages> FilesImages { get; set; }

    }
}
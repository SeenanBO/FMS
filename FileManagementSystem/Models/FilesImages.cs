using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.Models
{
    public class FilesImages : BaseModel
    {
        public string ImgPath { get; set; }
        public int FileId { get; set; }
        public virtual ICollection<Files> Files { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileManagementSystem.Models
{
    public class File_Images 
    {
        [Key]
        public int Id { get; set; }
        public int FileId { get; set; }
        public Files File { get; set; }
        public int FileImageId { get; set; }
        public FilesImages FileImages { get; set; }
    }
}
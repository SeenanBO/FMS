using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.ViewModels
{
    public class FileViewModel
    {
        public List<BFile> FilesList { get; set; }
    }

    public class BFile
    {
        public int Id { get; set; }
        public string FileBarcode { get; set; }
        public List<string> ImagePathList { get; set; }
        public string ImagePath { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
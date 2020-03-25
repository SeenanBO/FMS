using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.ViewModels
{

    public class BoxesViewModel
    {
        public List<Box> BoxesList { get; set; }
    }

    public class Box
    {
        public int Id { get; set; }
        public string BoxBarcode { get; set; }
        public string ImagePath { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public int FileCount { get; set; }
    }
}
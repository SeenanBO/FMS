using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
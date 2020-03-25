using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.Models
{
    public class WorkOrders : BaseModel
    { 
        public string WorkOrderNumber { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCompleted { get; set; }
        public string ImgPath { get; set; }
        public virtual ICollection<Boxes> Boxes { get; set; }
    }

}
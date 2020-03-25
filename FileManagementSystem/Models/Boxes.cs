using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.Models
{
    public class Boxes : BaseModel
    {
        public string BoxBarcode { get; set; }
        public string ImgPath { get; set; }
        public int WorkOrderId { get; set; }
        public bool IsDataEntry { get; set; }
        public WorkOrders WorkOrders { get; set; }
        public virtual ICollection<Files> Files { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManagementSystem.ViewModels
{
    public class WorkOrderViewModel
    {
        public List<WorkOrderList> WorkOrderList { get; set; }     
    }

    public class WorkOrderList
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public int BoxCount { get; set; }
        public int FileCount { get; set; }
        public bool IsFinished { get; set; }
    }
}
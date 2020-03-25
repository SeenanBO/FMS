using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileManagementSystem.ViewModels;

namespace FileManagementSystem.ViewModels
{

    public class EditBoxViewModel
    {
        public Box Box { get; set; }
        public List<BFile> FileList { get; set; }

    }
}
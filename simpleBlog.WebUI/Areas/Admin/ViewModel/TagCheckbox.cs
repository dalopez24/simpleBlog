using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simpleBlog.WebUI.Areas.Admin.ViewModel
{
    public class TagCheckbox
    {
        public int? TagId { get; set; }
        public string Name { get; set; }
        public bool isChecked { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace simpleBlog.WebUI.Areas.Admin.ViewModel
{
    public class PostViewModel
    {
        public bool isNew { get; set; }
        public int? PostId { get; set; }

        [Required, MaxLength(128)]
        public string Title { get; set; }
        [Required, MaxLength(128)]
        public string Slug { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public IList<TagCheckbox> Tags { get; set; }
    }
}
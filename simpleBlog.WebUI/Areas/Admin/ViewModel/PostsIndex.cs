using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using simpleBlog.Model;
using simpleBlog.WebUI.Infrastructure;

namespace simpleBlog.WebUI.Areas.Admin.ViewModel
{
    public class PostsIndex
    {
        public PagedData<Post> Posts  { get; set; }
    }
}
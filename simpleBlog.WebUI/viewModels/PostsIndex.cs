using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using simpleBlog.WebUI.Infrastructure;
using simpleBlog.Model;
namespace simpleBlog.WebUI.viewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }
}
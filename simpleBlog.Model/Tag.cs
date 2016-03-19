using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleBlog.Model
{
    public class Tag
    {
        public Tag()
        {
            this.Posts = new List<Post>();
        }
        public virtual int TagId { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Name { get; set; }

        public IList<Post> Posts { get; set; }

    }
}

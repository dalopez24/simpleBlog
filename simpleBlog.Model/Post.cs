using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace simpleBlog.Model
{
    public class Post
    {
      
        public Post()
        {
            this.Tags = new List<Tag>();
        }


        public virtual int PostId { get; set; }

        public virtual int UserId { get; set; }
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Slug { get; set; }
        [Required]
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set;}
        public virtual DateTime? DeletetAt { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public virtual bool IsDeleted { get { return DeletetAt != null; } }
        public virtual User User { get; set; }

    }
}

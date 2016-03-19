using simpleBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleBlog.DAL.Data;

namespace simpleBlog.DAL.Repositories
{
    public class TagRepository : BaseRepository<Tag>
    {
        public TagRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        } 
    }
}

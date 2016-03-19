using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleBlog.Model;
using simpleBlog.DAL.Data;

namespace simpleBlog.DAL.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}

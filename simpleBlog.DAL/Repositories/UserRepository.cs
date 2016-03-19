using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleBlog.DAL.Data;
using simpleBlog.Model;

namespace simpleBlog.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DataContext context) : base(context)
        {
            if(context == null)
                throw new ArgumentNullException();

           
        }
    }
}

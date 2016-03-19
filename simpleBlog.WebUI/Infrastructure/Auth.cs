using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using simpleBlog.Model;
using simpleBlog.Contracts.Repositories;
using System.Data.Entity;
using Microsoft.Practices.ServiceLocation;

namespace simpleBlog.WebUI.Infrastructure
{
    public class Auth
    {

        IBaseRepository<User> _users;

        public Auth(IBaseRepository<User> user)
        {
            this._users = user;
        }
        
        private const string UserKey = "simpleBlog.Atuh.UserKey";
        public virtual User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;

                if (user == null)
                {

                    user = _users.GetAll().FirstOrDefault(u => u.Name == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user;

                }

                return user;
            }
        }

    }
}
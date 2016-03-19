using System.ComponentModel.DataAnnotations;

namespace simpleBlog.Model
{
    public class User
    {

        public virtual int UserId { get; set; }

        public virtual int RoleId { get; set; }

        [Required]
        public virtual string Name { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public virtual string Password { get; set; }

        public virtual Role Roles { get; set; }



        public virtual void SetPassword(string pass)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(pass, 15);
        }
        public virtual bool GetPassword(string pass)
        {
            return BCrypt.Net.BCrypt.Verify(pass, Password);
        }


    }
}

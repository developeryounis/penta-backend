using System.Collections.Generic;

namespace Penta.Service.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int? EnteredBy { get; set; }

        public virtual ApplicationUser Parent { get; set; }

        public virtual ICollection<ApplicationUser> Children { get; set; }

        public virtual ICollection<UserPrivilege> Privileges { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}

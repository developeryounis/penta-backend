using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Models
{
    public class Privilege
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserPrivilege> Privileges { get; set; }
    }
}

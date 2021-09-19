namespace Penta.Service.Models
{
    public class UserPrivilege
    {
        public int UserId { get; set; }

        public int PrivilegeId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Privilege Privilege { get; set; }
    }
}

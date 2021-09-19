using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Penta.Service.Models.ModelConfigurations
{
    public class UserPrivilegeConfiguration : IEntityTypeConfiguration<UserPrivilege>
    {
        public void Configure(EntityTypeBuilder<UserPrivilege> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PrivilegeId });

            builder.HasOne(x => x.User)
                .WithMany(m => m.Privileges)
                .HasForeignKey(f => f.UserId);

            builder.HasOne(x => x.Privilege)
                .WithMany(m => m.Privileges)
                .HasForeignKey(f => f.PrivilegeId);

            builder.HasData(new UserPrivilege()
            {
                PrivilegeId = 1,
                UserId = 1
            }, new UserPrivilege()
            {
                PrivilegeId = 2,
                UserId = 1
            }, new UserPrivilege()
            {
                PrivilegeId = 3,
                UserId = 1
            }, new UserPrivilege()
            {
                PrivilegeId = 4,
                UserId = 1
            }, new UserPrivilege()
            {
                PrivilegeId = 1,
                UserId = 2
            }, new UserPrivilege()
            {
                PrivilegeId = 4,
                UserId = 2
            });
        }
    }
}

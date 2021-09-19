using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Penta.Service.Models.ModelConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1000);

            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.EnteredBy)
                .IsRequired(false);

            builder.HasMany(x => x.Privileges)
                .WithOne(c => c.User)
                .HasForeignKey(d => d.UserId);


            builder.HasMany(x => x.Students)
                .WithOne(c => c.User)
                .HasForeignKey(d => d.Entered);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(f => f.EnteredBy);

            builder.HasData(new ApplicationUser()
            {
                Name = "Amr",
                Password = "Amr",
                Username = "developer.younis@gmail.com",
                Id = 1
            }, new ApplicationUser()
            {
                Name = "Kareem",
                Password = "Kareem",
                Username = "Kiibrahim@momra.gov.sa",
                Id = 2
            });

        }
    }
}

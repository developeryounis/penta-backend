using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Models.ModelConfigurations
{
    public class PrivilegeConfiguration : IEntityTypeConfiguration<Privilege>
    {
        public void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Privileges)
                .WithOne(o => o.Privilege)
                .HasForeignKey(f => f.PrivilegeId);


            builder.HasData(new Privilege()
            {
                Id = 1,
                Name = "Add"
            },
             new Privilege()
             {
                 Id = 2,
                 Name = "Update"
             },
             new Privilege()
             {
                 Id = 3,
                 Name = "Delete"
             },
             new Privilege()
             {
                 Id = 4,
                 Name = "Search"
             });
        }
    }
}

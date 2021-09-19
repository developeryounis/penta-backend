using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Models.ModelConfigurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.ArabicName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.EnglishName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Entered)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.EnteredBy)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(m => m.Students)
                .HasForeignKey(f => f.EnteredBy);
        }
    }
}

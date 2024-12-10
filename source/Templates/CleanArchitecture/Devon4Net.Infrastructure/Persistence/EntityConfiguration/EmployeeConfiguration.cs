using Devon4Net.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Devon4Net.Infrastructure.Persistence.EntityConfiguration;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
                builder.Property(e => e.Id)
                        .IsRequired()
                        .HasMaxLength(255);
                builder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(255);
                builder.Property(e => e.Surname)
                        .IsRequired()
                        .HasMaxLength(255);
                builder.Property(e => e.Mail)
                        .IsRequired()
                        .HasMaxLength(255);
        }
}
using Devon4Net.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.Persistence;

/// <summary>
/// Employee database context definition
/// </summary>
public class EmployeeContext(DbContextOptions<EmployeeContext> options) : DbContext(options)
{
    private const string Schema = "employees";

    public DbSet<Employee> Employee => Set<Employee>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly).HasDefaultSchema(Schema);
}
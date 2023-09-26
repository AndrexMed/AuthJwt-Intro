using System.Reflection;
using AuthJwt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthJwt.Models.Data;

public partial class AuthJwtContext : DbContext
{
    public AuthJwtContext()
    {
    }

    public AuthJwtContext(DbContextOptions<AuthJwtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=myCnx");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

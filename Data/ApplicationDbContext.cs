using AIM.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIM.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<ApplicationUser> Registers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = StaticUserRoles.USER, NormalizedName = StaticUserRoles.USER.ToUpper() },
            new IdentityRole { Name = StaticUserRoles.ADMIN, NormalizedName = StaticUserRoles.ADMIN.ToUpper() },
            new IdentityRole { Name = StaticUserRoles.OWNER, NormalizedName = StaticUserRoles.OWNER.ToUpper() }
        );
    }
}

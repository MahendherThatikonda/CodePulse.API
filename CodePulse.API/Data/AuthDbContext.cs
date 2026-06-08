using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
  public class AuthDbContext : IdentityDbContext
  {
    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      var readerRoleId = "6892ad46-648f-4d76-b9fd-83ed1654b250";
      var writerRoleId = "086d0acd-9b07-43ff-8e04-62559b712379";
      //Create Reader and Writer role
      var roles = new List<IdentityRole>
      {
        new IdentityRole()
        {
          Id=readerRoleId,
          Name="Reader",
          NormalizedName="Reader".ToUpper(),
          ConcurrencyStamp=readerRoleId,
        },
        new IdentityRole()
        {
          Id =writerRoleId,
          Name="writer",
          NormalizedName="writer".ToUpper(),
          ConcurrencyStamp=writerRoleId,
        }
      };
         //seed the roles
         builder.Entity<IdentityRole>().HasData(roles);
      var adminUserId = "5fe58d0f-e37c-44a5-8130-30c948094726";
      //Create an Admin User
      var admin = new IdentityUser()
      {
        Id=adminUserId,
        UserName="admin@codepulse.com",
        Email="admin@codepulse.com",
        NormalizedEmail="admin@codepulse.com".ToUpper(),
        NormalizedUserName="admin@codepulse.com".ToUpper(),
      };

      admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
         
      builder.Entity<IdentityUser>().HasData(admin);
      //Give roles to admin
      var adminRoles = new List<IdentityUserRole<string>>()
      {
        new()
        {
          UserId=adminUserId,
          RoleId=readerRoleId,
        },
        new()
        {
          UserId=adminUserId,
          RoleId=readerRoleId,
        }

      };

      builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
  }
}

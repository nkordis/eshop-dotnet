using EShop.DataAccess.Data;
using EShop.Models.Models;
using EShop.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.DataAccess.DbInitializer;

public class DbInitializer(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext db
        ) : IDbInitializer
{

    public void Initialize()
    {
        //migrations if they are not applied
        try
        {
            if (db.Database.GetPendingMigrations().Count() > 0)
            {
                db.Database.Migrate();
            }

        }
        catch (Exception ex)
        {

        }

        //create roles if they are not created
        if (!roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

            //if roles are not created, then we will create admin user as well
            userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
                Name = "Admin",
                PhoneNumber = "1234567890",
                StreetAddress = "test street address",
                State = "Statest",
                PostalCode = "23422",
                City = "CityTest"
            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser user = db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@test.com");
            userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }

        return;
    }
}

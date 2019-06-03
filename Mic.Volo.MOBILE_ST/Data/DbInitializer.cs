using Mic.Volo.MOBILE_ST.Data.AppDbCont;
using Mic.Volo.MOBILE_ST.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data
{
    public static class DbInitializer
    {
        public static void SeedDatabase(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            System.Console.WriteLine("Seeding");
            var companies = new List<Company>
            {
                new Company { Name = "Samsung"},
                new Company { Name = "LG" },
                new Company { Name = "Apple"}
            };

            var phones = new List<Phone>
            {
                new Phone
                {
                    Name="Iphone 6S",
                    Price=2000,
                    ShortDescription="Smart random text",
                    LongDescription="Change this text",
                    Company=companies[0],
                    ImageUrl="iphone_1.jpg",
                    IsPhoneOfTheWeek=true
                },
                new Phone
                {
                    Name="Iphone 8",
                    Price=6000,
                    ShortDescription="Smart random text",
                    LongDescription="Change this text",
                    Company=companies[1],
                    ImageUrl="iphone_2.jpg",
                    IsPhoneOfTheWeek=true
                },
                new Phone
                {
                    Name="Iphone 4s",
                    Price=1000,
                    ShortDescription="Smart random text",
                    LongDescription="Change this text",
                    Company=companies[3],
                    ImageUrl="iphone_3.jpg",
                    IsPhoneOfTheWeek=true
                }
            };
            if(!context.Companies.Any()&&!context.Phones.Any())
            {
                context.Companies.AddRange(companies);
                context.Phones.AddRange(phones);
                context.SaveChanges();
            }
            IdentityUser usr = null;
            string userEmail=configuration["Admin:Email"]??"admin@gmail.com";
            string userName = configuration["Admin:UserName"] ?? "admin";
            string password = configuration["Admin:Password"] ?? "Artyom.1234";

            if (!context.Users.Any())
            {
                usr = new IdentityUser
                {
                    Email = userEmail,
                    UserName = userName
                };
                userManager.CreateAsync(usr, password);
            }
            if(!context.UserRoles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if(usr==null)
            {
                usr = userManager.FindByEmailAsync(userEmail).Result;
            }
            if(!userManager.IsInRoleAsync(usr,"Admin").Result)
            {
                userManager.AddToRoleAsync(usr, "Admin");
            }
            context.SaveChanges();
            System.Console.WriteLine("End");

        }
    }
}

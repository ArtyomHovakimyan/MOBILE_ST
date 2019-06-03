using Mic.Volo.MOBILE_ST.Data.AppDbCont;
using Mic.Volo.MOBILE_ST.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.ViewModel
{
    public static class SeedData
    {

        private static Dictionary<string, Company> companies;
        public static Dictionary<string, Company> Companies
        {
            get
            {
                if (companies == null)
                {
                    var compList = new Company[]
                    {
                        new Company { Name = "Apple" },
                        new Company { Name = "HTC"}
                    };

                    companies = new Dictionary<string, Company>();

                    foreach (Company item in compList)
                    {
                        companies.Add(item.Name, item);
                    }
                }

                return companies;
            }
        }
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();


            if (context.Phones.Any())
            {
                return;   // DB has been seeded
            }
            var phones = new Phone[]
            {
                    new Phone
                    {
                        Name = "Galaxy S",
                        Company=Companies["Apple"],
                        Price = 5200M,
                        ShortDescription = " change this text",
                        ImageUrl = "iphone_3.jpg",
                        IsPhoneOfTheWeek=false,
                        LongDescription="sfsdfsfsd"

                    },
                    new Phone
                    {
                        Name = "Galaxy",
                        Company=Companies["Apple"],
                        Price = 6600M,
                        ShortDescription = "change this text",
                        ImageUrl = "iphone_4.jpg",
                        IsPhoneOfTheWeek=true,
                        LongDescription="sfsdfsfsd"

                    },
                    new Phone
                    {
                        Name = "Note 9 ",
                        Company=Companies["HTC"],
                        Price = 3900M,
                        ShortDescription = "change this text.",
                        ImageUrl = "iphone_1.jpg",
                        IsPhoneOfTheWeek=true,
                        LongDescription="sfsdfsfsd"
                        


                    }
            };
            foreach (var p in phones)
            {
                context.Phones.Add(p);
            }
            context.SaveChanges();

        }
    }
}

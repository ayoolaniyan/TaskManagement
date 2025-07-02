using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new User()
                    {
                        Name = "Mary"
                    },
                    new User()
                    {
                        Name = "Job"
                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

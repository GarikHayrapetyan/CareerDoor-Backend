using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class SeedGetTogether
    {
        public static async Task Seed(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.GetTogethers.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var getTogethers = new List<GetTogether>
                {
                    new GetTogether
                    {
                        Title = "Workshop IT",
                        Description = "Finding a new job",
                        Date = DateTime.Now.AddMonths(2),
                        Link = "https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0234?f1url=%3FappId%3Droslyn%26k%3Dk(CS0234)",
                        PassCode = "axc14sd",
                        Attendees = new List<GetTogetherAttendee>
                        {
                            new GetTogetherAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            }
                        }
                    },
                    new GetTogether
                    {
                        Title = ".Net",
                        Description = "Get start with .Net",
                        Date = DateTime.Now.AddMonths(10),
                        Link = "https://dotnet.microsoft.com/apps/aspnet",
                        PassCode = "bxc15fd",
                        Attendees = new List<GetTogetherAttendee>
                        {
                            new GetTogetherAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new GetTogetherAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new GetTogether
                    {
                        Title = "Java",
                        Description = "Why Java?",
                        Date = DateTime.Now.AddMonths(3).AddDays(15),
                        Link = "https://www.oracle.com/java/technologies/",
                        PassCode = "qwser98e",
                        Attendees = new List<GetTogetherAttendee>
                        {
                            new GetTogetherAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new GetTogetherAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    }
                };
                await context.GetTogethers.AddRangeAsync(getTogethers);
                await context.SaveChangesAsync();
            }
        }
    }
}
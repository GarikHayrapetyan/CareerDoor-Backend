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

            if (!userManager.Users.Any() && !context.GetTogethers.Any())
            {
               

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

           if (!userManager.Users.Any() && !context.Jobs.Any())
           {

                var jobs = new List<Job> {
                    new Job{
                        Title=".Net core",
                        Type="Full-time",
                        Description=@"DXC Technology (NYSE: DXC) is the world’s leading independent, end-to-end IT services company,
                                    helping clients harness the power of innovation to thrive on change. Created by the merger of CSC and the Enterprise Services business
                                    of Hewlett Packard Enterprise, DXC Technology serves nearly 6,000 private and public sector clients across 70 countries. The company’s 
                                    technology independence, global talent and extensive partner alliance combine to deliver powerful next-generation IT services and solutions.
                                    DXC Technology is recognized among the best corporate citizens globally. For more information, visit http://www.dxc.technology/.
                                    We are looking for experienced and junior candidates, aiming to build a healthy mix of seniority and technology skills in the team.
                                    Net Developer
                                    Job Responsibilities Of This Role Will Include
                                    Develop high quality products and / or sub - systems to agreed deadlines,
                                    Prepares and executes unit / component tests and carries out unit tests,
                                    Investigation and resolution(where necessary) of customer issues,
                                    Participates in code reviews, identifying risks, and escalating issues when necessary,
                                    Collaborate with other Developers, Analysts, QA to ensure high - quality software,
                                    Identifies best practice improvements and makes changes as agreed with Team Leader,
                                    Provides information to the project leader to assist with project planning,
                                    Provides input on technical direction.
                                    We Require
                                    At least 2 years of experience with.NET,
                                    Good knowledge Microsoft.NET Framework 4.0 / 4.5,
                                    Experience in ADO.NET, MVC 3 / 4, API rest, Json,
                                    Base knowledge of Microsoft Visual Studio, TFS and GIT code repository,
                                    TSQL(SQL Server 2012 and above),
                                    Good communication skills in English language(reading, writing, speaking),
                                    BSc / MSc degree in Computer Science, Information Systems or equivalent.",
                           Date = DateTime.Now,
                           Company = "PJATK",
                           Function = "Backend",
                           Industry = "IT",
                           Location = "Warsaw",
                           EmployeeCount = "100+",
                           Candidates = new List<JobCandidate>{
                                new JobCandidate
                                {
                                    AppUser = users[0],
                                    IsEmployer = true
                                },
                                new JobCandidate
                                {
                                    AppUser = users[1],
                                    IsEmployer = false
                                },
                           }
                    },

                    new Job{
                        Title="Python",
                        Type="Part-time",
                        Description=@"DXC Technology (NYSE: DXC) is the world’s leading independent, end-to-end IT services company,
                                    helping clients harness the power of innovation to thrive on change. Created by the merger of CSC and the Enterprise Services business
                                    of Hewlett Packard Enterprise, DXC Technology serves nearly 6,000 private and public sector clients across 70 countries. The company’s 
                                    technology independence, global talent and extensive partner alliance combine to deliver powerful next-generation IT services and solutions.
                                    DXC Technology is recognized among the best corporate citizens globally. For more information, visit http://www.dxc.technology/.
                                    We are looking for experienced and junior candidates, aiming to build a healthy mix of seniority and technology skills in the team.
                                    Net Developer
                                    Job Responsibilities Of This Role Will Include
                                    Develop high quality products and / or sub - systems to agreed deadlines,
                                    Prepares and executes unit / component tests and carries out unit tests,
                                    Investigation and resolution(where necessary) of customer issues,
                                    Participates in code reviews, identifying risks, and escalating issues when necessary,
                                    Collaborate with other Developers, Analysts, QA to ensure high - quality software,
                                    Identifies best practice improvements and makes changes as agreed with Team Leader,
                                    Provides information to the project leader to assist with project planning,
                                    Provides input on technical direction.
                                    We Require
                                    At least 2 years of experience with.NET,
                                    Good knowledge Microsoft.NET Framework 4.0 / 4.5,
                                    Experience in ADO.NET, MVC 3 / 4, API rest, Json,
                                    Base knowledge of Microsoft Visual Studio, TFS and GIT code repository,
                                    TSQL(SQL Server 2012 and above),
                                    Good communication skills in English language(reading, writing, speaking),
                                    BSc / MSc degree in Computer Science, Information Systems or equivalent.",
                           Date = DateTime.Now,
                           Company = "Boom",
                           Function = "Backend",
                           Industry = "IT",
                           Location = "Krakow",
                           EmployeeCount = "150",
                           Candidates = new List<JobCandidate>{
                                new JobCandidate
                                {
                                    AppUser = users[2],
                                    IsEmployer = true
                                },
                                new JobCandidate
                                {
                                    AppUser = users[1],
                                    IsEmployer = false
                                },
                           }

                    },

                        new Job{
                        Title="Angular",
                        Type="Part-time",
                        Description=@"DXC Technology (NYSE: DXC) is the world’s leading independent, end-to-end IT services company,
                                    helping clients harness the power of innovation to thrive on change. Created by the merger of CSC and the Enterprise Services business
                                    of Hewlett Packard Enterprise, DXC Technology serves nearly 6,000 private and public sector clients across 70 countries. The company’s 
                                    technology independence, global talent and extensive partner alliance combine to deliver powerful next-generation IT services and solutions.
                                    DXC Technology is recognized among the best corporate citizens globally. For more information, visit http://www.dxc.technology/.
                                    We are looking for experienced and junior candidates, aiming to build a healthy mix of seniority and technology skills in the team.
                                    Net Developer
                                    Job Responsibilities Of This Role Will Include
                                    Develop high quality products and / or sub - systems to agreed deadlines,
                                    Prepares and executes unit / component tests and carries out unit tests,
                                    Investigation and resolution(where necessary) of customer issues,
                                    Participates in code reviews, identifying risks, and escalating issues when necessary,
                                    Collaborate with other Developers, Analysts, QA to ensure high - quality software,
                                    Identifies best practice improvements and makes changes as agreed with Team Leader,
                                    Provides information to the project leader to assist with project planning,
                                    Provides input on technical direction.
                                    We Require
                                    At least 2 years of experience with.NET,
                                    Good knowledge Microsoft.NET Framework 4.0 / 4.5,
                                    Experience in ADO.NET, MVC 3 / 4, API rest, Json,
                                    Base knowledge of Microsoft Visual Studio, TFS and GIT code repository,
                                    TSQL(SQL Server 2012 and above),
                                    Good communication skills in English language(reading, writing, speaking),
                                    BSc / MSc degree in Computer Science, Information Systems or equivalent.",
                           Date = DateTime.Now,
                           Company = "React",
                           Function = "Frontend",
                           Industry = "IT",
                           Location = "London",
                           EmployeeCount = "520",
                           Candidates = new List<JobCandidate>{
                                new JobCandidate
                                {
                                    AppUser = users[1],
                                    IsEmployer = true
                                },
                                new JobCandidate
                                {
                                    AppUser = users[2],
                                    IsEmployer = false
                                },
                           }
                    }

                };

                await context.Jobs.AddRangeAsync(jobs);
                await context.SaveChangesAsync();
            }
        }
    }
}
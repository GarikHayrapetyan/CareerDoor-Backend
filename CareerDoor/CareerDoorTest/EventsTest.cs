using API.Controllers;
using Application.Core;
using Application.GetTogethers;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.GetTogethers.List;

namespace CareerDoorTest
{
    public class EventsTest
    {
        [Fact]
        public void GetEventsTest()
        {
            // Arrange
            GetTogetherController controller = new GetTogetherController();
            GetTogetherParams eventParams = new GetTogetherParams { IsGoing = true };
            var token = new CancellationToken();

            // Act
            var events = controller.List(eventParams,token);
            // Assert
            Assert.NotNull(events);
        }

        [Fact]
        public void GetCurrentUserEventsTest()
        {
            // Arrange
            GetTogetherParams eventParams = new GetTogetherParams { IsGoing = true };
            Query listQuery = new Query { Params = eventParams };
            var token = new CancellationToken();

            var dbcontext = (Persistence.DataContext) A.Fake<IdentityDbContext<AppUser>>();
            var user = A.Fake<IUserAccessor>();
            var mapper = A.Fake<IMapper>();
            var handler = new Handler(dbcontext,mapper,user);
            Console.WriteLine(dbcontext.Jobs.Count());
            // Act
            var events = handler.Handle(listQuery, token).Result.Value;
            // Assert
           // Assert.Throws();
        }
    }
} 

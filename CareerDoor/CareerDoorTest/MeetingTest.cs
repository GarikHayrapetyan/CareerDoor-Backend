using Application.Core;
using Application.GetTogethers;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CareerDoorTest
{
    public class MeetingTest: IntegrationTest
    {
        [Fact]
        public async Task List_NotPassedMeetings_ReturnsOkResponse() {
            // Arrange
            await AuthenticateAsync();
            // Act
            var response = await TestClient.GetAsync(new Uri("http://localhost:23050/api/gettogether"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<GetTogetherDTO>>()).Should().NotBeEmpty();
            
        }
    }
}

using API.DTOs;
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
    public class AccountControllerTest:IntegrationTest
    {
        private List<RegisterDto> users;
        [Fact]
        public async Task Login_ReturnsOkResponse() {

            // Arrange
            var loginDto = new LoginDto { Email = "bob@test.com", Password = "Pa$$w0rd" };

            var json = JsonConvert.SerializeObject(loginDto);

            HttpContent payload = new StringContent(json, Encoding.UTF8, "application/json");
            // Act
            var response = await TestClient.PostAsync(new Uri("http://localhost:23050/api/account/login"),payload);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}

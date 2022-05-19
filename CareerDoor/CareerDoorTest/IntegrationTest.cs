using API;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CareerDoorTest
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();

        }

        protected async Task AuthenticateAsync()
        {
            var user= await GetUserAsync();
            var json = JObject.Parse(user);
            var token = json["token"].ToString();
            
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        private async Task<string> GetUserAsync()
        {
            var response = await TestClient.PostAsJsonAsync(new Uri("http://localhost:23050/api/account/login"), new LoginDto
            {
                Email = "bob@test.com",
                Password = "Pa$$w0rd"

            });
            var registrationResponse = response.Content.ReadAsStringAsync().Result;
            return registrationResponse;
        }
    }
} 

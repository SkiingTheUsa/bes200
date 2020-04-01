using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LibraryApi;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class OnCallDeveloperTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public OnCallDeveloperTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetOnCallEmployee()
        {
            var response = await _client.GetAsync("/oncalldeveloper");
            Assert.True(response.IsSuccessStatusCode);
            var data = await response.Content.ReadAsAsync<DeveloperResponse>();
            Assert.Equal("testing@test.com", data.Email);
        }
    }
    public class DeveloperResponse
    {
        public string Email { get; set; }
    }
}

using LibraryApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class HiringEmployees : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HiringEmployees(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanHireJoe()
        {
            var request = new { name = "Joe", startingSalary = 33000 };
            var response = await _client.PostAsJsonAsync("/employees", request);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hiring Joe starting at $33,000.00 with id of 00000000-0000-0000-0000-000000000000", content);
        }
    }

}

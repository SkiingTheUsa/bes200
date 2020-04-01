using LibraryApi.Controllers;
using LibraryApi.Models;
using System.Threading.Tasks;

namespace LibraryApiIntegrationTests
{
    public class FakeTeamsDeveloperLookup : ILookupOnCallDevelopers
    {
        public Task<OnCallDeveloperResponse> GetOnCallDeveloper()
        {
            return Task.FromResult(new OnCallDeveloperResponse { Email = "testing@test.com" });
        }
    }
}

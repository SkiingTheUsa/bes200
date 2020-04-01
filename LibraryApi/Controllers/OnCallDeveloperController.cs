using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class OnCallDeveloperController : Controller
    {
        private readonly ILookupOnCallDevelopers _onCallLookup;

        public OnCallDeveloperController(ILookupOnCallDevelopers onCallLookup)
        {
            _onCallLookup = onCallLookup; // made a change
        }

        [HttpGet("oncalldeveloper")]
        public async Task<ActionResult<OnCallDeveloperResponse>> GetOnCallDeveloper()
        {
            // WTCYWYH (Write the code you wish you had)
            var response = await _onCallLookup.GetOnCallDeveloper();
            return Ok(response);
        }
    }
}

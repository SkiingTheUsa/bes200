using System;
using System.Text;
using LibraryApi.Controllers;
using LibraryApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace LibraryApi.Services
{
    public class MicrosoftTeamsOnCallDeveloperLookup : ILookupOnCallDevelopers
    {
        private readonly IDistributedCache _Cache;

        public MicrosoftTeamsOnCallDeveloperLookup(IDistributedCache cache)
        {
            _Cache = cache;
        }

        [HttpGet("/oncalldeveloper")]
        public async Task<OnCallDeveloperResponse> GetOnCallDeveloper()
        {
            var email = await _Cache.GetAsync("email");
            string emailAddress = null;
            if (email == null)
            {
                // call the Microsoft Teams API, get the email address.
                var emailToSave = $"bob-{DateTime.Now.ToLongTimeString()}@aol.com";
                var encodedEmail = Encoding.UTF8.GetBytes(emailToSave);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(15));
                await _Cache.SetAsync("email", encodedEmail, options);
                // Add it the cache with an expiration
                // return that email addres.
                emailAddress = emailToSave;
            }
            else
            {
                emailAddress = Encoding.UTF8.GetString(email);
            }
            return new OnCallDeveloperResponse { Email = emailAddress };
        }
    }
}

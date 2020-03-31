using System.Net;
using LibraryApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class ResourceSmokeTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ResourceSmokeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/books")]
        [InlineData("/books/1")]
        public async Task CheckIfResourceIsAlive(string resource)
        {
            var response = await _client.GetAsync(resource);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CanGetABook()
        {
            var response = await _client.GetAsync(@"/books/1");

            var book = await response.Content.ReadAsAsync<BookDetailsResponse>();

            Assert.Equal(1, book.id);
            Assert.Equal("Walden", book.title);
            Assert.Equal("Philosophy", book.genre);
        }

        [Fact]
        public async Task CanAddABook()
        {
            var bookToAdd = new PostBookRequest
            {
                author = "James Patterson",
                genre = "Suspense",
                numberOfPages = 450,
                title = "Criss Cross"
            };

            var response = await _client.PostAsJsonAsync(@"/books", bookToAdd);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var location = response.Headers.Location.LocalPath;

            var getItResponse = await _client.GetAsync(location);
            var responseData = await getItResponse.Content.ReadAsAsync<BookDetailsResponse>();
            Assert.Equal(bookToAdd.title, responseData.title);
            Assert.Equal(bookToAdd.author, responseData.author);
        }
    }


    public class BookDetailsResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public int numberOfPages { get; set; }
    }

    public class PostBookRequest
    {
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public int numberOfPages { get; set; }
    }
}

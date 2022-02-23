using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ReviewYourMovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscoverMoviesController : Controller
    {
        private readonly string _requestUri = "https://api.themoviedb.org/3/";
        private readonly string _apiKey = "078e0acb19610d04a72b899c487240d1";

        // GET: DiscoverMoviesController
        [HttpGet]
        public async Task <ActionResult<DiscoverMovie>> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_requestUri}discover/movie?api_key={_apiKey}");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.SerializeObject(responseString);

            //var result = response.Content.ReadFromJsonAsync<DiscoverMovie>();

            return Ok(json);
        }
    }
}

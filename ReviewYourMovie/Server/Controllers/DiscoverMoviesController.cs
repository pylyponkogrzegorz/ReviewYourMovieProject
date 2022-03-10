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

        // POST: api/DiscoverMoviesController/page
        [HttpPost("page")]
        public async Task<ActionResult> PostPage([FromBody] int pageRequest)
        {
            //var page = HttpContext.Request.Form["Page"];
            var pageReq = pageRequest.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{_requestUri}discover/movie?api_key={ApiHelper.apikey}&page={pageReq}");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return Ok(json);
        }

        // POST: api/DiscoverMoviesController/queryandpage
        [HttpPost("queryandpage")]
        public async Task<ActionResult> SerachMoviesWithPages([FromBody] string json)
        {
            var requestJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            var query = requestJson["query"];
            var page = requestJson["page"];

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{_requestUri}search/movie?api_key={ApiHelper.apikey}&query={query}&page={page}");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            var jsonResp = await response.Content.ReadAsStringAsync();

            return Ok(jsonResp);
        }
    }
}

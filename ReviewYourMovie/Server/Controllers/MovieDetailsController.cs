using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewYourMovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieDetailsController : ControllerBase
    {
        private readonly string _requestUri = "https://api.themoviedb.org/3/";


        // POST: api/DiscoverMoviesController/id
        [HttpPost("id")]
        public async Task<ActionResult> GetMovieDetails( [FromBody] int movieId)
        {
            var id = movieId.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{_requestUri}movie/{id}?api_key={ApiHelper.apikey}");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return Ok(json);
        }
    }
}

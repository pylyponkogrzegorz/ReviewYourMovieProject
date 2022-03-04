using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewYourMovie.Server.Context;

namespace ReviewYourMovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieReviewController : ControllerBase
    {
        private readonly UserContext _context;

        public MovieReviewController(UserContext context)
        {
            _context = context;
        }

        private MovieReview movieReview;

        // POST: api/MovieReviewController/add
        [HttpPost("add")]
        public async Task<ActionResult<List<MovieReview>>> AddReview([FromBody] Review review)
        {
            var usernameClaims = User.Claims.FirstOrDefault(x => x.Type == "username");
            var user = await _context.Users.FirstOrDefaultAsync<User>(x => x.Username == usernameClaims.Value);

            movieReview = new()
            {
                MovieId = review.MovieId,
                UserId = user.UserId,
                ReviewDescription = review.ReviewDescription,
                ReviewDatetime = DateTime.Now,
                User = user
            };
            _context.MovieReviews.Add(movieReview);
            await _context.SaveChangesAsync();

            return await _context.MovieReviews.ToListAsync();
        }

    }
}

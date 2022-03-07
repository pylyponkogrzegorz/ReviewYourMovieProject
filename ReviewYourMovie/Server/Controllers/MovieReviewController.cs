using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewYourMovie.Server.Context;
using System.Security.Claims;
using static Microsoft.AspNetCore.Http.HttpContext;

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

        // POST: api/MovieReviewController/average
        [HttpPost("average")]
        public async Task<ActionResult<double>> GetAverage([FromBody] int id)
        {
            var sum = await _context.MovieReviews.Where(movie => movie.MovieId == id).ToListAsync();
            if (sum.Count < 1) return NotFound();

            double summary = 0;

            foreach (var item in sum)
            {
                summary += Convert.ToDouble(item.ReviewRating);
            }

            var average = summary / sum.Count;
            average = Math.Round(average, 2);

            return average;
        }


        [Authorize]
        // POST: api/MovieReviewController/add
        [HttpPost("add")]
        public async Task<ActionResult<Review>> AddReview([FromBody] Review review)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Username == HttpContext.User.Identity.Name);

            if (userFromDb == null) return StatusCode(401);

            movieReview = new()
            {
                MovieId = review.MovieId,
                UserId = userFromDb.UserId,
                ReviewDescription = review.ReviewDescription,
                ReviewDatetime = DateTime.UtcNow,
                ReviewRating = review.ReviewRating,
                User = userFromDb,
                UserScore = review.UserScore,
            };
            _context.MovieReviews.Add(movieReview);
            await _context.SaveChangesAsync();

            review = ReviewFactory.translateReview(movieReview);

            User user = await _context.Users.FirstOrDefaultAsync<User>(user => user.UserId == review.UserId);
            review.Username = user.Username;

            return review;
        }

        // POST: api/MovieReviewController/getMore
        [HttpPost("getMore")]
        public async Task<List<Review>?> GetMoreReviews([FromBody] Review review)
        {
            var movieReviews = await _context.MovieReviews.Where(x => x.ReviewDatetime < review.ReviewDatetime).Where(x => x.MovieId == review.MovieId).OrderByDescending(x => x.ReviewDatetime).Take(10).ToListAsync();
            var reviews = new List<Review>();
            movieReviews.ForEach(x => reviews.Add(ReviewFactory.translateReview(x)));

            User? user;

            foreach (var rev in reviews)
            {
                user = await _context.Users.FirstOrDefaultAsync<User>(user => user.UserId == rev.UserId);
                rev.Username= user.Username;
            }

            try
            {
                return reviews;

            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}

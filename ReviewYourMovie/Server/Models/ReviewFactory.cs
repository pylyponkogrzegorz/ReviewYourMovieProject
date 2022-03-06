using ReviewYourMovie.Server.Context;
using ReviewYourMovie.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewYourMovie.Server.Models;
using ReviewYourMovie.Server.Managers;
using System.Security.Claims;

namespace ReviewYourMovie.Server.Models
{
    public class ReviewFactory
    {
        public static Review translateReview(MovieReview toTranslate)
        {
            return new Review()
            {
                ReviewDescription = toTranslate.ReviewDescription,
                ReviewDatetime = toTranslate.ReviewDatetime,
                ReviewRating = toTranslate.ReviewRating,
                UserId = toTranslate.UserId,
                MovieId = toTranslate.MovieId,
                ReviewId = toTranslate.ReviewId,
                UserScore = toTranslate.UserScore,
            };
        }
    }
}

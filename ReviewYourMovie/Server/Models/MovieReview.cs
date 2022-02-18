using System;
using System.Collections.Generic;

namespace ReviewYourMovie.Server.Models
{
    public partial class MovieReview
    {
        public MovieReview()
        {
            VoteReviews = new HashSet<VoteReview>();
        }

        public int ReviewId { get; set; }
        public decimal ReviewRating { get; set; }
        public string? ReviewDescription { get; set; }
        public int UserId { get; set; }
        public long MovieId { get; set; }
        public DateTime ReviewDatetime { get; set; }
        public decimal UserScore { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<VoteReview> VoteReviews { get; set; }
    }
}

namespace ReviewYourMovie.Shared.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        private decimal reviewRating;
        public decimal ReviewRating 
        { 
            get
            {
                return Math.Round(reviewRating, 1);
            }
            set { reviewRating = value; }
        }
        public string? ReviewDescription { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public long MovieId { get; set; }
        public DateTime ReviewDatetime { get; set; }
        public decimal UserScore { get; set; }
    }
}

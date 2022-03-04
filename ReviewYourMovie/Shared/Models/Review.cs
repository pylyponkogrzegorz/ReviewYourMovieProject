using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewYourMovie.Shared.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public decimal ReviewRating { get; set; }
        public string? ReviewDescription { get; set; }
        public int UserId { get; set; }
        public long MovieId { get; set; }
        public DateTime ReviewDatetime { get; set; }
        public decimal UserScore { get; set; }
    }
}

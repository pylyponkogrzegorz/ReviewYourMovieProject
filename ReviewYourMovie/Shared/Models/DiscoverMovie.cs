using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewYourMovie.Shared
{
    public class DiscoverMovie
    {
        public int Page { get; set; }
        public Result[] Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }

}

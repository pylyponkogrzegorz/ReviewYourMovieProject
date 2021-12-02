namespace ReviewYourMovie.Shared
{
    public class MovieDetails
    {
        public bool Adult { get; set; }
        public string Backdrop_path { get; set; }
        public Belongs_To_Collection Belongs_to_collection { get; set; }
        public int Budget { get; set; }
        public Genre[] Genres { get; set; }
        public string Homepage { get; set; }
        public int Id { get; set; }
        public string Imdb_id { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Overview { get; set; }
        public float Popularity { get; set; }
        public string Poster_path { get; set; }
        public Production_Companies[] Production_companies { get; set; }
        public Production_Countries[] Production_countries { get; set; }
        public string Release_date { get; set; }
        public int Revenue { get; set; }
        public int Runtime { get; set; }
        public Spoken_Languages[] Spoken_languages { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public float Vote_average { get; set; }
        public int Vote_count { get; set; }
    }


}

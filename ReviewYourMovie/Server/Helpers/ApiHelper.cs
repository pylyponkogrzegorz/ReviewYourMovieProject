using System.Net.Http.Headers;

namespace ReviewYourMovie.Server.Helpers
{
    public class ApiHelper
    {
        public static HttpClient HttpClient { get; set; }

        public static string apikey = "078e0acb19610d04a72b899c487240d1";

        public static void InitializeClient()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.themoviedb.org/3/")
            };
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

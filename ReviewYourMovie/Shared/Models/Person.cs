namespace ReviewYourMovie.Shared
{
    public class Person
    {
        public int Id { get; set; }
        public Cast[] Cast { get; set; }
        public Crew[] Crew { get; set; }
    }

}

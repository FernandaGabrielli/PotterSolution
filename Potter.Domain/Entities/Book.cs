namespace Potter.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string ReleaseDate { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public string Cover { get; set; }
    }
}

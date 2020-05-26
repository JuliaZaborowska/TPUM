namespace PresentationLayer.Model
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public string Publisher { get; set; }

        public int ReleaseYear { get; set; }

    }
}

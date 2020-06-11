using System;
using System.Collections.Generic;
using System.Text;

namespace PresentationLayer.Model
{
    public class BookModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public string Publisher { get; set; }

        public int ReleaseYear { get; set; }
    }
}

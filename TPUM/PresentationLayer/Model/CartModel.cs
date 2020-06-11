using System;
using System.Collections.Generic;
using System.Text;

namespace PresentationLayer.Model
{
    public class CartModel
    {
        public UserModel User { get; set; }

        public IList<BookModel> Books { get; set; }
    }
}

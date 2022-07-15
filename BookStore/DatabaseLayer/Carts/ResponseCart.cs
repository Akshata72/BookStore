using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Carts
{
    public class ResponseCart
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string BookImage { get; set; }
        public int ActualPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int BookQuntity { get; set; }
    }
}

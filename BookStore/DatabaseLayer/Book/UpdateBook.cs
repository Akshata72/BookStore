using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Book
{
    public class UpdateBook
    {
        public string BookImage { get; set; }
        public string BookDetails { get; set; }
        public int ActualPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int BookQuntity { get; set; }
        public float BookRating { get; set; }
        public float RatingCount { get; set; }
    }
}

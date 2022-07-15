using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Book
{
    public class BookPostModel
    {
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public string BookImage { get; set; }
        [Required]
        public string BookDetails { get; set; }
        public int ActualPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int BookQuntity { get; set; }
        public float BookRating { get; set; }
        public float RatingCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.WishList
{
    public class WishListResponse
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int WishListId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string BookImage { get; set; }
        public int ActualPrice { get; set; }
        public int DiscountPrice { get; set; }
    }
}

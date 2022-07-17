﻿using DatabaseLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        public string AddToWishList(int UserId, int BookId);
        public string RemoveFromWishList(int UserId, int WishListId);
        public List<WishListResponse> GetAllFromWishList(int UserId);
    }
}

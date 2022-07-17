using BusinessLayer.Interface;
using DatabaseLayer.WishList;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public string AddToWishList(int UserId, int BookId)
        {
            try
            {
               return this.wishListRL.AddToWishList(UserId, BookId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<WishListResponse> GetAllFromWishList(int UserId)
        {
            try
            {
                return this.wishListRL.GetAllFromWishList(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RemoveFromWishList(int UserId, int WishListId)
        {
            try
            {
                return this.wishListRL.RemoveFromWishList(UserId,WishListId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

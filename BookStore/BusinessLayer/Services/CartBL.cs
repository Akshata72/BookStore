using BusinessLayer.Interface;
using DatabaseLayer.Carts;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL :ICartBL
    {
        ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public Addcart AddToCart(Addcart cart, int UserId)
        {
            try
            {
                return this.cartRL.AddToCart(cart, UserId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<ResponseCart> GetAllFromCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllFromCart(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RemoveFromCart(int UserId, int CartId)
        {
            try
            {
                return this.cartRL.RemoveFromCart(CartId, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateCart(int BookQuntity, int CartId, int UserId)
        {
            try
            {
                return this.cartRL.UpdateCart(BookQuntity,CartId, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

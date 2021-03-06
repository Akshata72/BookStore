using DatabaseLayer.Carts;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public Addcart AddToCart(Addcart cart, int UserId);
        public string RemoveFromCart(int UserId, int CartId);
        public string UpdateCart(int BookQuntity, int CartId, int UserId);
        public List<ResponseCart> GetAllFromCart(int UserId);
    }
}

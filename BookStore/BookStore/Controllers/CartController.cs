using BusinessLayer.Interface;
using DatabaseLayer.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [Authorize]
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(Addcart addcart)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.cartBL.AddToCart(addcart,userId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Added to cart Successfully",data = result });
            }
            return this.BadRequest(new { success = false, message = "Failed to Add to Cart." });
        }
        [Authorize]
        [HttpPost("RemoveFromCart/{CartId}")]
        public IActionResult DeleteBook(int CartId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.cartBL.RemoveFromCart(userId, CartId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Deleted from cart succsefully." });
            }
            return this.BadRequest(new { success = false, message = "Failed to Remove From Cart" });
        }
        [Authorize]
        [HttpPost("UpdateCart/{CartId}/{BookQuntity}")]
        public IActionResult UpdateCart(int CartId,int BookQuntity)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.cartBL.UpdateCart(BookQuntity,CartId,userId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "BookQuntity Updated succsefully." });
            }
            return this.BadRequest(new { success = false, message = "CartId does Not match." });
        }
        [Authorize]
        [HttpGet("GetAllFromCart")]
        public IActionResult GetAllFromCart()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.cartBL.GetAllFromCart(userId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Get All From Cart successfully", data = result });
            }
            return this.BadRequest(new { success = false, message = "Not Find data.." });
        }
    }
}

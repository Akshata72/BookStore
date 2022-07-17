using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }
        [Authorize]
        [HttpPost("AddToWishList/{BookId}")]
        public IActionResult AddToWishList(int BookId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = this.wishListBL.AddToWishList(userId, BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Added to WishList Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Added WishList." });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("Remove/{WishListId}")]
        public IActionResult RemoveFromWishList(int WishListId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = wishListBL.RemoveFromWishList(userId,WishListId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = "Removed from WishList sucessfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Remove from WishList" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("GetAllFromWishList")]
        public IActionResult GetAllWishList()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = wishListBL.GetAllFromWishList(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "GetALl WishList sucessfull", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to GetAll WishList" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}

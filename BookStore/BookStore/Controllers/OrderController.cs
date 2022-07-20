using BusinessLayer.Interface;
using DatabaseLayer.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL ordersBL;
        public OrderController(IOrderBL ordersBL)
        {
            this.ordersBL = ordersBL;
        }
        [Authorize]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(AddOrder addOrder)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = ordersBL.AddOrder(addOrder, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Ordered sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Order" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = ordersBL.GetAllOrders(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Orders Retrieved sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Retrieve Orders" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}

using BusinessLayer.Interface;
using DatabaseLayer.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }
        [Authorize]
        [HttpPost("AddAddress")]
        public ActionResult AddAddress(AddAddress addAddress)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = addressBL.AddAddress(userId,addAddress);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Address Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Address" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel updateAddress)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = addressBL.UpdateAddress(updateAddress, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Address Updated sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Update Address" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("Delete/{AddressId}")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = addressBL.DeleteAddress(addressId, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Address Deleted sucessfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Delete Address" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("GetAllAddresses")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = addressBL.GetAllAddresses(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Get All Address sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to Get All Addresses" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}

using BusinessLayer.Interface;
using DatabaseLayer.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookBL bookbl;
        public BookController(IBookBL bookBL)
        {
            this.bookbl = bookBL;
        }
        [Authorize]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookPostModel bookPost)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.bookbl.AddBook(userId,bookPost);
            if(result != null)
            {
                return this.Ok(new {success = true,message = "Book Added succsefully."});
            }
            return this.BadRequest(new { success = false, message = "Book Not added." });
        }
        [Authorize]
        [HttpPost("UpdateBook/{BookId}")]
        public IActionResult UpdateBook(int BookId,UpdateBook updateBook)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.bookbl.UpdateBook(userId,BookId,updateBook);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Book Updated succsefully." });
            }
            return this.BadRequest(new { success = false, message = "BookId does Not match." });
        }
        [Authorize]
        [HttpPost("DeleteBook/{BookId}")]
        public IActionResult DeleteBook(int BookId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.bookbl.DeleteBook(userId, BookId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Book Deleted succsefully." });
            }
            return this.BadRequest(new { success = false, message = "Failed to delte the book." });
        }
        [Authorize]
        [HttpGet("GetBookById/{BookId}")]
        public IActionResult GetBookById(int BookId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.bookbl.GetBookById(userId,BookId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Book got successfully", data =result });
            }
            return this.BadRequest(new { success = false, message = "Book got successfully.." });
        }
        [Authorize]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var result = this.bookbl.GetAllBooks(userId);
            if (result != null)
            {
                return this.Ok(new { success = true, message ="Get AllBook successfully", data = result });
            }
            return this.BadRequest(new { success = false, message = "Not Find data.." });
        }
    }
}

using BusinessLayer.Interface;
using DatabaseLayer.Book;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL BookRL;
        public BookBL(IBookRL bookRL)
        {
            this.BookRL = bookRL;
        }
     
        public BookPostModel AddBook(int UserId,BookPostModel bookPost)
        {
            try
            {
               return this.BookRL.AddBook(UserId,bookPost);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string DeleteBook(int UserId, int Bookid)
        {
            try
            {
                return this.BookRL.DeleteBook(UserId, Bookid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookPostModel> GetAllBooks(int UserId)
        {
            try
            {
                return this.BookRL.GetAllBooks(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookPostModel GetBookById(int UserId,int BookId)
        {
            try
            {
                return this.BookRL.GetBookById(UserId,BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateBook UpdateBook(int UserId, int BookId, UpdateBook updateBook)
        {
            try
            {
                return this.BookRL.UpdateBook(UserId, BookId,updateBook);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

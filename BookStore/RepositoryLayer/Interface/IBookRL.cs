using DatabaseLayer.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookPostModel AddBook(int UserId,BookPostModel bookPost);
        public UpdateBook UpdateBook(int UserId,int BookId,UpdateBook updateBook);
        public string DeleteBook(int UserId, int Bookid);
        public BookPostModel GetBookById(int UserId,int BookId);
        public List<BookPostModel> GetAllBooks(int UserId);
    }
}

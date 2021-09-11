using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Repositories.Book
{
    public interface IBookRepository
    {
        public List<Models.Book> getBooks();
        Models.Book getBookById(int id);
        void InsertBook(Models.Book Book);
        void UpdateBook(int id, Models.Book Book);
        void DeleteBook(int id);
    }
}

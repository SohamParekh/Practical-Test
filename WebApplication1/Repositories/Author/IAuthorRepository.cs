using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Repositories.Author
{
    public interface IAuthorRepository
    {
        public List<Models.Author> getAuthor();
        Models.Author getAuthorById(int id);
        void InsertAuthor(Models.Author author);
        void UpdateAuthor(int id, Models.Author author);
        void DeleteAuthor(int id);
        Models.AuthorAC GetAuthorDetailWithBooks(int id);
        void AddBookFromAuthorDetailPage(Models.Book book);
        int UpdateBookReturnAuthorId(int id, Models.Book book);
        int DeleteBook(int id);
    }
}

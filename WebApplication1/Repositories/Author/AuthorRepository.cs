using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Author
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Author.Find(id);
            _context.Author.Remove(author);
            _context.SaveChanges();
        }

        public List<Models.Author> getAuthor()
        {
            var author = _context.Author;
            return author.ToList();
        }

        public Models.Author getAuthorById(int id)
        {
            var authorDetail = _context.Author.FirstOrDefault(e => e.Id == id);
            var booksDetail = _context.Book.Where(x => x.AuthorId == id).ToList();
            Models.Author authorAC = new Models.Author
            {
                Id = authorDetail.Id,
                Address = authorDetail.Address,
                Name = authorDetail.Name,
                PhoneNumber = authorDetail.PhoneNumber
            };
            return authorAC;
        }

        public void InsertAuthor(Models.Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(int id, Models.Author author)
        {
            _context.Update(author);
            _context.SaveChanges();
        }

        public Models.AuthorAC GetAuthorDetailWithBooks(int id)
        {
            var authorDetail = _context.Author.FirstOrDefault(e => e.Id == id);
            var booksDetail = _context.Book.Where(x => x.AuthorId == id).ToList();
            Models.AuthorAC authorAC = new Models.AuthorAC
            {
                Id = authorDetail.Id,
                Address = authorDetail.Address,
                Name = authorDetail.Name,
                PhoneNumber = authorDetail.PhoneNumber,
                Book = booksDetail
            };
            return authorAC;
        }

        public void AddBookFromAuthorDetailPage(Models.Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();
        }

        public int UpdateBookReturnAuthorId(int id, Models.Book book)
        {
            var getBook = _context.Book.Find(id);
            getBook.Name = book.Name;
            getBook.Price = book.Price;
            getBook.PublishedDate = book.PublishedDate;
            getBook.Quantity = book.Quantity;
            _context.Book.Update(getBook);
            _context.SaveChanges();
            return (int)getBook.AuthorId;
        }

        public int DeleteBook(int id)
        {
            var book = _context.Book.Find(id);
            var returnId = book.AuthorId;
            _context.Book.Remove(book);
            _context.SaveChanges();
            return (int)returnId;
        }
    }
}

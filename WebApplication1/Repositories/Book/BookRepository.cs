using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Book
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public void DeleteBook(int id)
        {
            var book = _context.Book.Find(id);
            _context.Book.Remove(book);
            _context.SaveChanges();
        }

        public Models.Book getBookById(int id)
        {
            return _context.Book.FirstOrDefault(e => e.Id == id);
        }

        public List<Models.Book> getBooks()
        {
            var book = _context.Book.Include(e => e.Author);
            return book.ToList();
        }

        public void InsertBook(Models.Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(int id, Models.Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories.Author;
using WebApplication1.Repositories.Book;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public BooksController(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            return View(_bookRepository.getBooks());
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            ViewBag.authorName = _authorRepository.getAuthor();
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.InsertBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                Book author = _bookRepository.getBookById(id);
                return View(author);
            }
            return View();
        }

        // GET: Author/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.authorName = _authorRepository.getAuthor();
            Book book = _bookRepository.getBookById(id);
            return View(book);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            try
            {
                _bookRepository.UpdateBook(id, book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public IActionResult Delete(int id)
        {
            Book book = _bookRepository.getBookById(id);
            return View(book);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}

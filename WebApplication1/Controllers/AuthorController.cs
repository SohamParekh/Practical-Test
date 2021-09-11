using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repositories.Author;
using WebApplication1.Repositories.Book;

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        public AuthorController(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            return View(_authorRepository.getAuthor());
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _authorRepository.InsertAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                AuthorAC author = _authorRepository.GetAuthorDetailWithBooks(id);
                ViewBag.id = author.Id;
                return View(author);
            }
            return View();
        }

        public IActionResult AddBookWithAuthorId(int id)
        {
            Book book = new Book();
            var modalState = ModelState.Values;
            book.AuthorId = id;
            ViewBag.authorId = id;
            return PartialView("_bookModalPartial", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBookWithAuthorId(Book book)
        {
            if (ModelState.IsValid)
            {
                int authorId = int.Parse(Request.Form["AuthorId"]);
                book.Id = 0;
                book.AuthorId = authorId;
                _authorRepository.AddBookFromAuthorDetailPage(book);
                return Redirect("/Author/Details/"+authorId);
            }
            return View();
        }

        // GET: Author/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.authorName = _authorRepository.getAuthor();
            Author author = _authorRepository.getAuthorById(id);
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            try
            {
                _authorRepository.UpdateAuthor(id, author);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult EditBook(int id)
        {
            Book book = _bookRepository.getBookById(id);
            return PartialView("_bookEditModalPartial", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBook(int id, Book book)
        {
            try
            {
                var authorId =_authorRepository.UpdateBookReturnAuthorId(id, book);
                return Redirect("/Author/Details/" + authorId);
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public IActionResult Delete(int id)
        {
            Author employee = _authorRepository.getAuthorById(id);
            return View(employee);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _authorRepository.DeleteAuthor(id);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteBook(int id)
        {
            Book book = _bookRepository.getBookById(id);
            return PartialView("_bookDeleteModalPartial", book);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBookConfirmed(Book book)
        {
            var authorId = _authorRepository.DeleteBook(book.Id);
            return Redirect("/Author/Details/" + authorId);
        }
    }
}

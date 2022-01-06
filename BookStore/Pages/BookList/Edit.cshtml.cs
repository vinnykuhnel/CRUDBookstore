using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore.Data;

namespace BookStore.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book book { get; set; }
        public async Task OnGet(int id)
        {
            book = await _db.Book.FindAsync(id);
        }

        //Page will be rerouted so use Task of IActionResult
        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _db.Book.FindAsync(book.ID);
                BookFromDb.Name = book.Name;
                BookFromDb.ISBN = book.ISBN;
                BookFromDb.Author = book.Author;
                Console.WriteLine(BookFromDb.GetType());
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            return RedirectToPage();
        }
    }
}

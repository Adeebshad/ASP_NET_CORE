using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookList.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Customer> Customers{ get; set; }

        public async Task OnGet()
        {
            Customers = await _db.Customer.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var customer = await _db.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _db.Customer.Remove(customer);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

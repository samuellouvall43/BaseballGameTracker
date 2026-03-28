using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseballGameTracker.Data;

namespace BaseballGameTracker.Controllers
{
    public class EmailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Email.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email
                .FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }
        //View 
        public IActionResult DeleteEntry()
        {
            return View(); 
        }

        //Delete view based on email 
        [HttpPost]
    
        public async Task<IActionResult> DeleteBasedOnEmailAsync(Email email)
        {

          
            int emailId = GetEmailId(email.EmailAddress);

            Console.WriteLine("EMAIL ID TO BE DELETED: " + emailId);

            await DeleteConfirmed(emailId);

            return RedirectToAction("Index", "Home");
        }


        public int GetEmailId(string email)
        {

            var emails = _context.Email.ToList();
           

           foreach(var e in emails)
            {
           
                if(String.Equals(e.EmailAddress, email))
                {
                    return e.Id;
                }

            }
            return 0; 
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmailAddress")] Email email)
        {
            int emailId = GetEmailId(email.EmailAddress);
            Console.WriteLine("EMAIL ID " + emailId); 

            if(emailId != 0)
            {
                ModelState.AddModelError("EmailAddress", "Email is already entered");
            
            }

            if (ModelState.IsValid)
            {
                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmailAddress")] Email email)
        {
            if (id != email.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            var email = await _context.Email
                .FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            Console.WriteLine("DELETING THIS EMAIL" + id);

            var email = await _context.Email.FindAsync(id);
            if (email != null)
            {
                _context.Email.Remove(email);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(int id)
        {
            return _context.Email.Any(e => e.Id == id);
        }
    }
}

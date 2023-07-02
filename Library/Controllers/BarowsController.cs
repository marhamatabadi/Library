using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;

namespace Library.Controllers
{
    public class BarowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Barows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Barows.Include(b => b.Book).Include(b => b.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Barows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Barows == null)
            {
                return NotFound();
            }

            var barow = await _context.Barows
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barow == null)
            {
                return NotFound();
            }

            return View(barow);
        }

        // GET: Barows/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name");
            return View();
        }

        // POST: Barows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,BookId,StartDate,EndDate")] Barow barow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", barow.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", barow.MemberId);
            return View(barow);
        }

        // GET: Barows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Barows == null)
            {
                return NotFound();
            }

            var barow = await _context.Barows.FindAsync(id);
            if (barow == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", barow.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", barow.MemberId);
            return View(barow);
        }

        // POST: Barows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,BookId,StartDate,EndDate")] Barow barow)
        {
            if (id != barow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarowExists(barow.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", barow.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", barow.MemberId);
            return View(barow);
        }

        // GET: Barows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Barows == null)
            {
                return NotFound();
            }

            var barow = await _context.Barows
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barow == null)
            {
                return NotFound();
            }

            return View(barow);
        }

        // POST: Barows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Barows == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Barows'  is null.");
            }
            var barow = await _context.Barows.FindAsync(id);
            if (barow != null)
            {
                _context.Barows.Remove(barow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarowExists(int id)
        {
          return (_context.Barows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

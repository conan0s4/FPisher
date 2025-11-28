using FPisher.Data;
using FPisher.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPisher.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class PageVisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PageVisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PageVisits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Page_Visits.ToListAsync());
        }

        // GET: PageVisits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageVisits = await _context.Page_Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageVisits == null)
            {
                return NotFound();
            }

            return View(pageVisits);
        }

        // GET: PageVisits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageVisits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Page_emails")] PageVisits pageVisits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageVisits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageVisits);
        }

        // GET: PageVisits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageVisits = await _context.Page_Visits.FindAsync(id);
            if (pageVisits == null)
            {
                return NotFound();
            }
            return View(pageVisits);
        }

        // POST: PageVisits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Page_emails")] PageVisits pageVisits)
        {
            if (id != pageVisits.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageVisits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageVisitsExists(pageVisits.Id))
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
            return View(pageVisits);
        }

        // GET: PageVisits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageVisits = await _context.Page_Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageVisits == null)
            {
                return NotFound();
            }

            return View(pageVisits);
        }

        // POST: PageVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageVisits = await _context.Page_Visits.FindAsync(id);
            if (pageVisits != null)
            {
                _context.Page_Visits.Remove(pageVisits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageVisitsExists(int id)
        {
            return _context.Page_Visits.Any(e => e.Id == id);
        }
    }
}

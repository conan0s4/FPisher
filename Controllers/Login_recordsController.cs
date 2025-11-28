using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPisher.Data;
using FPisher.Models;
using Microsoft.AspNetCore.Authorization;

namespace FPisher.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class Login_recordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Login_recordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login_records
        public async Task<IActionResult> Index()
        {
            return View(await _context.Login_Records.ToListAsync());
        }

        // GET: Login_records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_records = await _context.Login_Records
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_records == null)
            {
                return NotFound();
            }

            return View(login_records);
        }

        // GET: Login_records/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login_records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,uname,pass")] Login_records login_records)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login_records);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login_records);
        }

        // GET: Login_records/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_records = await _context.Login_Records.FindAsync(id);
            if (login_records == null)
            {
                return NotFound();
            }
            return View(login_records);
        }

        // POST: Login_records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,uname,pass")] Login_records login_records)
        {
            if (id != login_records.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login_records);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Login_recordsExists(login_records.Id))
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
            return View(login_records);
        }

        // GET: Login_records/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_records = await _context.Login_Records
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_records == null)
            {
                return NotFound();
            }

            return View(login_records);
        }

        // POST: Login_records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login_records = await _context.Login_Records.FindAsync(id);
            if (login_records != null)
            {
                _context.Login_Records.Remove(login_records);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Login_recordsExists(int id)
        {
            return _context.Login_Records.Any(e => e.Id == id);
        }
    }
}

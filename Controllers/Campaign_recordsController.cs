using FPisher.Data;
using FPisher.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FPisher.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Campaign_recordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Campaign_recordsController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpPost]
        public async Task<IActionResult> SendEmails()
        {
            var campaigns = await _context.Campaign_Records.ToListAsync();

            if (campaigns == null || campaigns.Count == 0)
            {
                TempData["Message"] = "No emails found to send.";
                return RedirectToAction(nameof(Index));
            }

            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")
            {
                Port = 587,
                Credentials = new NetworkCredential("37d0077f609044", "11d2b5e8e79468"),
                EnableSsl = true,
            };

            foreach (var campaign in campaigns)
            {
                if (string.IsNullOrWhiteSpace(campaign.custom_link))
                    continue; // skip if no link generated

                string subject = "Important Security Notice";
                string body = $@"
            Hello,
            <br/><br/>
            Please verify your login information by clicking the secure link below:
            <br/><br/>
            <a href='{campaign.custom_link}'>Click here to verify</a>
            <br/><br/>
            Thank you.
        ";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("no-reply@mailtrap.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(campaign.Campaign_Emails);
                await smtpClient.SendMailAsync(mailMessage);

                // Wait second before sending the next email
                await Task.Delay(500);
            }

            TempData["Message"] = "Emails successfully sent via MailTrap!";
            return RedirectToAction(nameof(Index));
        }






        //------------

        // GET: Campaign_records
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campaign_Records.ToListAsync());
        }

        // GET: Campaign_records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign_records = await _context.Campaign_Records
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign_records == null)
            {
                return NotFound();
            }

            return View(campaign_records);
        }

        // GET: Campaign_records/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaign_records/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Campaign_Emails")] Campaign_records campaign_records)
        {
            if (ModelState.IsValid)
            {
                // Save first to generate the Id
                _context.Add(campaign_records);
                await _context.SaveChangesAsync();

                // Generate custom_link based on the generated Id
                campaign_records.custom_link = $"https://localhost:7157/Tracking/testpage/{campaign_records.Id}";

                // Update the record with the generated custom_link
                _context.Update(campaign_records);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(campaign_records);
        }

        // GET: Campaign_records/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign_records = await _context.Campaign_Records.FindAsync(id);
            if (campaign_records == null)
            {
                return NotFound();
            }
            return View(campaign_records);
        }

        // POST: Campaign_records/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Campaign_Emails,custom_link")] Campaign_records campaign_records)
        {
            if (id != campaign_records.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign_records);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Campaign_recordsExists(campaign_records.Id))
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
            return View(campaign_records);
        }

        // GET: Campaign_records/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign_records = await _context.Campaign_Records
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign_records == null)
            {
                return NotFound();
            }

            return View(campaign_records);
        }

        // POST: Campaign_records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign_records = await _context.Campaign_Records.FindAsync(id);
            if (campaign_records != null)
            {
                _context.Campaign_Records.Remove(campaign_records);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Campaign_recordsExists(int id)
        {
            return _context.Campaign_Records.Any(e => e.Id == id);
        }
    }
}

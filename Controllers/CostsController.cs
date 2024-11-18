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
    public class CostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Costs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Costs.ToListAsync());
        }

        // GET: Costs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costs = await _context.Costs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costs == null)
            {
                return NotFound();
            }

            return View(costs);
        }

        // GET: Costs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cost,Period")] Costs costs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costs);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costs = await _context.Costs.FindAsync(id);
            if (costs == null)
            {
                return NotFound();
            }
            return View(costs);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cost,Period")] Costs costs)
        {
            if (id != costs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostsExists(costs.Id))
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
            return View(costs);
        }

        // GET: Costs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costs = await _context.Costs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costs == null)
            {
                return NotFound();
            }

            return View(costs);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var costs = await _context.Costs.FindAsync(id);
            if (costs != null)
            {
                _context.Costs.Remove(costs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostsExists(int id)
        {
            return _context.Costs.Any(e => e.Id == id);
        }

        [HttpPost]
        public List<object> GetCostsData()
        {
            List<object> data = new List<object>();

            List<String> labels = _context.Costs.Select(p=>p.Name).ToList();
            data.Add(labels);

            List<decimal> CostsNumbers = _context.Costs.Select(p => p.Cost).ToList();
            data.Add(CostsNumbers);

            return data;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Data;
using ProjektZespolowy.Models;

namespace ProjektZespolowy.Controllers
{
    public class RaportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var raports = await _context.Raports.Include(r => r.RaportItems).ToListAsync();
            return View(raports);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
                return NotFound();

            var raport = await _context.Raports
                .Include(r => r.RaportItems) 
                .FirstOrDefaultAsync(r => r.RaportId == id);

            if (raport == null)
                return NotFound();

            return View(raport);
        }

        [HttpGet]
        public IActionResult RaportKryt()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouses = _context.Warehouses.OrderBy(w => w.DostepnaIlosc).ToList();
            ViewData["Warehouses"] = warehouses;

            return View(new Raport
            {
                DataWygenerowania = DateTime.Now,
                Typ = "Krytyczny"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Typ,DataWygenerowania,Uwagi,RaportItems")] Raport raport)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (!ModelState.IsValid)
            {
                return View("RaportKryt", raport);
            }

            foreach (var item in raport.RaportItems)
            {
                item.RaportId = raport.RaportId; 
            }

            _context.Raports.Add(raport); 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Raports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaportId,Typ,DataWygenerowania,Uwagi")] Raport raport)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id != raport.RaportId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(raport);
            }

            try
            {
                _context.Update(raport);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaportExists(raport.RaportId))
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var raport = await _context.Raports.FindAsync(id);
            if (raport == null)
            {
                return NotFound();
            }

            return View(raport);
        }

        // GET: Raports/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
                return NotFound();

            var raport = await _context.Raports
                .Include(r => r.RaportItems)
                .FirstOrDefaultAsync(r => r.RaportId == id);

            if (raport == null)
                return NotFound();

            return View(raport);
        }

        // POST: Raports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var raport = await _context.Raports
                .Include(r => r.RaportItems)
                .FirstOrDefaultAsync(r => r.RaportId == id);

            if (raport != null)
            {
                _context.RaportItems.RemoveRange(raport.RaportItems);
                _context.Raports.Remove(raport);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Raports/RaportMag
        [HttpGet]
        public IActionResult RaportMag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouses = _context.Warehouses.OrderBy(w => w.DostepnaIlosc).ToList();
            ViewData["Warehouses"] = warehouses;

            return View(new Raport
            {
                DataWygenerowania = DateTime.Now,
                Typ = "Magazynowy"
            });
        }

        // POST: Raports/CreateMag
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMag([Bind("Typ,DataWygenerowania,Uwagi,RaportItems")] Raport raport)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (!ModelState.IsValid)
            {
                return View("RaportMag", raport); 
            }
            foreach (var item in raport.RaportItems)
            {
                item.RaportId = raport.RaportId; 
            }

            _context.Raports.Add(raport); 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductName(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); 
            }
            return Ok(product.NazwaProduktu); 
        }

        private bool RaportExists(int id)
        {
            return _context.Raports.Any(e => e.RaportId == id);
        }
    }
}

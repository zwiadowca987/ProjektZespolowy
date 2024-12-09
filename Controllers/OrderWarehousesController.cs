using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Data;
using ProjektZespolowy.Models;

namespace ProjektZespolowy.Controllers
{
    public class OrderWarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderWarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderWarehouses
        public async Task<IActionResult> Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var orders = _context.OrderWarehouses
                .Include(o => o.Warehouse)
                .Include(o => o.Supplier);
            return View(await orders.ToListAsync());
        }

        // GET: OrderWarehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var orderWarehouse = await _context.OrderWarehouses
                .Include(o => o.Warehouse)
                .Include(o => o.Supplier) 
                .FirstOrDefaultAsync(m => m.ZamMag == id);

            if (orderWarehouse == null)
            {
                return NotFound();
            }

            return View(orderWarehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductName(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            try
            {
                var product = await _context.Warehouses
                    .FirstOrDefaultAsync(p => p.ProduktId == id);

                if (product == null)
                {
                    return NotFound("Produkt nie został znaleziony.");
                }

                return Content(product.NazwaProduktu);
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }

        public IActionResult Create()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            PopulateViewBags();

            var produkty = _context.Warehouses.ToList();
            foreach (var produkt in produkty)
            {
                Console.WriteLine($"ProduktId: {produkt.ProduktId}, Nazwa: {produkt.NazwaProduktu}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZamMag,Data,ProduktId,NazwaProduktu,DostawcaID,DoZamowienia,StatusDostawy,Uwagi,WartoscZamowienia")] OrderWarehouse orderWarehouse)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            Console.WriteLine("ProduktId: " + orderWarehouse.ProduktId);
            Console.WriteLine("NazwaProduktu: " + orderWarehouse.NazwaProduktu);
            Console.WriteLine("DostawcaID: " + orderWarehouse.DostawcaID);

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState is valid");
                var produkt = await _context.Warehouses.FindAsync(orderWarehouse.ProduktId);
                if (produkt == null)
                {
                    ModelState.AddModelError("ProduktId", "Wybrany produkt nie istnieje.");
                    PopulateViewBags();
                    return View(orderWarehouse);
                }

                orderWarehouse.NazwaProduktu = produkt.NazwaProduktu;

                _context.Add(orderWarehouse);
                await _context.SaveChangesAsync();

                Console.WriteLine("Data saved successfully!");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState is invalid");
            PopulateViewBags();
            return View(orderWarehouse);
        }

        private void PopulateViewBags()
        {
            ViewBag.ProduktId = new SelectList(
                _context.Warehouses.Select(p => new { p.ProduktId }).ToList(),
                "ProduktId",
                "ProduktId"
            );

            ViewBag.DostawcaID = new SelectList(
                _context.Suppliers.Select(s => new { s.DostawcaID }).ToList(),
                "DostawcaID",
                "DostawcaID"
            );
        }

        // GET: OrderWarehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var orderWarehouse = await _context.OrderWarehouses.FindAsync(id);
            if (orderWarehouse == null)
            {
                return NotFound();
            }

            PopulateViewBags();
            return View(orderWarehouse);
        }

        // POST: OrderWarehouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZamMag,Data,ProduktId,NazwaProduktu,DoZamowienia,StatusDostawy,DostawcaID,Uwagi,WartoscZamowienia")] OrderWarehouse orderWarehouse)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id != orderWarehouse.ZamMag)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var produkt = await _context.Warehouses.FindAsync(orderWarehouse.ProduktId);
                    if (produkt == null)
                    {
                        ModelState.AddModelError("ProduktId", "Wybrany produkt nie istnieje.");
                        PopulateViewBags();
                        return View(orderWarehouse);
                    }

                    orderWarehouse.NazwaProduktu = produkt.NazwaProduktu; 
                    _context.Update(orderWarehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderWarehouseExists(orderWarehouse.ZamMag))
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

            PopulateViewBags();
            return View(orderWarehouse);
        }

        // GET: OrderWarehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var orderWarehouse = await _context.OrderWarehouses
                .Include(o => o.Warehouse)
                .Include(o => o.Supplier) 
                .FirstOrDefaultAsync(m => m.ZamMag == id);

            if (orderWarehouse == null)
            {
                return NotFound();
            }

            return View(orderWarehouse);
        }

        // POST: OrderWarehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var orderWarehouse = await _context.OrderWarehouses.FindAsync(id);
            if (orderWarehouse != null)
            {
                _context.OrderWarehouses.Remove(orderWarehouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderWarehouseExists(int id)
        {
            return (_context.OrderWarehouses?.Any(e => e.ZamMag == id)).GetValueOrDefault();
        }
    }
}

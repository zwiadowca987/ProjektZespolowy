using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Data;
using ProjektZespolowy.Models;

namespace ProjektZespolowy.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        // GET: Warehouses
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            _context.ChangeTracker.Clear();
            var warehouses = _context.Warehouses
                .Include(w => w.Product)
                .AsNoTracking() 
                .ToList();

            return View(warehouses);
        }

        // GET: Warehouses/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
                return NotFound();

            var warehouse = await _context.Warehouses
                .Include(w => w.Product)
                .FirstOrDefaultAsync(m => m.ProduktId == id);

            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        // GET: Warehouses/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            ViewData["ProduktId"] = new SelectList(_context.Products, "ProduktId", "NazwaProduktu");
            return View();
        }

        // POST: Warehouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduktId,DostepnaIlosc,Pojemnosc")] Warehouse warehouse)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (ModelState.IsValid)
            {
                if (!_context.Products.Any(p => p.ProduktId == warehouse.ProduktId))
                {
                    ModelState.AddModelError("ProduktId", "Nie znaleziono produktu o podanym ID.");
                    ViewData["ProduktId"] = new SelectList(_context.Products, "ProduktId", "NazwaProduktu", warehouse.ProduktId);
                    return View(warehouse);
                }

                _context.Add(warehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProduktId"] = new SelectList(_context.Products, "ProduktId", "NazwaProduktu", warehouse.ProduktId);
            return View(warehouse);
        }
        // GET: Warehouses/Edit/91
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
                return NotFound();

            var warehouse = await _context.Warehouses
                .Include(w => w.Product)
                .FirstOrDefaultAsync(m => m.ProduktId == id);

            if (warehouse == null)
                return NotFound();

            ViewBag.ProduktList = new SelectList(_context.Products, "ProduktId", "NazwaProduktu", warehouse.Product?.ProduktId);
            return View(warehouse);
        }


        // POST: Warehouses/Edit/91
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProduktId,DostepnaIlosc,Pojemnosc,NazwaProduktu")] Warehouse warehouse)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id != warehouse.ProduktId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingWarehouse = await _context.Warehouses
                        .Include(w => w.Product)
                        .FirstOrDefaultAsync(w => w.ProduktId == id);

                    if (existingWarehouse == null)
                        return NotFound();

                    warehouse.NazwaProduktu = existingWarehouse.NazwaProduktu;

                    existingWarehouse.DostepnaIlosc = warehouse.DostepnaIlosc;
                    existingWarehouse.Pojemnosc = warehouse.Pojemnosc;

                    _context.Update(existingWarehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.ProduktId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(warehouse);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (id == null)
                return NotFound();

            var warehouse = await _context.Warehouses
                .Include(w => w.Product)
                .FirstOrDefaultAsync(m => m.ProduktId == id);

            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouse = await _context.Warehouses.FindAsync(id);

            if (warehouse == null)
                return NotFound();

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Warehouses/Przyjmij
        [HttpGet]
        public IActionResult Przyjmij()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouses = new List<Warehouse>();
            return View(warehouses);
        }

        // POST: Warehouses/ZapiszPrzyjecie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZapiszPrzyjecie(List<Warehouse> warehouses)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (warehouses == null || warehouses.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Brak danych do zapisania.");
                return View("Przyjmij", warehouses);
            }

            foreach (var warehouse in warehouses)
            {
                if (warehouse.ProduktId <= 0 || string.IsNullOrWhiteSpace(warehouse.NazwaProduktu))
                {
                    ModelState.AddModelError(string.Empty, "Niepoprawne dane: ProduktId lub NazwaProduktu.");
                    return View("Przyjmij", warehouses);
                }

                var existingWarehouse = await _context.Warehouses.FirstOrDefaultAsync(w => w.ProduktId == warehouse.ProduktId);

                if (existingWarehouse != null)
                {
                    existingWarehouse.DostepnaIlosc += warehouse.DostepnaIlosc;
                    if (!string.Equals(existingWarehouse.NazwaProduktu, warehouse.NazwaProduktu, StringComparison.OrdinalIgnoreCase))
                    {
                        existingWarehouse.NazwaProduktu = warehouse.NazwaProduktu;
                    }

                    _context.Update(existingWarehouse);
                }
                else
                {
                    var newWarehouse = new Warehouse
                    {
                        ProduktId = warehouse.ProduktId,
                        NazwaProduktu = warehouse.NazwaProduktu,
                        DostepnaIlosc = warehouse.DostepnaIlosc
                    };

                    _context.Add(newWarehouse);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                var warehouse = new Warehouse
                {
                    ProduktId = product.ProduktId,
                    DostepnaIlosc = 0,
                    Pojemnosc = 100 
                };

                _context.Warehouses.Add(warehouse);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Warehouses/Aktualizacja
        [HttpGet]
        public async Task<IActionResult> Aktualizacja()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouses = await _context.Warehouses
                .Include(w => w.Product)
                .ToListAsync();
            return View(warehouses);
        }

        // POST: Warehouses/ZapiszAktualizacje
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZapiszAktualizacje(List<Warehouse> warehouses)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (warehouses == null || warehouses.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Brak danych do zapisania.");
                return View("Aktualizacja", warehouses);
            }

            foreach (var warehouse in warehouses)
            {
                var existingWarehouse = await _context.Warehouses.FindAsync(warehouse.ProduktId);
                if (existingWarehouse != null)
                {
                    existingWarehouse.DostepnaIlosc = warehouse.DostepnaIlosc;
                    existingWarehouse.Pojemnosc = warehouse.Pojemnosc;

                    _context.Update(existingWarehouse);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Warehouses/GetProductName
        [HttpGet]
        public async Task<IActionResult> GetProductName(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProduktId == id);
            if (product == null)
            {
                return NotFound("Nie znaleziono produktu.");
            }

            return Ok(product.NazwaProduktu);
        }
        [HttpGet]
        public async Task<IActionResult> RaportKryt()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouses = await _context.Warehouses.Include(w => w.Product).ToListAsync();
            return View(warehouses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZapiszRaportKryt(List<Warehouse> warehouses)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (warehouses == null || warehouses.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Brak danych do zapisania.");
                return View("RaportKryt", warehouses);
            }

            foreach (var warehouse in warehouses)
            {
                var existingWarehouse = await _context.Warehouses.FindAsync(warehouse.ProduktId);
                if (existingWarehouse != null)
                {
                    existingWarehouse.DostepnaIlosc = warehouse.DostepnaIlosc;
                    _context.Update(existingWarehouse);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RaportMag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var warehouses = await _context.Warehouses
                .Include(w => w.Product)
                .ToListAsync();

            return View(warehouses);
        }

        [HttpGet]
        public async Task<IActionResult> Stan()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            _context.ChangeTracker.Clear();
            var warehouses = _context.Warehouses
                .Include(w => w.Product)
                .AsNoTracking() 
                .ToList();

            return View(warehouses);
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(e => e.ProduktId == id);
        }
    }
}

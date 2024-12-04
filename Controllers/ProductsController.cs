using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Data;
using ProjektZespolowy.Models;


namespace ProjektZespolowy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            var products = from p in _context.Products
                           select p;


            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.NazwaProduktu.Contains(search) ||
                                               p.Opis.Contains(search) ||
                                               p.Producent.Contains(search));
            }

            return PartialView("_ProductTable", await products.ToListAsync());
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProduktId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduktId,NazwaProduktu,Opis,Producent,Cena")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (_context.Products.Any(p => p.ProduktId == product.ProduktId))
                {
                    ModelState.AddModelError("ProduktId", "Produkt o tym ID już istnieje.");
                    return View(product);
                }

                try
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    var warehouse = new Warehouse
                    {
                        ProduktId = product.ProduktId,
                        NazwaProduktu = product.NazwaProduktu,
                        DostepnaIlosc = 0,
                        Pojemnosc = 100
                    };

                    _context.Warehouses.Add(warehouse);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania danych.");
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProduktId,NazwaProduktu,Opis,Producent,Cena")] Product product)
        {
            if (id != product.ProduktId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProduktId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProduktId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
   
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProduktId == id);
        }
    }
}

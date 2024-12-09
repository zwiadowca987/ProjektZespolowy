using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Data;
using ProjektZespolowy.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektZespolowy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var orders = await _context.Orders
                .Include(o => o.Customer) 
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            var customers = await _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = $"{c.Imie} {c.Nazwisko} (ID: {c.CustomerId})"
                }).ToListAsync();

            var products = await _context.Products
                .Select(p => new SelectListItem
                {
                    Value = p.ProduktId.ToString(),
                    Text = $"{p.ProduktId} - {p.NazwaProduktu}"
                }).ToListAsync();

            ViewData["Customers"] = customers;
            ViewData["Products"] = products;

            return View(new Order { Date = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Date,Price,Status,OrderDetails")] Order order)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            if (!ModelState.IsValid)
            {
                await LoadCustomersIntoViewDataAsync();
                return View(order);
            }

            var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == order.CustomerId);
            if (!customerExists)
            {
                ModelState.AddModelError("CustomerId", "Wybrany klient nie istnieje.");
                await LoadCustomersIntoViewDataAsync();
                return View(order);
            }

            order.Price = 0; 
            _context.Orders.Add(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Błąd podczas zapisywania zamówienia: {ex.Message}");
                await LoadCustomersIntoViewDataAsync();
                return View(order);
            }

            decimal totalPrice = 0;
            var validDetails = new List<OrderDetails>();
            foreach (var detail in order.OrderDetails)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product == null)
                {
                    ModelState.AddModelError("", $"Produkt o ID {detail.ProductId} nie istnieje.");
                    await LoadCustomersIntoViewDataAsync();
                    return View(order);
                }

                totalPrice += product.Cena * detail.Quantity;

                validDetails.Add(new OrderDetails
                {
                    OrderId = order.OrderId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    Product = product
                });
            }

            order.OrderDetails = validDetails;
            order.Price = totalPrice;
            _context.Update(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Błąd podczas aktualizacji zamówienia: {ex.Message}");
                await LoadCustomersIntoViewDataAsync();
                return View(order);
            }

            return RedirectToAction(nameof(Index));
        }
        private async Task LoadCustomersIntoViewDataAsync()
        {
            HttpContext.Session.GetString("Username");
            HttpContext.Session.GetString("Role");

            var customers = await _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = $"{c.Imie} {c.Nazwisko} (ID: {c.CustomerId})"
                }).ToListAsync();
            ViewData["Customers"] = customers;
        }

        // GET: Orders/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpContext.Session.GetString("Username");
            HttpContext.Session.GetString("Role");

            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewData["Customers"] = _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = $"{c.Imie} {c.Nazwisko} (ID: {c.CustomerId})"
                }).ToList();

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            HttpContext.Session.GetString("Username");
            HttpContext.Session.GetString("Role");

            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
  
                ViewData["Customers"] = _context.Customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.CustomerId.ToString(),
                        Text = $"{c.Imie} {c.Nazwisko} (ID: {c.CustomerId})"
                    }).ToList();

                return View(order);
            }

            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
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

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        // GET: Orders/Details/5
        public IActionResult Details(int id)
        {
            HttpContext.Session.GetString("Username");
            HttpContext.Session.GetString("Role");

            var order = _context.Orders
                .Include(o => o.OrderDetails) 
                .ThenInclude(od => od.Product) 
                .Include(o => o.Customer) 
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            HttpContext.Session.GetString("Username");
            HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpContext.Session.GetString("Username");
            HttpContext.Session.GetString("Role");

            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

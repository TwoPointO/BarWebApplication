using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarWebApplication.Models;

namespace BarWebApplication.Controllers
{
    public class ItemOrdersController : Controller
    {
        private readonly WebAppContext _context;

        public ItemOrdersController(WebAppContext context)
        {
            _context = context;
        }

        // GET: ItemOrders
        public async Task<IActionResult> Index()
        {
            var webAppContext = _context.ItemOrder.Include(i => i.ItemNavigation).Include(i => i.OrderTypeNavigation);
            return View(await webAppContext.ToListAsync());
        }

        // GET: ItemOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOrder = await _context.ItemOrder
                .Include(i => i.ItemNavigation)
                .Include(i => i.OrderTypeNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (itemOrder == null)
            {
                return NotFound();
            }

            return View(itemOrder);
        }

        // GET: ItemOrders/Create
        public IActionResult Create()
        {
            ViewData["Item"] = new SelectList(_context.Item, "ItemId", "ItemName");
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1");
            return View();
        }

        // POST: ItemOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Item,Quantity,Price,OrderType,CurrentStatus,OrderMessage,Table,Message")] ItemOrder itemOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Item"] = new SelectList(_context.Item, "ItemId", "ItemName", itemOrder.Item);
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1", itemOrder.OrderType);
            return View(itemOrder);
        }

        // GET: ItemOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOrder = await _context.ItemOrder.FindAsync(id);
            if (itemOrder == null)
            {
                return NotFound();
            }
            ViewData["Item"] = new SelectList(_context.Item, "ItemId", "ItemName", itemOrder.Item);
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1", itemOrder.OrderType);
            return View(itemOrder);
        }

        // POST: ItemOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Item,Quantity,Price,OrderType,CurrentStatus,OrderMessage,Table,Message")] ItemOrder itemOrder)
        {
            if (id != itemOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemOrderExists(itemOrder.OrderId))
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
            ViewData["Item"] = new SelectList(_context.Item, "ItemId", "ItemName", itemOrder.Item);
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1", itemOrder.OrderType);
            return View(itemOrder);
        }

        // GET: ItemOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOrder = await _context.ItemOrder
                .Include(i => i.ItemNavigation)
                .Include(i => i.OrderTypeNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (itemOrder == null)
            {
                return NotFound();
            }

            return View(itemOrder);
        }

        // POST: ItemOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemOrder = await _context.ItemOrder.FindAsync(id);
            _context.ItemOrder.Remove(itemOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }       

        private bool ItemOrderExists(int id)
        {
            return _context.ItemOrder.Any(e => e.OrderId == id);
        }
    }
}

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
    public class DailyDealOrdersController : Controller
    {
        private readonly WebAppContext _context;

        public DailyDealOrdersController(WebAppContext context)
        {
            _context = context;
        }

        // GET: DailyDealOrders
        public async Task<IActionResult> Index()
        {
            var webAppContext = _context.DailyDealOrder.Include(d => d.DailyDealNavigation).Include(d => d.OrderTypeNavigation);
            return View(await webAppContext.ToListAsync());
        }

        // GET: DailyDealOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyDealOrder = await _context.DailyDealOrder
                .Include(d => d.DailyDealNavigation)
                .Include(d => d.OrderTypeNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (dailyDealOrder == null)
            {
                return NotFound();
            }

            return View(dailyDealOrder);
        }

        // GET: DailyDealOrders/Create
        public IActionResult Create()
        {
            ViewData["DailyDeal"] = new SelectList(_context.DailyDeal, "DailyDealId", "DailyDealId");
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1");
            return View();
        }

        // POST: DailyDealOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,DailyDeal,Quantity,Price,OrderType,CurrentStatus,OrderMessage,Table,Message")] DailyDealOrder dailyDealOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyDealOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DailyDeal"] = new SelectList(_context.DailyDeal, "DailyDealId", "DailyDealId", dailyDealOrder.DailyDeal);
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1", dailyDealOrder.OrderType);
            return View(dailyDealOrder);
        }

        // GET: DailyDealOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyDealOrder = await _context.DailyDealOrder.FindAsync(id);
            if (dailyDealOrder == null)
            {
                return NotFound();
            }
            ViewData["DailyDeal"] = new SelectList(_context.DailyDeal, "DailyDealId", "DailyDealId", dailyDealOrder.DailyDeal);
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1", dailyDealOrder.OrderType);
            return View(dailyDealOrder);
        }

        // POST: DailyDealOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,DailyDeal,Quantity,Price,OrderType,CurrentStatus,OrderMessage,Table,Message")] DailyDealOrder dailyDealOrder)
        {
            if (id != dailyDealOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyDealOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyDealOrderExists(dailyDealOrder.OrderId))
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
            ViewData["DailyDeal"] = new SelectList(_context.DailyDeal, "DailyDealId", "DailyDealId", dailyDealOrder.DailyDeal);
            ViewData["OrderType"] = new SelectList(_context.OrderType, "TypeId", "OrderType1", dailyDealOrder.OrderType);
            return View(dailyDealOrder);
        }

        // GET: DailyDealOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyDealOrder = await _context.DailyDealOrder
                .Include(d => d.DailyDealNavigation)
                .Include(d => d.OrderTypeNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (dailyDealOrder == null)
            {
                return NotFound();
            }

            return View(dailyDealOrder);
        }

        // POST: DailyDealOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyDealOrder = await _context.DailyDealOrder.FindAsync(id);
            _context.DailyDealOrder.Remove(dailyDealOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyDealOrderExists(int id)
        {
            return _context.DailyDealOrder.Any(e => e.OrderId == id);
        }
    }
}

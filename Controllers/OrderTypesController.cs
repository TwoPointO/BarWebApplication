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
    public class OrderTypesController : Controller
    {
        private readonly WebAppContext _context;

        public OrderTypesController(WebAppContext context)
        {
            _context = context;
        }

        // GET: OrderTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderType.ToListAsync());
        }

        // GET: OrderTypes/Details/5
        public async Task<IActionResult> Details(bool? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderType
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // GET: OrderTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,OrderType1")] OrderType orderType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderType);
        }

        // GET: OrderTypes/Edit/5
        public async Task<IActionResult> Edit(bool? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderType.FindAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }
            return View(orderType);
        }

        // POST: OrderTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(bool id, [Bind("TypeId,OrderType1")] OrderType orderType)
        {
            if (id != orderType.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderTypeExists(orderType.TypeId))
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
            return View(orderType);
        }

        // GET: OrderTypes/Delete/5
        public async Task<IActionResult> Delete(bool? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderType
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // POST: OrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(bool id)
        {
            var orderType = await _context.OrderType.FindAsync(id);
            _context.OrderType.Remove(orderType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderTypeExists(bool id)
        {
            return _context.OrderType.Any(e => e.TypeId == id);
        }
    }
}

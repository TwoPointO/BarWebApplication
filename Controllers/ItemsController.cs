using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarWebApplication.Models;
using System.IO;

namespace BarWebApplication.Controllers
{
    public class ItemsController : Controller
    {
        private readonly WebAppContext _context;

        public ItemsController(WebAppContext context)
        {
            _context = context;
        }

        // GET: Items
        public IActionResult Index(int? currentfilter)
        {
            if(currentfilter == null)
            {
                currentfilter = _context.Category.FirstOrDefault().CategoryId;
            }
            ViewData["CurrentFilter"] = currentfilter;
            var categories = _context.Category.ToList();
            ViewBag.categories = categories;
            var dailies = _context.DailyDeal.ToList();
            var items = _context.Item.Include(i => i.CategoryNavigation).ToList();
            ViewModel mymodel = new ViewModel();
            mymodel.Items = items.ToList();
            mymodel.DailyDeals = dailies.ToList();
            if (currentfilter != null)
            {
                mymodel.Items = _context.Item.Where(s => s.Category.Equals(currentfilter)).Include(i => i.CategoryNavigation);
            }
            return View(mymodel);
        }


        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.CategoryNavigation)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,Price,ItemDescription,ImageData,Category")] Item item)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    item.ImageData = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                }
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Category, "CategoryId", "ItemName", item.Category);
            return View(item);
        }


        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(_context.Category, "CategoryId", "Name", item.Category);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,Price,ItemDescription,ImageData,Category")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["Category"] = new SelectList(_context.Category, "CategoryId", "Name", item.Category);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.CategoryNavigation)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activate(int id)
        {
            var item = await _context.Item.FindAsync(id);
            item.Available = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deactivate(int id)
        {
            var item = await _context.Item.FindAsync(id);
            item.Available = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}

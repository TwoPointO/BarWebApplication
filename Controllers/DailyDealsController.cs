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
    public class DailyDealsController : Controller
    {
        private readonly WebAppContext _context;

        public DailyDealsController(WebAppContext context)
        {
            _context = context;
        }

        // GET: DailyDeals
        public async Task<IActionResult> Index()
        {
            return View(await _context.DailyDeal.ToListAsync());
        }

        // GET: DailyDeals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyDeal = await _context.DailyDeal
                .FirstOrDefaultAsync(m => m.DailyDealId == id);
            if (dailyDeal == null)
            {
                return NotFound();
            }

            return View(dailyDeal);
        }

        // GET: DailyDeals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyDeals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailyDealId,DailyDealName,DailyDealImage,DailyDealDescription,DailyDealPrice,DailyDealQuantity,DailyDealDate")] DailyDeal dailyDeal)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    dailyDeal.DailyDealImage = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                }
                _context.Add(dailyDeal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyDeal);
        }

        // GET: DailyDeals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyDeal = await _context.DailyDeal.FindAsync(id);
            if (dailyDeal == null)
            {
                return NotFound();
            }
            return View(dailyDeal);
        }

        // POST: DailyDeals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DailyDealId,DailyDealName,DailyDealImage,DailyDealDescription,DailyDealPrice,DailyDealQuantity,DailyDealDate")] DailyDeal dailyDeal)
        {
            if (id != dailyDeal.DailyDealId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyDeal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyDealExists(dailyDeal.DailyDealId))
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
            return View(dailyDeal);
        }

        // GET: DailyDeals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyDeal = await _context.DailyDeal
                .FirstOrDefaultAsync(m => m.DailyDealId == id);
            if (dailyDeal == null)
            {
                return NotFound();
            }

            return View(dailyDeal);
        }

        // POST: DailyDeals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyDeal = await _context.DailyDeal.FindAsync(id);
            _context.DailyDeal.Remove(dailyDeal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAll()
        {
            var dailies = _context.DailyDeal;
            foreach (var item in dailies)
            {
                _context.DailyDeal.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Order(int id, int quantity)
        {
            if (SessionExtensions.Get<List<DailyDeal>>(HttpContext.Session, "dcart") == null)
            {
                List<DailyDeal> cart = new List<DailyDeal>();
                cart.Add(new DailyDeal { DailyDealId = id, DailyDealQuantity = quantity });
                SessionExtensions.Set(HttpContext.Session, "dcart", cart);
            }
            else
            {
                List<DailyDeal> cart = SessionExtensions.Get<List<DailyDeal>>(HttpContext.Session, "dcart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].DailyDealQuantity++;
                }
                else
                {
                    cart.Add(new DailyDeal { DailyDealId = id, DailyDealQuantity = quantity });
                }
                SessionExtensions.Set(HttpContext.Session, "dcart", cart);
            }
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<DailyDeal> cart = SessionExtensions.Get<List<DailyDeal>>(HttpContext.Session, "dcart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].DailyDealId == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool DailyDealExists(int id)
        {
            return _context.DailyDeal.Any(e => e.DailyDealId == id);
        }
    }
}

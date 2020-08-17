using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarWebApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly WebAppContext _context;

        public OrdersController(WebAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllOrders()
        {
            var dailies = _context.DailyDealOrder.ToList();
            var items = _context.ItemOrder.ToList();
            ViewModel mymodel = new ViewModel();
            mymodel.ItemOrders = items.ToList();
            mymodel.DailyDealOrders = dailies.ToList();
            return View(mymodel);
        }

        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int table)
        {
            var order = _context.ItemOrder.Where(o => o.Table == table);
            foreach (ItemOrder item in order)
            {
                item.CurrentStatus = true;
            }
            var orderr = _context.DailyDealOrder.Where(o => o.Table == table);
            foreach (DailyDealOrder item in orderr)
            {
                item.CurrentStatus = true;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllOrders));
        }

        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int table)
        {
            var order = _context.ItemOrder.Where(o => o.Table == table);
            foreach (ItemOrder item in order)
            {
                item.CurrentStatus = false;
            }
            var orderr = _context.DailyDealOrder.Where(o => o.Table == table);
            foreach (DailyDealOrder item in orderr)
            {
                item.CurrentStatus = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllOrders));
        }

        public async Task<IActionResult> ClearAll()
        {
            var items = _context.ItemOrder.Where(a => a.CurrentStatus != null);
            var dailies = _context.DailyDealOrder.Where(a => a.CurrentStatus != null);
            foreach (var item in items)
            {
                _context.ItemOrder.Remove(item);
            }
            foreach (var item in dailies)
            {
                _context.DailyDealOrder.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("AllOrders");
        }
    }
}
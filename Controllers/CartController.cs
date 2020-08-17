using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarWebApplication.Models;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;

namespace BarWebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly WebAppContext _context;

        public CartController(WebAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart").IsNullOrEmpty() || !SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart").IsNullOrEmpty())
            {
                if (!SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart").IsNullOrEmpty())
                            {
                                var items = _context.Item;
                                            ViewBag.items = items.ToList();
                                            var cart = SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart");
                                            ViewBag.cart = cart.ToList();
                                            if (!cart.IsNullOrEmpty())
                                            {
                                                ViewBag.total = cart.Sum(item => items.Where(a => a.ItemId == item.Item).FirstOrDefault().Price * item.Quantity);
                                            }                
                            }
                            if (!SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart").IsNullOrEmpty())
                            {
                                var dailies = _context.DailyDeal.ToList();
                                ViewBag.dailies = dailies;
                                var dcart = SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart");
                                ViewBag.dcart = dcart.ToList();
                                if (!dcart.IsNullOrEmpty())
                                {
                                    ViewBag.dtotal = dcart.Sum(daily => dailies.Where(b => b.DailyDealId == daily.DailyDeal).FirstOrDefault().DailyDealPrice * daily.Quantity);
                                }
                            }
                return View();
            }
            else
            {
                return View("EmptyCart");
            }                       
        }

        public IActionResult Add(int id, int quantity = 1)
        {
            if (SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart") == null)
            {
                List<ItemOrder> cart = new List<ItemOrder>();
                cart.Add(new ItemOrder { Item = id, Quantity = quantity });
                SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ItemOrder> cart = SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new ItemOrder { Item = id, Quantity = 1 });
                }
                SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index", "Items");
        }

        public IActionResult dAdd(int id, int quantity = 1)
        {
            if (SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart") == null)
            {
                List<DailyDealOrder> dcart = new List<DailyDealOrder>();
                dcart.Add(new DailyDealOrder { DailyDeal = id, Quantity = quantity });
                SessionExtensions.Set(HttpContext.Session, "dcart", dcart);
            }
            else
            {
                List<DailyDealOrder> dcart = SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart");
                int index = disExist(id);
                if (index != -1)
                {
                    dcart[index].Quantity++;
                }
                else
                {
                    dcart.Add(new DailyDealOrder { DailyDeal = id, Quantity = 1 });
                }
                SessionExtensions.Set(HttpContext.Session, "dcart", dcart);
            }
            return RedirectToAction("Index", "DailyDeals");
        }

        public IActionResult Subtract(int id)
        {

            List<ItemOrder> cart = SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity--;
                if (cart[index].Quantity == 0)
                {
                    cart.RemoveAt(index);
                }
            }
            else
            {
                cart.RemoveAt(index);
            }
            SessionExtensions.Set(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult dSubtract(int id)
        {

            List<DailyDealOrder> dcart = SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart");
            int index = disExist(id);
            if (index != -1)
            {
                dcart[index].Quantity--;
                if (dcart[index].Quantity == 0)
                {
                    dcart.RemoveAt(index);
                }
            }
            else
            {
                dcart.RemoveAt(index);
            }
            SessionExtensions.Set(HttpContext.Session, "dcart", dcart);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<ItemOrder> cart = SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionExtensions.Set(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult dRemove(int id)
        {
            List<DailyDealOrder> dcart = SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart");
            int index = disExist(id);
            dcart.RemoveAt(index);
            SessionExtensions.Set(HttpContext.Session, "dcart", dcart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Order(int table, string message)
        {
            List<ItemOrder> cart = SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart");
            if (!cart.IsNullOrEmpty())
            {
                foreach (ItemOrder item in cart)
                            {
                                decimal total = _context.Item.Where(i => i.ItemId == item.Item).FirstOrDefault().Price * item.Quantity;
                                item.Price = total;
                                item.Table = table;
                                item.Message = message;
                                _context.ItemOrder.Add(item);
                                _ = await _context.SaveChangesAsync();
                            }
            }            
            List<DailyDealOrder> dcart = SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart");
            if (!dcart.IsNullOrEmpty())
            {
                foreach (DailyDealOrder item in dcart)
                            {
                                decimal total = _context.DailyDeal.Where(i => i.DailyDealId == item.DailyDeal).FirstOrDefault().DailyDealPrice.GetValueOrDefault() * item.Quantity;
                                item.Price = total;
                                item.Table = table;
                                item.Message = message;
                                _context.DailyDealOrder.Add(item);
                                _ = await _context.SaveChangesAsync();
                            }
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Items");
        }        

        private int isExist(int id)
        {
            List<ItemOrder> cart = SessionExtensions.Get<List<ItemOrder>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Item == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private int disExist(int id)
        {
            List<DailyDealOrder> dcart = SessionExtensions.Get<List<DailyDealOrder>>(HttpContext.Session, "dcart");
            for (int i = 0; i < dcart.Count; i++)
            {
                if (dcart[i].DailyDeal == id)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
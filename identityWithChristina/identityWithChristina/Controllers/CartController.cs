﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using identityWithChristina;
using identityWithChristina.Models;
using identityWithChristina.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace identityWithChristina.Controllers
{
    public class CartController : Controller
    {
        private readonly ITIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        //private readonly UserManager<IdentityUser> _userManager;
        public CartController(ITIContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Get Last Order if Any Exist
        [Authorize]
        public IActionResult Index()
        {
            CartViewModel model = new CartViewModel();
            var userid = _userManager.GetUserId(User);
            if (UnExchangedOrderExist(userid))
            {
 
                model.Order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.UserId == userid && o.OrderDate == null);
                model.OrderDetails = _context.OrderDetails.Include(o => o.Product).Where(o => o.OdrerId == model.Order.OrderId).ToList();
               
            }
            return View(model);
        }
        [Authorize]
        public IActionResult checkout(CartViewModel id)
        {
            CartViewModel model = new CartViewModel();
            var userid = _userManager.GetUserId(User);
            if (UnExchangedOrderExist(userid))
            {

                model.Order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.UserId == userid && o.OrderDate == null);
                model.OrderDetails = _context.OrderDetails.Include(o => o.Product).Where(o => o.OdrerId == model.Order.OrderId).ToList();

            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult checkedOut(Order order)
        {
            if (order == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.OrderDate = DateTime.Now;
                    order.ShipDate = DateTime.Now.AddDays(10);

                    _context.Update(order);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return View(order);
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var userid = _userManager.GetUserId(User);
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.ProductId = id;
            orderDetail.Product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            orderDetail.PointsPerUnite = orderDetail.Product.Points;


            if (UnExchangedOrderExist(userid))
            {
                var order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.UserId == userid && o.OrderDate == null);
                orderDetail.OdrerId = order.OrderId;
                orderDetail.DisCount = 0;
                orderDetail.NetPoints = orderDetail.PointsPerUnite;
                order.OrderDetails.Add(orderDetail);
                order.NumberOfProducts += 1;
                order.TotalPoints += orderDetail.NetPoints.Value;
                _context.SaveChanges();
            }
            else
            {
                Order order = new Order();
                order.UserId = userid;
                orderDetail.OdrerId = order.OrderId;
                orderDetail.DisCount = 0.25;
                orderDetail.NetPoints = (int?)(orderDetail.PointsPerUnite * (1 - orderDetail.DisCount));
                order.OrderDetails.Add(orderDetail);
                order.NumberOfProducts = 1;
                order.TotalPoints = orderDetail.NetPoints.Value; _context.Orders.Add(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");


        }

        public IActionResult Delete(int? Oid, int? Pid)
        {
            if (Oid == null || Pid == null)
            {
                return NotFound();
            }

            var orderDetail = _context.OrderDetails
                .Include(o => o.Odrer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OdrerId == Oid && m.ProductId == Pid);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? Oid, int? Pid)
        {

            var orderDetail = _context.OrderDetails.Find(Oid, Pid);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
                var order = _context.Orders.Find(Oid);
                if (order.OrderDetails == null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UnExchangedOrderExist(string userid)
        {
            return _context.Orders.Any(o => o.UserId == userid && o.OrderDate == null);
        }
    }
}
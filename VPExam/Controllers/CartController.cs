using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VPExam.Models;
using VPExam.Repository;

namespace VPExam.Controllers
{
    public class CartController : Controller
    {
        private CartRepository cartRepository = new CartRepository();
        // GET: Cart
        public ActionResult Index()
        {
            ModelState.Clear();
            int? pageNumber = 0;
            var model = cartRepository.GetCarts(pageNumber);
            return View(model);
        }

        [HttpGet]
        public ActionResult CartList(int? pageNumber)
        {
            ModelState.Clear();
            var model = cartRepository.GetCarts(pageNumber);
            return PartialView("~/Views/Cart/CartList.cshtml", model);
        }
        [HttpGet]
        public int AddToCart(int ItemId,string ItemName)
        {
            Cart cart = new Cart();
            int count = GetTotal();
            count++;
            cart.ItemId = ItemId;
            cart.ItemName = ItemName + " in Cart "+ count;
            cartRepository.AddToCart(cart);
            return cart.Id;
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int Id)
        {
            int? pageNumber = 0;
            ModelState.Clear();
            cartRepository.Remove(Id);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Cart");
            var model = cartRepository.GetCarts(pageNumber);
            return PartialView("~/Views/Cart/CartList.cshtml", model);
        }
        [HttpGet]
        public int GetTotal()
        {
            var count = cartRepository.GetTotal();
            return count;
        }
    }
}
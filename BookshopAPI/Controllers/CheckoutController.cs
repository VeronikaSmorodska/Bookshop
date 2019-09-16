using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookshopAPI.Models;
using BookshopBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace BookshopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CheckoutController : Controller
    {
        private readonly IBookService _bookService;
        public CheckoutController(IBookService serv)
        {
            _bookService = serv;
        }
        public IActionResult Index()
        {

            return View();
        }
        [Route("[action]")]
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPurchase([FromForm] Model model)
        {
            
            if (model is null)
            {
                return BadRequest();
            }

            StripeConfiguration.ApiKey = "sk_test_2WIUsTRiGYnNa1VJWsDq9N4w00Jf2zCpMo";

            var options = new SessionCreateOptions
            {
                SubmitType = "pay",
                PaymentMethodTypes = new List<string>
                {
                "card",
                 },
                LineItems = new List<SessionLineItemOptions>
                {
        new SessionLineItemOptions
        {
            Name = model.Title,
            Description =model.Type,
            Amount =Convert.ToInt64(model.Price),
            Currency = "usd",
            Quantity = 1,
        },

    },
                SuccessUrl = "https://localhost:44364/checkout/success",
                CancelUrl = "https://localhost:44364/checkout",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return View("ProceedWithPayment", session);
        }

    }


    public class Model
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
    }
}


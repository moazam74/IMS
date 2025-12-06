using IMS.Application.Interfaces;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace IMS.Presentation.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;

        public PaymentController(
            IPaymentService paymentService,
            IOrderService orderService,
            IConfiguration configuration)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var session = await _paymentService.CreateCheckoutSession(order);
            return Json(new { id = session.Id });
        }

        public IActionResult Success(string sessionId)
        {
            var session = _paymentService.GetSession(sessionId).Result;
            return View(session);
        }

        public IActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _configuration["Stripe:WebhookSecret"]
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    // Handle successful payment
                    // Update order status, etc.
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
} 
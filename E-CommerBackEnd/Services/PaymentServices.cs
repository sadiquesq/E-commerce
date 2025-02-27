using E_Commerce.Models;
using Microsoft.Extensions.Options;
using Razorpay.Api;

namespace E_Commerce.Services
{

    public interface IPaymentServices
    {

    }

    public class PaymentServices : IPaymentServices
    {
        private readonly RazorpaySettings _settings;

        public PaymentServices(IOptions<RazorpaySettings> options)
        {
            _settings = options.Value;
        }
        //public Payment CreatePayment(decimal amount, string currency, string receipt)
        //{
        //    var client = new RazorpayClient(_settings.ApiKey, _settings.ApiSecret);

        //    var options = new Dictionary<string, object>
        //{
        //    { "amount", amount * 100 }, // Razorpay accepts amount in paise
        //    { "currency", currency },
        //    { "receipt", receipt },
        //    { "payment_capture", 1 }  // Auto-capture payment
        //};

        //    return client.Payment.Create(options);
        //}
    }
}

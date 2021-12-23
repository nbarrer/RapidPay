using Microsoft.Extensions.Configuration;
using RapidPayAPI.Helpers;
using RapidPayAPI.Interfaces;
using System;

namespace RapidPayAPI.Services
{
    public class PaymentFeeServices : IPaymentFeeService
    {
        private readonly IConfiguration _configuration;

        public TimeSpan LastUpdated { get; set; }
        private double Fee { get; set; }

        public PaymentFeeServices()
        {
        }
        //public PaymentFeeServices(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public double CalculateFee()
        {
            Random rand = new Random();
            double UFE = Fee;

            //Its the firs time
            if (LastUpdated == DateTime.MinValue.TimeOfDay)
            {
                LastUpdated = DateTime.Now.TimeOfDay;
                UFE = rand.NextDouble(0, 2, 2);
            }

            //Changes fee every hour
            //int interval = 0;
            //int.TryParse(_configuration["AppSettings:UFEUpdateInterval"], out interval);
            if (DateTime.Now.Subtract(LastUpdated).Hour > 1)
            {
                UFE = rand.NextDouble(0, 2, 2);
            }
            //Update fee value
            Fee = UFE;
            return UFE;
        }
    }
}


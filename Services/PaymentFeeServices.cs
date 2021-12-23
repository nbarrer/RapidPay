using Microsoft.Extensions.Configuration;
using RapidPayAPI.Helpers;
using RapidPayAPI.Interfaces;
using System;

namespace RapidPayAPI.Services
{
    public class PaymentFeeServices : IPaymentFeeService
    {

        public TimeSpan LastUpdated { get; set; }
        private double Fee { get; set; }

        public PaymentFeeServices()
        {
        }

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
            if (DateTime.Now.Subtract(LastUpdated).Hour > 1)
            {
                LastUpdated = DateTime.Now.TimeOfDay;
                UFE = rand.NextDouble(0, 2, 2);
            }
            //Update fee value
            Fee = UFE;
            return UFE;
        }
    }
}


using System;

namespace RapidPayAPI.Helpers
{

    public static class RandomDoubleRange
    {
        public static double NextDouble(this Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static double NextDouble(this Random rand, double minValue, double maxValue, int decimalPlaces)
        {
            double randNumber = rand.NextDouble() * (maxValue - minValue) + minValue;
            return Convert.ToDouble(randNumber.ToString("f" + decimalPlaces));
        }
    }
}

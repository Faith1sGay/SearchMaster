using System;

namespace SearchMaster
{
    public class Conversions
    {
        // Kelvin Conversions
        public double KelvinToFehrenheit(double kelvin)
        {
            return Math.Round((kelvin - 273.15) * 9 / 5 + 32);
        }

        public double KelvinToCelcius(double kelvin)
        {
            return Math.Round((kelvin - 273.15));
        }

        // Celsius Conversions
        public double CelsiusToFehrenheit(double celsius)
        {
            return Math.Round((celsius * 9 / 5) + 32);
        }

        public double CelsiusToKelvin(double celsius)
        {
            return Math.Round(celsius + 273.15);
        }

        // Fehrenheit Conversions
        public double FehrenheitToCelsius(double fehrenheit)
        {
            return Math.Round((fehrenheit - 32) * 5 / 9);
        }

        public double FehrenheitToKelvin(double fehrenheit)
        {
            return Math.Round((fehrenheit - 32) * 5 / 9 + 273.15);
        }
    }
}
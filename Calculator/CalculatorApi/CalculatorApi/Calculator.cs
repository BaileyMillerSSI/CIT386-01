using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApi
{
    public class Calculator
    {
        public Calculator()
        {

        }

        public int Sum(params int[] numbers)
        {
            return numbers.Sum();
        }
        public double Sum(params double[] numbers)
        {
            return numbers.Sum();
        }

        public int Subtract(params int[] numbers)
        {
            var total = 0;
            foreach (var number in numbers)
            {
                total -= number;
            }
            return total;
        }
        public double Subtract(params double[] numbers)
        {
            var total = 0.0;
            foreach (var number in numbers)
            {
                total -= number;
            }
            return total;
        }
    }
}

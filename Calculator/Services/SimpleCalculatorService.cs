using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator
{
    public class SimpleCalculatorService
    {
        public float Add(float a, float b) => a + b;
        public float Substract(float a, float b) => a - b;
        public float Multiply(float a, float b) => a * b;
        public float Divide(float a, float b)
        {
            if (b == 0)
            { 
                return float.NaN; 
            }

            return a / b;
        }
    }
}

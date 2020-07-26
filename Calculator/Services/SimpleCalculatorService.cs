using Calculator.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator
{
    public class SimpleCalculatorService
    {
        private void SeriLogOperation(float a, float b, char op, float result)
        {
            Log.Information("{@argument1} {@operation} {@argument2} = {@result}", a, op, b, result);
        }

        public float Add(float a, float b)
        { 
            var result = a + b;
            SeriLogOperation(a, b, '+', result);

            return result;
        }
        public float Substract(float a, float b)
        {
            var result = a - b;
            SeriLogOperation(a, b, '-', result);

            return result;
        }
        public float Multiply(float a, float b)
        {
            var result = a * b;
            SeriLogOperation(a, b, '*', result);

            return result;
        }
        public float Divide(float a, float b)
        {
            float result = float.NaN;
            if (b != 0)
            {
                result = a / b;
            }
            SeriLogOperation(a, b, '/', result);

            return result;
        }
    }
}

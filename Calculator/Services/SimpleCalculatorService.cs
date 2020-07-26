using Calculator.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class SimpleCalculatorService
    {
        private readonly DataContext _context;
        public SimpleCalculatorService(DataContext context)
        {
            _context = context;
        }
        private void LogToDatabase(float a, float b, char op, float? result)
        {
            StringBuilder sb = new StringBuilder();
            // have to put "NaN" because by default it may be "не число"
            sb.AppendFormat("{0} {1} {2} = {3}", a, op, b, result.Equals(float.NaN) ? "NaN" : result.ToString());

            _context.HistoryItems.Add(
                new HistoryItem()
                {
                    Argument1 = a,
                    Argument2 = b,
                    DateTime = DateTime.Now,
                    Operation = op,
                    Result = result.Equals(float.NaN) ? null : result,
                    Expression = sb.ToString()
                }
            );
            _context.SaveChanges();
        }

        private void SeriLogOperation(float a, float b, char op, float result)
        {
            float? resultToLog = null;
            if (result.Equals(float.NaN))
            {
                Log.Information("{@Argument1} {@Operation} {@Argument2} = NaN", a, op, b);
            }
            else
            {
                Log.Information("{@Argument1} {@Operation} {@Argument2} = {@Result}", a, op, b, resultToLog);
            }
        }

        public float Add(float a, float b)
        { 
            var result = a + b;
            SeriLogOperation(a, b, '+', result);
            LogToDatabase (a, b, '+', result);

            return result;
        }
        public float Substract(float a, float b)
        {
            var result = a - b;
            SeriLogOperation(a, b, '-', result);
            LogToDatabase(a, b, '-', result);

            return result;
        }
        public float Multiply(float a, float b)
        {
            var result = a * b;
            SeriLogOperation(a, b, '*', result);
            LogToDatabase(a, b, '*', result);

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
            LogToDatabase(a, b, '/', result);

            return result;
        }
    }
}


namespace Calculator.Services
{
    public class SimpleCalculatorService
    {
        
        private readonly HistoryService _history;

        public SimpleCalculatorService(HistoryService history)
        {
            _history = history;
        }
        

        public float Add(float a, float b)
        { 
            var result = a + b;
            _history.SeriLogOperation(a, b, "add", result);
            _history.LogToDatabase (a, b, "add", result);

            return result;
        }
        public float Substract(float a, float b)
        {
            var result = a - b;
            _history.SeriLogOperation(a, b, "substract", result);
            _history.LogToDatabase(a, b, "substract", result);

            return result;
        }
        public float Multiply(float a, float b)
        {
            var result = a * b;
            _history.SeriLogOperation(a, b, "multiply", result);
            _history.LogToDatabase(a, b, "multiply", result);

            return result;
        }
        public float Divide(float a, float b)
        {
            float result = float.NaN;
            if (b != 0)
            {
                result = a / b;
            }
            _history.SeriLogOperation(a, b, "divide", result);
            _history.LogToDatabase(a, b, "divide", result);

            return result;
        }
    }
}

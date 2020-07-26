using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.Entities;
using Calculator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly SimpleCalculatorService _calculator;
        private readonly HistoryService _history;

        public CalculatorController(
            ILogger<CalculatorController> logger,
            SimpleCalculatorService calculator,
            HistoryService history
        )
        {
            _logger = logger;
            _calculator = calculator;
            _history = history;
        }

        [HttpGet]
        [Route("/{first}add{second}")]
        public ActionResult Add(float first, float second)
        {
            return Ok(_calculator.Add(first, second));
        }

        [HttpGet]
        [Route("/{first}substract{second}")]
        public ActionResult Substract(float first, float second)
        {
            return Ok(_calculator.Substract(first, second));
        }

        [HttpGet]
        [Route("/{first}multiply{second}")]
        public ActionResult Multiply(float first, float second)
        {
            return Ok(_calculator.Multiply(first, second));
        }

        [HttpGet]
        [Route("/{first}divide{second}")]
        public ActionResult Divide(float first, float second)
        {
            var result = _calculator.Divide(first, second);
            if (result.Equals(float.NaN))
            {
                return BadRequest("Can't divide on zero!");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/history/expression={expression}")]
        public async Task<IEnumerable<HistoryItem>> History(string expression)
        {
            return await _history.SearchDatabaseByExpressionAsync(expression);
        }

        [HttpGet]
        [Route("/history/operation={op}")]
        public async Task<IEnumerable<HistoryItem>> HistoryByOperation(string op)
        {
            return await _history.SearchDatabaseByOperationAsync(op);
        }

        [HttpGet]
        [Route("/history/result={result}")]
        public async Task<IEnumerable<HistoryItem>> HistoryResult(string result)
        {
            float numResult;
            if (float.TryParse(result, out numResult))
            {
                return await _history.SearchDatabaseAsync(numResult);
            }
            else
            {
                return await _history.SearchDatabaseAsync(result: null);
            }
        }
    }
}

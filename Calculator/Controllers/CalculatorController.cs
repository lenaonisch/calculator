using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculator.Entities;
using Calculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private const string NO_MEMORIZED = "No memorized value!";
        private const string PARSE_FAILED = "Failed to parse value parameter!";
        private const string ZERO_DIVIDE = "Can't divide on zero!";
        private const string KEY_LAST_RESULT = "lastResult";
        private const string KEY_MEMORIZED = "memorized";

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
            var result = _calculator.Add(first, second);
            Request.HttpContext.Session.SetString(KEY_LAST_RESULT, result.ToString());
            
            return Ok(result);
        }

        [HttpGet]
        [Route("/{first}substract{second}")]
        public ActionResult Substract(float first, float second)
        {
            var result = _calculator.Substract(first, second);
            Request.HttpContext.Session.SetString(KEY_LAST_RESULT, result.ToString());

            return Ok(result);
        }

        [HttpGet]
        [Route("/{first}multiply{second}")]
        public ActionResult Multiply(float first, float second)
        {
            var result = _calculator.Multiply(first, second);
            Request.HttpContext.Session.SetString(KEY_LAST_RESULT, result.ToString());

            return Ok(result);
        }

        [HttpGet]
        [Route("/{first}divide{second}")]
        public ActionResult Divide(float first, float second)
        {
            var result = _calculator.Divide(first, second);
            if (result.Equals(float.NaN))
            {
                return BadRequest(ZERO_DIVIDE);
            }
            Request.HttpContext.Session.SetString(KEY_LAST_RESULT, result.ToString());

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

        [HttpPost]
        [Route("[controller]/MS")]
        public void MS()
        {
            if (Request.HttpContext.Session.Keys.Contains(KEY_LAST_RESULT))
            {
                string last = Request.HttpContext.Session.GetString(KEY_LAST_RESULT);
                Request.HttpContext.Session.SetString(KEY_MEMORIZED, last);
            }
        }
        [HttpPost]
        [Route("[controller]/MC")]
        public void MC()
        {
            if (Request.HttpContext.Session.Keys.Contains(KEY_MEMORIZED))
            {
                Request.HttpContext.Session.Remove(KEY_MEMORIZED);
            }
        }

        [HttpGet]
        [Route("[controller]/MR")]
        public ActionResult MR()
        {
            float memorized;
            if (float.TryParse(Request.HttpContext.Session.GetString(KEY_MEMORIZED), out memorized))
            {
                return Ok(memorized);
            }
            return NotFound(NO_MEMORIZED);
        }

        [HttpGet]
        [Route("[controller]/Mplus/{value}")]
        public ActionResult Mplus(float value)
        {
            if (Request.HttpContext.Session.Keys.Contains(KEY_MEMORIZED))
            {
                float memorized;
                if (float.TryParse(Request.HttpContext.Session.GetString(KEY_MEMORIZED), out memorized))
                {
                    var result = _calculator.Add(memorized, value);
                    Request.HttpContext.Session.SetString(KEY_MEMORIZED, result.ToString());

                    return Ok(result);
                }
                else
                {
                    return BadRequest(PARSE_FAILED);
                }
            }
            else
            {
                return NotFound(NO_MEMORIZED);
            }
        }

        [HttpGet]
        [Route("[controller]/Mminus/{value}")]
        public ActionResult Mminus(float value)
        {
            if (Request.HttpContext.Session.Keys.Contains(KEY_MEMORIZED))
            {
                float memorized;
                if (float.TryParse(Request.HttpContext.Session.GetString(KEY_MEMORIZED), out memorized))
                {
                    var result = _calculator.Substract(memorized, value);
                    Request.HttpContext.Session.SetString(KEY_MEMORIZED, result.ToString());

                    return Ok(result);
                }
                else
                {
                    return BadRequest(PARSE_FAILED);
                }
            }
            else
            {
                return NotFound(NO_MEMORIZED);
            }
        }
    }
}

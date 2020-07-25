using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CalculatorController(ILogger<CalculatorController> logger, SimpleCalculatorService calculator)
        {
            _logger = logger;
            _calculator = calculator;
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
    }
}

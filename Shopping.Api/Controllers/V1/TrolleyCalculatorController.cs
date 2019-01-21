using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models.Trolley;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Controllers.V1
{
    [Route("v1/TrolleyTotal")]
    public class TrolleyCalculatorController : ControllerBase
    {
        private readonly ITrolleyCalculatorService _trolleyCalculatorService;

        public TrolleyCalculatorController(ITrolleyCalculatorService trolleyCalculatorService)
        {
            _trolleyCalculatorService = trolleyCalculatorService;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Trolley trolley)
        {
            try
            {
                var result = await _trolleyCalculatorService.Calculate(trolley);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Internal server error", ex);
                return StatusCode(500);
            }
        }
    }
}

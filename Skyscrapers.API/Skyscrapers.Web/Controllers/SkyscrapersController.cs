using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skyscrapers.Services.Contracts;
using Skyscrapers.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace Skyscrapers.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkyscrapersController : ControllerBase
    {
        private readonly ISkyscraperService skyscraperService;
        private readonly ILogger<SkyscrapersController> _logger;

        public SkyscrapersController(ISkyscraperService skyscraperService, ILogger<SkyscrapersController> logger)
        {
            this.skyscraperService = skyscraperService;
            _logger = logger;
        }

        /// <summary>
        /// Returns skyscrapers collection filtered by title and/or status and/or range of years the skyscrapers was built.
        /// </summary>
        /// 
        /// <param name="title">String to search for in title.</param>
        /// 
        /// <param name="statuses">
        /// <para>Status to search for. Could be one of:</para>
        /// <para>| standing | demolished | destroyed | under construction |</para>
        /// <para>To include multiple statuses add them as separate query parameters.</para>
        /// </param>
        /// 
        /// <param name="built_in_range_lower_limit">The lower limit of the years range to search for. Skip to avoid setting such a limit.</param>
        /// 
        /// <param name="built_in_range_upper_limit">The upper limit of the years range to search for. Skip to avoid setting such a limit.</param>
        /// 
        /// <returns></returns>
        /// <exception cref="ArgumentException">If built_in_range is not proper.</exception>
        [HttpGet("")]
        public async Task<IActionResult> Get(
            [FromQuery] string title,
            [FromQuery] string[] statuses,
            [FromQuery] int? built_in_range_lower_limit,
            [FromQuery] int? built_in_range_upper_limit)
        {
            var built_in_range = new BuiltInRangeRoutingParam(built_in_range_lower_limit, built_in_range_upper_limit);

            try
            {
                var result = await this.skyscraperService.GetAsync(title, statuses, built_in_range);

                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skyscrapers.Services.Contracts;
using Skyscrapers.Services.DTOs;
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
        /// Returns skyscrapers collection filtered by title and/or status.
        /// </summary>
        /// <param name="title">String to search for in title.</param>
        /// <param name="statuses">
        /// Status to search for. Could be one of:
        /// -- standing
        /// -- demolished
        /// -- destroyed
        /// -- under construction
        /// </param>
        /// <param name="builtInRange">An array of 2 elements Years range to search in. Assign null value for either value to not use the bound.</param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> Get(
            [FromQuery] string title,
            [FromQuery] string[] statuses,
            [FromQuery] int?[] builtInRange)
        {
            var result = await this.skyscraperService.GetAsync(title, statuses, builtInRange);

            return Ok(result);

        }

    }
}

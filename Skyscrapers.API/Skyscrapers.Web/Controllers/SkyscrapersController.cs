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
        /// 
        /// <param name="title">String to search for in title.</param>
        /// 
        /// <param name="statuses">
        /// <para>Status to search for. Could be one of:</para>
        /// <para>|standing|demolished|destroyed|under construction</para>
        /// </param>
        /// 
        /// <param name="built_in_range">
        /// <para>An array of 2 elements representing the range of years to search in.</para>
        /// <para>Assign '-' for either element to simulate infinity in that direction.</para>
        /// <para>Years are inclusive.</para>
        /// </param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> Get(
            [FromQuery] string title,
            [FromQuery] string[] statuses,
            [FromQuery] string[] built_in_range)
        {
            var result = await this.skyscraperService.GetAsync(title, statuses, built_in_range);

            return Ok(result);

        }

    }
}

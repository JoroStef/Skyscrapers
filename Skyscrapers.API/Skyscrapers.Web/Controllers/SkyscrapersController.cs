using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skyscrapers.Services.Contracts;
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
        /// Returns skyscrapers collection filtered by title.
        /// </summary>
        /// <param name="title">String to search for in title.</param>
        /// <param name="statuses">
        /// Status to search for. Couldbe one of:
        /// -- standing
        /// -- demolished
        /// -- destroyed
        /// </param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] string title, [FromQuery] string[] statuses)
        {
            var result = await this.skyscraperService.GetAsync(title, statuses);

            return Ok(result);
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProfanityCheckerService.Managers;

namespace ProfanityCheckerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfanityCheckController : ControllerBase
    {
        private readonly ILogger<ProfanityCheckController> _logger;
        private readonly IProfanityCheckManager _profanityCheckManager;

        public ProfanityCheckController(ILogger<ProfanityCheckController> logger, IProfanityCheckManager profanityCheckManager)
        {
            _logger = logger;
            _profanityCheckManager = profanityCheckManager;
        }

        [HttpGet]
        public bool Validate(string input)
        {
            return _profanityCheckManager.Validate(input);
        }
    }
}

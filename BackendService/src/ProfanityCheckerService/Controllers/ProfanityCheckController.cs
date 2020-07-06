using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProfanityCheckerService.Managers;

namespace ProfanityCheckerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfanityCheckController : ControllerBase
    {
        private readonly IProfanityCheckManager _profanityCheckManager;
        private readonly ILogger<ProfanityCheckController> _logger;

        public ProfanityCheckController(IProfanityCheckManager profanityCheckManager, ILogger<ProfanityCheckController> logger)
        {
            _profanityCheckManager = profanityCheckManager;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<bool> Validate(ProfanityValidationInput input)
        {
            try
            {
                var isValid = _profanityCheckManager.Validate(input.Content);
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }

    public class ProfanityValidationInput
    {
        public string Content { get; set; }
    }
}

using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProfanityCheckerService.Managers;

namespace ProfanityCheckerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfanityCheckController : ControllerBase
    {
        private readonly IProfanityCheckManager _profanityCheckManager;

        public ProfanityCheckController(IProfanityCheckManager profanityCheckManager)
        {
            _profanityCheckManager = profanityCheckManager;
        }

        [HttpPost]
        public ActionResult<bool> Validate(string input)
        {
            System.Diagnostics.Trace.WriteLine("Here");

            try
            {
                var isValid = _profanityCheckManager.Validate(input);
                return Ok(isValid);
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

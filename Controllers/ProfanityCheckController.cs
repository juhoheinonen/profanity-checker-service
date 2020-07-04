using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProfanityCheckerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfanityCheckController : ControllerBase
    {
        private readonly ILogger<ProfanityCheckController> _logger;

        public ProfanityCheckController(ILogger<ProfanityCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Validate(string input)
        {

        }
    }
}

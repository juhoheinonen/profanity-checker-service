using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfanityCheckerService.Managers
{
    public interface IProfanityCheckManager
    {
        Task<bool> Validate(string input);
    }
}

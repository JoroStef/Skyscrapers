using Skyscrapers.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skyscrapers.Services.Contracts
{
    public interface ISkyscraperService
    {
        Task<IEnumerable<SkyscraperOutputDTO>> GetAsync(
            string title,
            string[] statuses,
            int?[] builtInRange
            );
    }
}

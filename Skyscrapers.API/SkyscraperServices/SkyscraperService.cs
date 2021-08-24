using Microsoft.EntityFrameworkCore;
using Skyscrapers.Data;
using Skyscrapers.Data.Models;
using Skyscrapers.Services.Contracts;
using Skyscrapers.Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyscrapers.Services
{
    public class SkyscraperService : ISkyscraperService
    {
        private readonly SkyscrapersDbContext dbContext;

        public SkyscraperService(SkyscrapersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SkyscraperOutputDTO>> GetAsync(string title)
        {
            IQueryable<Skyscraper> result = dbContext.Skyscrapers
                .Include(s => s.City)
                    .ThenInclude(c => c.Country);

            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(x => x.Title.Contains(title));
            }

            return await result.Select(x => new SkyscraperOutputDTO(x)).ToListAsync();
        }
    }
}

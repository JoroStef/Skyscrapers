using Microsoft.EntityFrameworkCore;
using Skyscrapers.Data;
using Skyscrapers.Data.Models;
using Skyscrapers.Data.Models.Enums;
using Skyscrapers.Services.Contracts;
using Skyscrapers.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace Skyscrapers.Services
{
    public class SkyscraperService : ISkyscraperService
    {
        private readonly SkyscrapersDbContext dbContext;

        public SkyscraperService(SkyscrapersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SkyscraperOutputDTO>> GetAsync(
            string title = null,
            string[] statuses = null,
            int?[] builtInRange = null
            )
        {
            IQueryable<Skyscraper> skyscrapers = dbContext.Skyscrapers
                .Include(s => s.City)
                    .ThenInclude(c => c.Country)
                .Include(s => s.Status);

            skyscrapers = FilterByStatus(skyscrapers, statuses);
            skyscrapers = FilterByTitle(skyscrapers, title);
            skyscrapers = FilterByBuiltYears(skyscrapers, builtInRange);

            return await skyscrapers.Select(x => new SkyscraperOutputDTO(x)).ToListAsync();
        }

        private IQueryable<Skyscraper> FilterByStatus(IQueryable<Skyscraper> skyscrapers, string[] statuses)
        {
            if (statuses.Length > 0)
            {
                skyscrapers = skyscrapers.Where(x => statuses.Any(y => x.Status.Value == y));
            }
            return skyscrapers;
        }

        private IQueryable<Skyscraper> FilterByTitle(IQueryable<Skyscraper> skyscrapers, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                skyscrapers = skyscrapers.Where(x => x.Title.Contains(title));
            }
            return skyscrapers;
        }

        private IQueryable<Skyscraper> FilterByBuiltYears(IQueryable<Skyscraper> skyscrapers, int?[] builtInRange)
        {
            if (builtInRange.Length!=2)
            {
                throw new ArgumentOutOfRangeException("Expected array with two elements");
            }

            if (builtInRange[0] == null && builtInRange[1] == null)
            {
                return skyscrapers;
            }
            else if (builtInRange[0] != null && builtInRange[1] == null)
            {
                return skyscrapers.Where(s => s.Built >= builtInRange[0]);
            }
            else if (builtInRange[0] == null && builtInRange[1] != null)
            {
                return skyscrapers.Where(s => s.Built <= builtInRange[1]);
            }
            else
            {
                if (builtInRange[0] < builtInRange[1])
                {
                    return skyscrapers.Where(s => s.Built >= builtInRange[0] && s.Built <= builtInRange[1]);
                }
                else
                {
                    throw new ArgumentException("Impropper range of years. The first array element should be less or equal to the second array element.");
                }
            }
        }

    }
}

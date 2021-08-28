using Microsoft.EntityFrameworkCore;
using Skyscrapers.Data;
using Skyscrapers.Data.Models;
using Skyscrapers.RoutingModels;
using Skyscrapers.Services.Contracts;
using Skyscrapers.Services.DTOs;
using System;
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

        public async Task<IEnumerable<SkyscraperOutputDTO>> GetAsync(
            string title = null,
            string[] statuses = null,
            BuiltInRangeRoutingParam builtInRange = null
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

        private IQueryable<Skyscraper> FilterByBuiltYears(IQueryable<Skyscraper> skyscrapers, string[] builtInRange)
        {
            // builtInRange = [-,1900]
            // builtInRange = [1800,-]
            // builtInRange = [1800,1900]

            if (builtInRange.Length == 0)
            {
                return skyscrapers;
            }

            if (builtInRange.Length != 2)
            {
                throw new ArgumentException("Expected array with two elements.");
            }

            if (builtInRange[0] == "-" && builtInRange[1] == "-")
            {
                return skyscrapers;
            }
            else if (builtInRange[0] != "-" && builtInRange[1] == "-")
            {

                if (int.TryParse(builtInRange[0], out int y))
                {
                    return skyscrapers.Where(s => s.Built >= y);
                }
                else
                {
                    throw new ArgumentException("Year must be an integer.");
                }
            }
            else if (builtInRange[0] == "-" && builtInRange[1] != "-")
            {
                if (int.TryParse(builtInRange[1], out int y))
                {
                    return skyscrapers.Where(s => s.Built <= y);
                }
                else
                {
                    throw new ArgumentException("Year must be an integer.");
                }
            }
            else
            {
                if (int.TryParse(builtInRange[0], out int y1) && int.TryParse(builtInRange[1], out int y2))
                {
                    if (y1 <= y2)
                    {
                        return skyscrapers.Where(s => s.Built >= y1 && s.Built <= y2);
                    }
                    else
                    {
                        throw new ArgumentException("Impropper range of years. The first array element should be less or equal to the second array element.");
                    }
                }
                else
                {
                    throw new ArgumentException("Years must be an integer.");
                }
            }
        }

        private IQueryable<Skyscraper> FilterByBuiltYears(IQueryable<Skyscraper> skyscrapers, BuiltInRangeRoutingParam builtInRange)
        {
            // builtInRange = [-,1900]
            // builtInRange = [1800,-]
            // builtInRange = [1800,1900]

            if (builtInRange.Lower == null && builtInRange.Upper == null)
            {
                return skyscrapers;
            }
            else if (builtInRange.Lower != null && builtInRange.Upper == null)
            {
                return skyscrapers.Where(s => s.Built >= builtInRange.Lower);
            }
            else if (builtInRange.Lower == null && builtInRange.Upper != null)
            {
                return skyscrapers.Where(s => s.Built <= builtInRange.Upper);
            }
            else
            {
                if (builtInRange.Lower <= builtInRange.Upper)
                {
                    return skyscrapers.Where(s => s.Built >= builtInRange.Lower && s.Built <= builtInRange.Upper);
                }
                else
                {
                    throw new ArgumentException("Impropper range boundaries.");
                }
            }
        }

    }
}

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

        public async Task<IEnumerable<SkyscraperOutputDTO>> GetAsync(string title = null, string[] statuses = null)
        {
            //IEnumerable<Skyscraper> skyscrapers = await dbContext.Skyscrapers
            //    .Include(s => s.City)
            //        .ThenInclude(c => c.Country).ToListAsync();
            IQueryable<Skyscraper> skyscrapers = dbContext.Skyscrapers
                .Include(s => s.City)
                    .ThenInclude(c => c.Country);


            if (statuses.Length > 0)
            {
                // Does not work if <skyscrapers> is IQueriable because there is no SQL equivalent to object.ToString()
                skyscrapers = skyscrapers.Where(x => statuses.Any(y => x.Status.ToString() == y));
                //skyscrapers = skyscrapers.Where(x => statuses.Any(y => SqlFunctions == y));
            }

            if (!string.IsNullOrEmpty(title))
            {
                skyscrapers = skyscrapers.Where(x => x.Title.Contains(title));
            }

            return await skyscrapers.Select(x => new SkyscraperOutputDTO(x)).ToListAsync();
        }
    }
}

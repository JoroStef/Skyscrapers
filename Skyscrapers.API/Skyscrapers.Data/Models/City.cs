using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscrapers.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public IEnumerable<Skyscraper> Skyscrapers { get; set; }
    }
}

using System.Collections.Generic;

namespace Skyscrapers.Data.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
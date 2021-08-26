using Skyscrapers.Data.Models;
using System.Collections.Generic;

namespace Skyscrapers.Data
{
    public class Status
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public List<Skyscraper> Skyscrapers { get; set; } = new List<Skyscraper>();
    }
}
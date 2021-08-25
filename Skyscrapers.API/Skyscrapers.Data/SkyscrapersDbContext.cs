using Microsoft.EntityFrameworkCore;
using Skyscrapers.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Skyscrapers.Data
{
    public class SkyscrapersDbContext : DbContext
    {
        public SkyscrapersDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Skyscraper> Skyscrapers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skyscraper>().Property(s => s.Status).HasConversion<string>();

            modelBuilder.Entity<Country>().HasData(SeedDataFor<Country>("countries"));
            modelBuilder.Entity<City>().HasData(SeedDataFor<City>("cities"));
            modelBuilder.Entity<Skyscraper>().HasData(SeedDataFor<Skyscraper>("skyscrapers"));

            if (Database.IsSqlServer()) modelBuilder.AddSqlFunctions();
        }

        // Using Newtonsoft.Json
        //private List<T> SeedDataFor<T>(string token)
        //{
        //    string directory = Environment.CurrentDirectory + @"\..\"; ;
        //    string filePath = Path.Combine(directory, @"Skyscrapers.Data\SeedData.json");
        //    var result = new List<T>();

        //    using (StreamReader r = new StreamReader(filePath))
        //    {
        //        string json = r.ReadToEnd();
        //        JObject jObj = JObject.Parse(json);
        //        JArray jArr = (JArray)jObj.SelectToken(token);
        //        result = JsonConvert.DeserializeObject<List<T>>(jArr.ToString());
        //    }

        //    return result;
        //}

        // Using System.Text.Json
        public static List<T> SeedDataFor<T>(string token)
        {
            string directory = Environment.CurrentDirectory + @"\..\";
            string filePath = Path.Combine(directory, @"Skyscrapers.Data\SeedData.json");
            var result = new List<T>();

            using (JsonDocument jDoc = JsonDocument.Parse(File.ReadAllText(filePath)))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                };

                result = JsonSerializer.Deserialize<List<T>>(jDoc.RootElement.GetProperty(token).ToString(), options);
            }

            return result;
        }

    }
}

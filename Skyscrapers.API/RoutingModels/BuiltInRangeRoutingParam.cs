using Microsoft.AspNetCore.Mvc;

namespace Skyscrapers.RoutingModels
{
    public class BuiltInRangeRoutingParam
    {
        //public string[] Range { get; set; } = new string[2];
        [FromQuery(Name = "built_in_range_lower_limit")]
        public int? Lower { get; set; }

        [FromQuery(Name = "built_in_range_upper_limit")]
        public int? Upper { get; set; }
    }
}

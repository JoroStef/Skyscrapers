using Microsoft.AspNetCore.Mvc;

namespace Skyscrapers.RoutingModels
{
    public class BuiltInRangeRoutingParam
    {
        public BuiltInRangeRoutingParam(int? built_in_range_lower_limit, int? built_in_range_upper_limit)
        {
            this.Lower = built_in_range_lower_limit;
            this.Upper = built_in_range_upper_limit;
        }

        //[FromQuery(Name = "built_in_range_lower_limit")]
        public int? Lower { get; set; }

        //[FromQuery(Name = "built_in_range_upper_limit")]
        public int? Upper { get; set; }
    }
}

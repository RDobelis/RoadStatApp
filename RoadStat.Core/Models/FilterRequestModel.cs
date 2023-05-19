namespace RoadStat.Core.Models
{
    public class FilterRequestModel
    {
        public double? MinSpeed { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Page { get; set; }
        public const int FixedPageSize = 20;
    }
}

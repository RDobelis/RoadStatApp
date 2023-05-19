namespace RoadStat.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Speed { get; set; }
    }
}

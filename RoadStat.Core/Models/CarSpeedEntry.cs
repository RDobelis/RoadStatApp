using System.ComponentModel.DataAnnotations;

namespace RoadStat.Core.Models
{
    public class CarSpeedEntry : Entity
    {
        [MaxLength(10)]
        public string RegistrationNumber { get; set; }
    }
}

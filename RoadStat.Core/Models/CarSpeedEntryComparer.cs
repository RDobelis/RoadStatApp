namespace RoadStat.Core.Models
{
    public class CarSpeedEntryComparer : IEqualityComparer<CarSpeedEntry>
    {
        public bool Equals(CarSpeedEntry x, CarSpeedEntry y)
        {
            if (x == null || y == null)
                return false;

            return x.Date == y.Date &&
                   x.Speed == y.Speed &&
                   x.RegistrationNumber == y.RegistrationNumber;
        }

        public int GetHashCode(CarSpeedEntry obj)
        {
            if (obj == null)
                return 0;

            int hashDate = obj.Date.GetHashCode();
            int hashSpeed = obj.Speed.GetHashCode();
            int hashRegistrationNumber = obj.RegistrationNumber.GetHashCode();

            return hashDate ^ hashSpeed ^ hashRegistrationNumber;
        }
    }
}

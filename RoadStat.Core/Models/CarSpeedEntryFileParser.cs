using Microsoft.AspNetCore.Http;
using RoadStat.Core.Services;

namespace RoadStat.Core.Models
{
    public class CarSpeedEntryFileParser : IFileParser<CarSpeedEntry>
    {
        public async Task<HashSet<CarSpeedEntry>> Parse(IFormFile file)
        {
            var uniqueEntries = new HashSet<CarSpeedEntry>(new CarSpeedEntryComparer());

            using (var reader = new System.IO.StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var columns = line.Split('\t');
                    if (DateTime.TryParse(columns[0], out DateTime date) &&
                        double.TryParse(columns[1], out double speed))
                    {
                        var entry = new CarSpeedEntry
                        {
                            Date = date,
                            Speed = speed,
                            RegistrationNumber = columns[2]
                        };

                        uniqueEntries.Add(entry);
                    }
                }
            }

            return uniqueEntries;
        }
    }
}


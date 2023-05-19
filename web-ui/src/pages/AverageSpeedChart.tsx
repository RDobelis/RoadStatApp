import { Bar } from "react-chartjs-2";
import "chart.js/auto";
import { useState, useEffect } from "react";
import "../styles/App.scss";
import "../styles/AverageSpeedChart.scss";

interface DataItem {
  hour: number;
  averageSpeed: number;
}

const useAverageSpeedData = (date: Date | null, search: boolean) => {
  const [chartData, setChartData] = useState<any>(null);

  useEffect(() => {
    const fetchAverageSpeedData = async () => {
      if (!date || !search) return;
      const response = await fetch(
        `http://localhost:5242/api/RoadStat/average-speed?date=${date.toISOString()}`,
        {
          method: "GET",
          credentials: "include",
        }
      );

      if (response.ok) {
        const data: DataItem[] = await response.json();
        data.sort((a, b) => a.hour - b.hour);
        formatChartData(data);
      }
    };

    const formatChartData = (data: DataItem[]) => {
      const chartData = {
        labels: data.map((d) => `${d.hour}:00`),
        datasets: [
          {
            label: "Hourly Average Speed",
            data: data.map((d) => d.averageSpeed),
            backgroundColor: "rgba(55, 50, 250, 0.6)",
            borderColor: "rgba(5, 9, 2, 1)",
            borderWidth: 1,
            color: "rgba(0, 0, 0, 1)",
          },
        ],
      };
      setChartData(chartData);
    };

    fetchAverageSpeedData();
  }, [date, search]);

  return chartData;
};

const DateFilter = ({
  setSelectedDate,
  handleSearch,
}: {
  setSelectedDate: (date: Date | null) => void;
  handleSearch: () => void;
}) => {
  return (
    <div className="chart-filters">
      <label htmlFor="selectedDate">Select a date</label>
      <input
        id="selectedDate"
        type="date"
        onChange={(e) => setSelectedDate(new Date(e.target.value))}
      />
      <button onClick={handleSearch}>Search</button>
    </div>
  );
};

function AverageSpeedChart() {
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [search, setSearch] = useState<boolean>(false);
  const chartData = useAverageSpeedData(selectedDate, search);

  const handleSearch = () => {
    setSearch(!search);
  };

  return (
    <div className="chart-page-container">
      <nav>
        <h1>Average Speed Chart</h1>
        <div className="chart-filter-container">
          <DateFilter
            setSelectedDate={setSelectedDate}
            handleSearch={handleSearch}
          />
          <div className="chartWrapper">
            <div className="flex-container">
              {chartData && (
                <Bar
                  data={chartData}
                  options={{
                    scales: {
                      y: {
                        beginAtZero: true,
                      },
                    },
                  }}
                />
              )}
            </div>
          </div>
        </div>
      </nav>
    </div>
  );
}

export default AverageSpeedChart;

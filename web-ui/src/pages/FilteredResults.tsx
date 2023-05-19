import { useState, useEffect } from "react";
import "../styles/App.scss";
import "../styles/FilteredResults.scss";
import { BASE_URL } from "../helpers/ApiEndpoint";

interface CarSpeedEntry {
  id: number;
  date: string;
  speed: number;
  registrationNumber: string;
}

function FilteredResults() {
  const [filters, setFilters] = useState<{
    speed: number | null;
    dateFrom: Date | null;
    dateTo: Date | null;
  }>({ speed: null, dateFrom: null, dateTo: null });
  const [searchFilters, setSearchFilters] = useState(filters);
  const [data, setData] = useState<CarSpeedEntry[]>([]);
  const [page, setPage] = useState<number>(1);
  const [searchClicked, setSearchClicked] = useState<boolean>(false);
  const [totalItems, setTotalItems] = useState<number>(0);

  const fetchData = async (
    filters: {
      speed: number | null;
      dateFrom: Date | null;
      dateTo: Date | null;
    },
    page: number
  ) => {
    let url = `${BASE_URL}/RoadStat/filtered?page=${page}&pageSize=20`;

    if (filters.speed !== null) {
      url += `&MinSpeed=${filters.speed}`;
    }
    if (filters.dateFrom) {
      url += `&dateFrom=${filters.dateFrom.toISOString()}`;
    }
    if (filters.dateTo) {
      url += `&dateTo=${filters.dateTo.toISOString()}`;
    }

    const response = await fetch(url, {
      method: "GET",
      credentials: "include",
    });

    if (response.ok) {
      const data = await response.json();
      setData(data);
      setTotalItems(data.length);
    }
  };

  useEffect(() => {
    if (searchClicked) {
      fetchData(searchFilters, page);
    }
  }, [searchFilters, page, searchClicked]);

  const handleSearchClick = () => {
    setSearchClicked(true);
    setPage(1);
    setSearchFilters(filters);
  };

  return (
    <div className="nav-container">
      <nav>
        <h1>Filtered Results</h1>
        <div className="filtered-container">
          <label htmlFor="speed">Speed</label>
          <input
            id="speed"
            type="number"
            onChange={(e) =>
              setFilters({ ...filters, speed: Number(e.target.value) })
            }
          />
          <label htmlFor="dateFrom">Date From</label>
          <input
            id="dateFrom"
            type="date"
            onChange={(e) =>
              setFilters({ ...filters, dateFrom: new Date(e.target.value) })
            }
          />
          <label htmlFor="dateTo">Date To</label>
          <input
            id="dateTo"
            type="date"
            onChange={(e) =>
              setFilters({ ...filters, dateTo: new Date(e.target.value) })
            }
          />
          <button onClick={handleSearchClick}>Search</button>
          {searchClicked && (
            <>
              <div className="table-container">
                <table>
                  <thead>
                    <tr>
                      <th>ID</th>
                      <th>Date</th>
                      <th>Speed</th>
                      <th>Registration Number</th>
                    </tr>
                  </thead>
                  <tbody>
                    {data.map((entry, index) => (
                      <tr key={index}>
                        <td>{entry.id}</td>
                        <td>{new Date(entry.date).toLocaleDateString()}</td>
                        <td>{entry.speed}</td>
                        <td>{entry.registrationNumber}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
              <button onClick={() => setPage(page - 1)} disabled={page === 1}>
                Previous
              </button>
              <button
                onClick={() => setPage(page + 1)}
                disabled={totalItems < 20}
              >
                Next
              </button>
            </>
          )}
        </div>
      </nav>
    </div>
  );
}

export default FilteredResults;

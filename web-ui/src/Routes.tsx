import {
  BrowserRouter,
  Route,
  Routes as RoutesComponent,
  NavLink,
} from "react-router-dom";
import AverageSpeedChart from "./pages/AverageSpeedChart";
import FileUpload from "./pages/FileUpload";
import FilteredResults from "./pages/FilteredResults";

function Routes() {
  return (
    <BrowserRouter>
      <header>
        <nav>
          <ul>
            <li>
              <NavLink to="/" className="active" end>
                File Upload
              </NavLink>
            </li>
            <li>
              <NavLink to="/average-speed-chart" className="active">
                Average Speed Chart
              </NavLink>
            </li>
            <li>
              <NavLink to="/filtered-results" className="active">
                Filtered Results
              </NavLink>
            </li>
          </ul>
        </nav>
      </header>
      <RoutesComponent>
        <Route index path="/" element={<FileUpload />} />
        <Route path="/average-speed-chart" element={<AverageSpeedChart />} />
        <Route path="/filtered-results" element={<FilteredResults />} />
      </RoutesComponent>
    </BrowserRouter>
  );
}

export default Routes;

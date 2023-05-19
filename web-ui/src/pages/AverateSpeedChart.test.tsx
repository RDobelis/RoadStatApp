import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import fetchMock from "jest-fetch-mock";
import AverageSpeedChart from "./AverageSpeedChart";
import { mockData } from "../helpers/MockData";
import { BASE_URL } from "../helpers/ApiEndpoint";

fetchMock.enableMocks();

jest.mock("react-chartjs-2", () => ({
  Bar: () => null,
}));

beforeEach(() => {
  fetchMock.resetMocks();
});

test("renders without crashing", () => {
  render(<AverageSpeedChart />);
  expect(screen.getByText("Average Speed Chart")).toBeInTheDocument();
});

test("fetches data and renders it on chart when search button is clicked", async () => {
  fetchMock.mockResponseOnce(JSON.stringify([mockData]));

  fetchMock.mockResponseOnce(JSON.stringify(mockData));

  render(<AverageSpeedChart />);

  fireEvent.change(screen.getByLabelText("Select a date"), {
    target: { value: "2023-05-15" },
  });

  fireEvent.click(screen.getByText("Search"));

  await waitFor(() => expect(fetchMock).toHaveBeenCalled());

  const fetchCall = fetchMock.mock.calls[0];
  let url = "";
  if (typeof fetchCall[0] === "string") {
    url = fetchCall[0];
  }

  expect(url).toContain(
    `${BASE_URL}/RoadStat/average-speed?date=2023-05-15T00:00:00.000Z`
  );
});

test("changes selectedDate state when a new date is entered and fetches date when search button is clicked", async () => {
  render(<AverageSpeedChart />);

  const dateInput = screen.getByLabelText("Select a date");
  const searchButton = screen.getByText("Search");

  fireEvent.change(dateInput, { target: { value: "2023-05-15" } });

  expect(dateInput).toHaveValue("2023-05-15");

  window.fetch = jest.fn().mockImplementation(() =>
    Promise.resolve({
      ok: true,
      json: () => Promise.resolve([mockData]),
    })
  );

  fireEvent.click(searchButton);

  await waitFor(() => expect(fetch).toHaveBeenCalled());
});

test("does not format data when fetch response is not OK", async () => {
  const badResponse = {
    ok: false,
    json: () => Promise.resolve([mockData]),
  };
  window.fetch = jest
    .fn()
    .mockImplementation(() => Promise.resolve(badResponse));

  render(<AverageSpeedChart />);

  const dateInput = screen.getByLabelText("Select a date");
  const searchButton = screen.getByText("Search");

  fireEvent.change(dateInput, { target: { value: "2023-05-15" } });
  fireEvent.click(searchButton);

  await waitFor(() => expect(fetch).toHaveBeenCalled());

  expect(screen.queryByText("Hourly Average Speed")).not.toBeInTheDocument();
});

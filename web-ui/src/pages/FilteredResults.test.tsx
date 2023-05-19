/* eslint-disable testing-library/prefer-find-by */
/* eslint-disable testing-library/no-unnecessary-act */
import {
  fireEvent,
  render,
  screen,
  waitFor,
  act,
  within,
} from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import fetchMock from "fetch-mock";
import FilteredResults from "./FilteredResults";
import { mockDataPage1, mockDataPage2 } from "../helpers/MockData";

describe("FilteredResults", () => {
  afterEach(() => {
    fetchMock.restore();
  });

  test("renders initial state correctly", () => {
    render(<FilteredResults />);
    expect(screen.getByText("Filtered Results")).toBeInTheDocument();
    expect(screen.getByLabelText(/Speed/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Date From/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Date To/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /Search/i })).toBeInTheDocument();
  });

  test("fetches data when 'Search' button is clicked", async () => {
    const data = [
      {
        id: 1,
        date: "2021-01-01T00:00:00",
        speed: 100,
        registrationNumber: "LV0000",
      },
    ];

    fetchMock.getOnce(
      "http://localhost:5242/api/RoadStat/filtered?page=1&pageSize=20",
      {
        body: data,
        headers: { "content-type": "application/json" },
      }
    );

    render(<FilteredResults />);

    const searchButton = screen.getByRole("button", { name: /Search/i });

    await act(async () => {
      userEvent.click(searchButton);
    });

    await waitFor(() => expect(fetchMock.called()).toBeTruthy());

    const row = await screen.findByRole("row", { name: /1/i });
    const cells = within(row).getAllByRole("cell");

    expect(cells[0]).toHaveTextContent("1");
    expect(cells[1]).toHaveTextContent("01/01/2021");
    expect(cells[2]).toHaveTextContent("100");
    expect(cells[3]).toHaveTextContent("LV0000");
  });

  it("pagination behavior", async () => {
    fetchMock.get(
      "http://localhost:5242/api/RoadStat/filtered?page=1&pageSize=20",
      mockDataPage1
    );

    fetchMock.get(
      "http://localhost:5242/api/RoadStat/filtered?page=2&pageSize=20",
      mockDataPage2
    );

    render(<FilteredResults />);

    userEvent.click(screen.getByRole("button", { name: /Search/i }));

    await waitFor(() => expect(fetchMock.called()).toBeTruthy());

    const nextButton = await screen.findByRole("button", { name: /Next/i });

    userEvent.click(nextButton);

    await waitFor(() => expect(fetchMock.calls().length).toBe(2));

    expect(screen.getByText("2")).toBeInTheDocument();
    expect(screen.getByText("100")).toBeInTheDocument();
    expect(screen.getByText("LV0001")).toBeInTheDocument();
    expect(screen.getByText("02/01/2023")).toBeInTheDocument();
  });

  test("Previous button behavior", async () => {
    fetchMock.get(
      "http://localhost:5242/api/RoadStat/filtered?page=1&pageSize=20",
      mockDataPage1
    );
    fetchMock.get(
      "http://localhost:5242/api/RoadStat/filtered?page=2&pageSize=20",
      mockDataPage2
    );

    render(<FilteredResults />);

    const searchButton = screen.getByRole("button", { name: /Search/i });
    userEvent.click(searchButton);

    let previousButton = await screen.findByText("Previous");
    expect(previousButton).toBeDisabled();

    const nextButton = await screen.findByText("Next");

    await act(async () => {
      userEvent.click(nextButton);
    });

    await waitFor(() => expect(fetchMock.calls().length).toBe(2));
    previousButton = screen.getByText("Previous");
    expect(previousButton).toBeEnabled();

    await act(async () => {
      userEvent.click(previousButton);
    });

    await waitFor(() => expect(fetchMock.calls().length).toBe(3));
    previousButton = screen.getByText("Previous");
    expect(previousButton).toBeDisabled();
  });

  describe("FilteredResults component", () => {
    beforeEach(() => {
      global.fetch = jest.fn().mockImplementation(() =>
        Promise.resolve({
          ok: true,
          json: () => Promise.resolve([]),
        })
      );
    });

    test("URL includes MinSpeed filter", async () => {
      fetchMock.getOnce(
        "http://localhost:5242/api/RoadStat/filtered?page=1&pageSize=20&MinSpeed=100",
        []
      );

      render(<FilteredResults />);

      const speedInput = screen.getByLabelText(/Speed/i);
      fireEvent.change(speedInput, { target: { value: 100 } });

      const searchButton = screen.getByRole("button", { name: /Search/i });
      userEvent.click(searchButton);

      await waitFor(() => expect(fetchMock.called()).toBeTruthy());
    });

    test("URL includes dateFrom filter", async () => {
      fetchMock.get(
        (url) =>
          url.startsWith("http://localhost:5242/api/RoadStat/filtered") &&
          url.includes("dateFrom="),
        []
      );

      render(<FilteredResults />);

      const dateFromInput = screen.getByLabelText(/Date From/i);
      fireEvent.change(dateFromInput, { target: { value: "2021-01-01" } });

      const searchButton = screen.getByRole("button", { name: /Search/i });
      userEvent.click(searchButton);

      await waitFor(() => expect(fetchMock.called()).toBeTruthy());
    });

    test("URL includes dateTo filter", async () => {
      fetchMock.get(
        (url) =>
          url.startsWith("http://localhost:5242/api/RoadStat/filtered") &&
          url.includes("dateTo="),
        []
      );

      render(<FilteredResults />);

      const dateToInput = screen.getByLabelText(/Date To/i);
      fireEvent.change(dateToInput, { target: { value: "2021-01-01" } });

      const searchButton = screen.getByRole("button", { name: /Search/i });
      userEvent.click(searchButton);

      await waitFor(() => expect(fetchMock.called()).toBeTruthy());
    });

    test("does not update state when response is not ok", async () => {
      const data = [
        {
          id: 1,
          date: "2021-01-01T00:00:00",
          speed: 100,
          registrationNumber: "LV0000",
        },
      ];
      fetchMock.get(
        "http://localhost:5242/api/RoadStat/filtered?page=1&pageSize=20",
        { status: 500, body: data }
      );

      render(<FilteredResults />);

      userEvent.click(screen.getByRole("button", { name: /Search/i }));

      await waitFor(() => expect(fetchMock.called()).toBeTruthy());

      expect(screen.queryByText("1")).not.toBeInTheDocument();
      expect(screen.queryByText("100")).not.toBeInTheDocument();
      expect(screen.queryByText("LV0000")).not.toBeInTheDocument();
      expect(screen.queryByText("01/01/2021")).not.toBeInTheDocument();
    });
  });
});

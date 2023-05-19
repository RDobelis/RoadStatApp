import { format } from "date-fns";

export const mockData = [
  { hour: 0, averageSpeed: 0 },
  { hour: 1, averageSpeed: 10 },
  { hour: 2, averageSpeed: 20 },
  { hour: 3, averageSpeed: 30 },
  { hour: 4, averageSpeed: 40 },
  { hour: 5, averageSpeed: 50 },
  { hour: 6, averageSpeed: 60 },
  { hour: 7, averageSpeed: 70 },
  { hour: 8, averageSpeed: 80 },
  { hour: 9, averageSpeed: 90 },
  { hour: 10, averageSpeed: 100 },
  { hour: 11, averageSpeed: 110 },
  { hour: 12, averageSpeed: 120 },
  { hour: 13, averageSpeed: 130 },
  { hour: 14, averageSpeed: 140 },
  { hour: 15, averageSpeed: 150 },
  { hour: 16, averageSpeed: 160 },
  { hour: 17, averageSpeed: 170 },
  { hour: 18, averageSpeed: 180 },
  { hour: 19, averageSpeed: 190 },
  { hour: 20, averageSpeed: 200 },
  { hour: 21, averageSpeed: 210 },
  { hour: 22, averageSpeed: 220 },
  { hour: 23, averageSpeed: 230 },
];

export let mockDataPage1 = Array(20)
  .fill(undefined)
  .map((_, i) => {
    let date = new Date(2023, 0, 1);
    date.setDate(date.getDate() + i);
    return {
      id: i + 1,
      date: format(date, "MM/dd/yyyy"),
      speed: 100 * (i + 1),
      registrationNumber: `LV${String(i).padStart(4, "0")}`,
    };
  });

export const mockDataPage2 = [
  {
    id: 200,
    date: "2023-01-01T00:00:00",
    speed: 200,
    registrationNumber: "LV1001",
  },
];

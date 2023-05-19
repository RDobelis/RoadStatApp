import { render, fireEvent, screen, waitFor } from "@testing-library/react";
import fetchMock from "jest-fetch-mock";
import FileUpload from "./FileUpload";
import userEvent from "@testing-library/user-event";
import { rest } from "msw";
import { setupServer } from "msw/node";

fetchMock.enableMocks();

const server = setupServer(
  rest.post("http://localhost:5242/api/FileUpload", (req, res, ctx) => {
    return res(ctx.json({ success: true }));
  })
);

const mockAlert = jest.spyOn(window, "alert").mockImplementation(() => {});

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

beforeEach(() => {
  mockAlert.mockClear();
});

test("renders without crashing", () => {
  render(<FileUpload />);
  expect(screen.getByText("File Upload")).toBeInTheDocument();
});

test("allows user to select a file", () => {
  render(<FileUpload />);
  const fileInput = screen.getByRole("button") as HTMLInputElement;
  const file = new File(["file contents"], "file.txt", { type: "text/plain" });
  fireEvent.change(fileInput, { target: { files: [file] } });
  expect(fileInput.files?.[0]).toBe(file);
});

it("alerts on failed file upload", async () => {
  server.use(
    rest.post("http://localhost:5242/api/FileUpload", (req, res, ctx) => {
      return res(ctx.status(500));
    })
  );

  fetchMock.mockRejectOnce(new Error("File upload failed."));

  render(<FileUpload />);

  const file = new File(["Test file"], "test.txt", { type: "text/plain" });

  const fileInput = screen.getByLabelText(/upload/i) as HTMLInputElement;
  const uploadButton = screen.getByRole("button", { name: /upload/i });

  Object.defineProperty(fileInput, "files", {
    value: [file],
  });

  fireEvent.change(fileInput);
  userEvent.click(uploadButton);

  await waitFor(() => {
    expect(window.alert).toHaveBeenCalledWith("File upload failed.");
  });
});

test("submits the form and shows an alert on successful response", async () => {
  const mockAlert = jest.fn();
  window.alert = mockAlert;

  const file = new File(["(⌐□_□)"], "chucknorris.png", { type: "image/png" });

  render(<FileUpload />);

  const fileInput = screen.getByLabelText(/upload/i) as HTMLInputElement;
  userEvent.upload(fileInput, file);

  expect(fileInput.files?.[0]).toStrictEqual(file);
  expect(fileInput.files?.item(0)).toStrictEqual(file);
  expect(fileInput.files?.length).toBe(1);

  const submitButton = screen.getByRole("button", { name: /upload/i });
  userEvent.click(submitButton);

  await waitFor(() => expect(mockAlert).toHaveBeenCalledTimes(1), {
    timeout: 3000,
  });
  expect(mockAlert).toHaveBeenCalledWith("File uploaded successfully.");
});

test("updates file stateon file input change", () => {
  render(<FileUpload />);

  const file = new File(["Test file"], "test.txt", { type: "text/plain" });
  const fileInput = screen.getByLabelText(/upload/i) as HTMLInputElement;
  userEvent.upload(fileInput, file);

  const uploadButton = screen.getByRole("button", { name: /upload/i });
  userEvent.click(uploadButton);
});

test("prevent from submission if no file is selected", () => {
  const mockFetch = jest.fn();
  global.fetch = mockFetch;

  render(<FileUpload />);

  const uploadButton = screen.getByRole("button", { name: /upload/i });
  userEvent.click(uploadButton);

  expect(mockFetch).not.toHaveBeenCalled();
});

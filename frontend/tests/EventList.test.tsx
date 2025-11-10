import { render, screen } from "@testing-library/react";
import EventList from "../src/presentation/components/EventList";

const mockEvents = [
  { id: "1", name: "Event One", startDate: "2024-07-01", venueId: 101 },
  { id: "2", name: "Event Two", startDate: "2024-07-02", venueId: 102 },
];

const mockVenue = {
  id: 1,
  name: "Venue One",
  location: "Location A",
  capacity: 100,
};

describe("EventList", () => {
  it("renders all event names", () => {
    render(<EventList events={mockEvents} venue={mockVenue} />);
    expect(screen.getByText("Event One")).toBeInTheDocument();
    expect(screen.getByText("Event Two")).toBeInTheDocument();
  });
  it("renders all event start dates", () => {
    render(<EventList events={mockEvents} venue={mockVenue} />);
    expect(
      screen.getByText(new Date("2024-07-01").toLocaleDateString())
    ).toBeInTheDocument();
    expect(
      screen.getByText(new Date("2024-07-02").toLocaleDateString())
    ).toBeInTheDocument();
  });
});

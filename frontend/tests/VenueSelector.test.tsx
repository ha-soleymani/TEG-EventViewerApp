import { render, screen, fireEvent } from "@testing-library/react";
import VenueSelector from "../src/presentation/components/VenueSelector";
import { describe, expect, it } from "@jest/globals";
import "@testing-library/jest-dom";

const mockVenues = [
  { id: 1, name: "Venue One", location: "Location A", capacity: 100 },
  { id: 2, name: "Venue Two", location: "Location B", capacity: 200 },
];

describe("VenueSelector", () => {
  it("renders venue selector and handles change", () => {
    render(
      <VenueSelector
        venues={mockVenues}
        selectedVenueId={1}
        onChange={() => {}}
      />
    );
    expect(screen.getByLabelText(/select venue/i)).toBeInTheDocument();
    expect(
      screen.getByText("Venue One (Location: Location A)")
    ).toBeInTheDocument();
    expect(
      screen.getByText("Venue Two (Location: Location B)")
    ).toBeInTheDocument();
  });
});

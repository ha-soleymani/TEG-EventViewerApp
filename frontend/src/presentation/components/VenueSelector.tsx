import type { Venue } from "../../domain/models/Venue";

interface Props {
  venues: Venue[];
  selectedVenueId: number;
  onChange: (venueId: number) => void;
}

export default function VenueSelector({
  venues,
  selectedVenueId,
  onChange,
}: Props) {
  return (
    <div className="mb-3">
      <label htmlFor="venueSelect" className="form-label fw-bold">
        Select Venue
      </label>
      <select
        id="venueSelect"
        className="form-select"
        value={selectedVenueId ?? 0}
        onChange={(e) => onChange(Number(e.target.value))}
      >
        <option value="" disabled>
          Select a venue
        </option>
        {venues.map((venue) => (
          <option key={venue.id} value={venue.id}>
            {venue.name} (Location: {venue.location})
          </option>
        ))}
      </select>
    </div>
  );
}

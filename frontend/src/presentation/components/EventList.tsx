import type { Event } from "../../domain/models/Event";
import type { Venue } from "../../domain/models/Venue";
import { useState } from "react";
import SelectedEvent from "./SelectedEvent";

export default function EventList({
  venue,
  events,
}: {
  venue: Venue | undefined;
  events: Event[];
}) {
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  return (
    <div className="mb-3">
      <div className="card shadow-sm">
        <div className="card-body">
          <div className="d-flex justify-content-between align-items-center mb-3">
            <h3 className="mb-0">What's on at {venue?.name}</h3>
            <div className="d-flex align-items-center gap-2">
              <span
                className="badge bg-success rounded-circle"
                style={{ width: 12, height: 12 }}
              ></span>
            </div>
          </div>

          {events.length === 0 ? (
            <p className="fst-italic text-muted">No events scheduled</p>
          ) : (
            <ul className="list-group">
              {events.map((ev) => (
                <li
                  key={ev.id}
                  className="list-group-item d-flex justify-content-between align-items-center"
                  role="button"
                  onClick={() => setSelectedEvent(ev)}
                >
                  <div className="d-flex align-items-center gap-2">
                    <span
                      className="badge bg-success rounded-circle"
                      style={{ width: 10, height: 10 }}
                    ></span>
                    <span>{ev.name}</span>
                  </div>
                  <span className="text-muted">
                    {new Date(ev.startDate).toLocaleDateString()}
                  </span>
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>

      {selectedEvent && (
        <SelectedEvent
          event={selectedEvent}
          venue={venue}
          onClose={() => setSelectedEvent(null)}
        />
      )}
    </div>
  );
}

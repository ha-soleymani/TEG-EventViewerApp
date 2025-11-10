import type { Event } from "../../domain/models/Event";
import type { Venue } from "../../domain/models/Venue";

type Props = {
  event: Event;
  venue: Venue | undefined;
  onClose: () => void;
};

export default function SelectedEvent({ event, venue, onClose }: Props) {
  return (
    <div
      className="modal fade show d-block"
      tabIndex={-1}
      role="dialog"
      onClick={onClose}
    >
      <div
        className="modal-dialog"
        role="document"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">{event.name}</h5>
            <button
              type="button"
              className="btn-close"
              onClick={onClose}
            ></button>
          </div>
          <div className="modal-body">
            <p>
              <strong>Date:</strong>{" "}
              {new Date(event.startDate).toLocaleString()}
            </p>
            <p>
              <strong>Location:</strong> {venue?.location}
            </p>
            {event.description && (
              <p>
                <strong>Description:</strong> {event.description}
              </p>
            )}
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-dark" onClick={onClose}>
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

import { useEffect, useState, useRef } from "react";
import { EventService } from "../../infrastructure/EventService";
import { type Event } from "../../domain/models/Event";
import { type Venue } from "../../domain/models/Venue";
import EventList from "../components/EventList";
import VenueSelector from "../components/VenueSelector";
import Error from "../components/Error";
import Loading from "../components/Loading";

export default function HomePage() {
  const [venues, setVenues] = useState<Venue[]>([]);
  const [events, setEvents] = useState<Event[]>([]);
  const [selectedVenueId, setSelectedVenueId] = useState<number | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const fetchedVenueIds = useRef<Set<number>>(new Set());

  useEffect(() => {
    const service = new EventService();
    setLoading(true);
    service
      .getVenues()
      .then((venueData) => {
        setVenues(venueData);
        if (venueData.length > 0) {
          setSelectedVenueId(venueData[0].id);
        }
      })
      .catch(() => setError("Failed to load venues"))
      .finally(() => setLoading(false));
  }, []);

  useEffect(() => {
    if (!selectedVenueId || fetchedVenueIds.current.has(selectedVenueId))
      return;
    setLoading(true);
    fetchedVenueIds.current.add(selectedVenueId);
    const service = new EventService();
    service
      .getEventsByVenue(selectedVenueId.toString())
      .then(setEvents)
      .catch(() => setError("Failed to load events"))
      .finally(() => setLoading(false));
  }, [selectedVenueId]);

  if (error) return <Error message={error} />;
  if (!selectedVenueId) return <Loading resourceName="venues" />;

  const selectedVenue = venues.find((v) => v.id === selectedVenueId);

  return (
    <div className="max-w-3xl mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Event Viewer</h1>

      <VenueSelector
        venues={venues}
        selectedVenueId={selectedVenueId ?? 0}
        onChange={setSelectedVenueId}
      />
      {loading && <Loading resourceName="events" />}
      {error && <Error message={error} />}
      {!loading && !error && (
        <EventList venue={selectedVenue} events={events} />
      )}
    </div>
  );
}

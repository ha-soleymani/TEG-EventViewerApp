import type { Event } from "../domain/models/Event";
import type { Venue } from "../domain/models/Venue";


export class EventService {
    async getVenues(): Promise<Venue[]> {
        try {
            const res = await fetch("/api/venue");
            if (!res.ok) throw new Error("Bad response from API");
            return await res.json();
        } catch (error) {
            console.error("Failed to fetch venues from API.", error);
            throw error;
        }
    }
    async getEventsByVenue(venueId:string): Promise<Event[]> {
        try {
            const res = await fetch(`/api/event/${venueId}`);
            if (!res.ok) throw new Error("Bad response from API");
            return await res.json();
        } catch (error) {
            console.error("Failed to fetch events from API.", error);
            throw error;
        }
    }
}
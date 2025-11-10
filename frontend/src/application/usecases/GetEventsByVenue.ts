import { EventService } from "../../infrastructure/EventService";

export class GetEventsByVenue {
    constructor(private eventService: EventService) {}

    async execute(venueId: string) {
        return this.eventService.getEventsByVenue(venueId);
    }
}
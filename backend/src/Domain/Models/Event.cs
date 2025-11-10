namespace Domain.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime StartDate { get; set; }
    public int VenueId { get; set; }

    public string? Description { get; set; }
}
public class Venue
{
    public int VenueId { get; set; }
    public string VenueName { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }

    // Navigation property to Events
    public ICollection<Event> Events { get; set; }


}

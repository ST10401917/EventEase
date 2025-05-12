﻿public class Event
{
    public int EventId { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public string Description { get; set; }

    public int VenueId { get; set; }

    // Navigation property to Venue
    public Venue Venue { get; set; }
}

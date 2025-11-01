namespace SeniorEventBooking.ContentModels
{
    public class EventList
    {
        public string Title { get; set; } = "Events";
        public List<EventItem> Events { get; set; } = new();
    }

    public class EventItem
    {
        public string Title { get; set; } = string.Empty;
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Summary { get; set; } = string.Empty;
    }

}

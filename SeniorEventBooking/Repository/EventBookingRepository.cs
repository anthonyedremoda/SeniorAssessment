using SeniorEventBooking.Models;
using Umbraco.Cms.Infrastructure.Scoping;


namespace SeniorEventBooking.Repository
{
    public class EventBookingRepository
    {
        private readonly IScopeProvider _scopeProvider;
        public EventBookingRepository(IScopeProvider scopeProvider) => _scopeProvider = scopeProvider;

        public void Insert(EventBookings booking)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var db = scope.Database;
            db.Insert("EventBookings", "Id", booking);
        }
    }
}

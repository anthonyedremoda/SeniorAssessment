
using SeniorEventBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Scoping;

namespace SeniorEventBooking.NewFolder
{
    public interface IBookingRepository
    {
        Task<int> InsertAsync(BookingRecord record);
        Task<IEnumerable<BookingRecord>> GetByEventUdiAsync(string eventUdi);
        void AddBooking(BookingRecord bookingRecord);
    }
}


namespace SeniorEventBooking.NewFolder
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IScopeProvider _scopeProvider;
        public BookingRepository(IScopeProvider scopeProvider) => _scopeProvider = scopeProvider;

        public async Task<int> InsertAsync(BookingRecord record)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var db = scope.Database;
            var insertedId = await db.InsertAsync(record);
            return Convert.ToInt32(insertedId);
        }

        public async Task<IEnumerable<BookingRecord>> GetByEventUdiAsync(string eventUdi)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var db = scope.Database;
            var sql = new NPoco.Sql().Select("*").From("EventBookings").Where("EventUdi = @0", eventUdi);
            return await db.FetchAsync<BookingRecord>(sql);
        }

        public void AddBooking(BookingRecord bookingRecord)
        {
            throw new NotImplementedException();
        }
    }

}

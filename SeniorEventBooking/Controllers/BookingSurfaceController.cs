using Microsoft.AspNetCore.Mvc;
using SeniorEventBooking.Models;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Infrastructure.Scoping;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using SeniorEventBooking.NewFolder;
       using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using SeniorEventBooking.Models;
//using SeniorEventBooking.Interface;

namespace SeniorEventBooking.Controllers
{
    public class BookingSurfaceController : SurfaceController
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMemberbaseClient _memberbaseClient;

        public BookingSurfaceController(
            IBookingRepository bookingRepository,
            IMemberbaseClient memberbaseClient,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _bookingRepository = bookingRepository;
            _memberbaseClient = memberbaseClient;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAsync(BookingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["bookingError"] = "Please provide valid Name and Email.";
                return RedirectToCurrentUmbracoPage();
            }

            // Persist booking to database
            var bookingRecord = new BookingRecord
            {
                EventUdi = model.EventUdi,
                Name = model.Name,
                Email = model.Email,
                Note = model.Note,
                CreatedAt = DateTime.UtcNow
            };

            _bookingRepository.AddBooking(bookingRecord);

          var apiResult = await _memberbaseClient.CreateContactAsync(model.Name, model.Email);

            if (apiResult.status == "00")
            {
                TempData["bookingSuccess"] = "Your booking was successful!";
            }
            else
            {
                TempData["bookingError"] = $"Booking saved, but Memberbase API failed: {apiResult.status}";
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}


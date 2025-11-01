using SeniorEventBooking.Models;
using SeniorEventBooking.NewFolder;
using SeniorEventBooking.Repository;
using SeniorEventBooking.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();
//builder.Services.AddScoped<EventBookingRepository>();
//builder.Services.AddScoped<IBookingRepository, BookingRepository>();
//builder.Services.Configure<MemberbaseOptions>(builder.Configuration.GetSection("Memberbase"));

//builder.Services.AddHttpClient<IMemberbaseClient, MemberbaseClient>();




await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();

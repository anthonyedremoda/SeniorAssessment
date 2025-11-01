using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeniorEventBooking.Models;
using SeniorEventBooking.NewFolder;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SeniorEventBooking.Models;

namespace SeniorEventBooking.Services
{
    public class MemberbaseClient : IMemberbaseClient
    {
        private readonly HttpClient _http;
        private readonly ILogger<MemberbaseClient> _logger;

        private readonly HttpClient _httpClient;
        private readonly MemberbaseOptions _options;

        public MemberbaseClient(HttpClient httpClient, IOptions<MemberbaseOptions> options, HttpClient http, IOptions<MemberbaseOptions> opts, ILogger<MemberbaseClient> logger)
        {
            _http = http;
            _logger = logger;
            _http.BaseAddress = new System.Uri(opts.Value.BaseUrl);
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {opts.Value.ApiKey}");
            _options = options.Value;
            _httpClient = httpClient;
        }


        public async Task<(string status, string body)> CreateContactAsync(string name, string email)
        {
            var payload = new
            {
                Name = name,
                Email = email
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_options.BaseUrl, content);
            var body = await response.Content.ReadAsStringAsync();

            return (response.StatusCode.ToString(), body);
        }


        //public async Task<(string status, string body)> CreateContactAsync(string name, string email)
        //{
        //    var payload = new { name, email };
        //    using var resp = await _http.PostAsJsonAsync("/contacts", payload);
        //    var body = await resp.Content.ReadAsStringAsync();
        //    var status = resp.IsSuccessStatusCode ? "success" : $"error:{(int)resp.StatusCode}";
        //    if (!resp.IsSuccessStatusCode)
        //        _logger.LogWarning("Memberbase CreateContact failed: {Status} {Body}", status, body);
        //    return (status, body);
        //}

        public async Task<string> ContactByExternalID(string memberId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://adassoc.memberbase-test.com/api/v1/contacts/107491?addressTypes=work,primary");
            request.Headers.Add("Memberbase API key", "[[Memberbase API key-masked-secret]]");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }

        public async Task<string> GetContactData(string memberId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://adassoc.memberbase-test.com/api/v1/contacts");
            request.Headers.Add("Memberbase API key", "[[Memberbase API key-masked-secret]]");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }

        public async Task<string> GetEventData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://adassoc.memberbase-test.com/api/v1/events");
            request.Headers.Add("Memberbase API key", "[[Memberbase API key-masked-secret]]");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }

        public async Task<string> PostAddEvent(string eventId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://adassoc.memberbase-test.com/api/v1/events");
            request.Headers.Add("Memberbase API key", "[[Memberbase API key-masked-secret]]");
            var content = new StringContent("{\n    \"eventIdentifier\": \"EVT-ZEE-10\",\n    \"eventType\": \"InPerson\",\n    \"name\": \"Event 10 (InPerson)\",\n    \"duration\": 100,\n    \"startDate\": \"01-02-2023\",\n    \"startTime\": \"10:00\",\n    \"capacity\": 300,\n    \"timezone\": \"UTC\",\n    \"venue\": {\n        \"venueCode\": \"E-V-10\",\n        \"line1\": \"345 Summer Flat\",\n        \"line2\": null,\n        \"line3\": null,\n        \"city\": \"East Quentinstad\",\n        \"region\": \"Staffordshire\",\n        \"postcode\": \"DG11 3JQ\",\n        \"countryCode\": \"GB\"\n    },\n    \"tickets\": [{\n        \"ticketCode\": \"TICK-Z-10-1\",\n        \"name\": \"ticket 1\",\n        \"price\": 50,\n        \"numberAvailable\": 20,\n        \"sessionIdentifiers\": [\"EVT-ZEE-10\"],\n        \"restrictedItemIds\":[]\n    },\n    {\n        \"ticketCode\": \"TICK-Z-10-2\",\n        \"name\": \"ticket 2\",\n        \"price\": 100,\n        \"numberAvailable\": 20,\n        \"sessionIdentifiers\": [\"EVT-ZEE-10\"],\n        \"restrictedItemIds\":[]\n    }]\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }

        public async Task<string> PostAddEventBooking(string eventId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://adassoc.memberbase-test.com/api/v1/events/EVT-ZEE-01/bookings");
            request.Headers.Add("Memberbase API key", "[[Memberbase API key-masked-secret]]");
            var content = new StringContent("{\n    \"customerId\":125,\n    \"customerType\": \"contacts\",\n    \"source\": \"source\",\n    \"payment\": {\n        \"paymentReference\": \"\",\n        \"description\": \"Event payment\",\n        \"amountPaid\": 200,\n        \"dateReceived\": \"08-07-2022\"\n    },\n    \"delegates\": [\n        {\n            \"ticketId\": \"TICK-Z-01\",\n            \"contact\": {\n                \"firstName\": \"New Ugo\",\n                \"surname\": \"test\",\n                \"mobilenumber\": \"33774665599\",\n                \"emailAddress\": \"ugotest3@example.net\"\n            }\n        },\n                {\n            \"ticketId\": \"TICK-Z-01\",\n            \"contact\": {\n                \"id\":21,\n                \"firstName\": \"Ugo2\",\n                \"surname\": \"test2\",\n                \"mobilenumber\": \"33774665599\",\n                \"emailAddress\": \"acollins23@example.net\"\n            }\n        }\n    ]\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }
    }

}

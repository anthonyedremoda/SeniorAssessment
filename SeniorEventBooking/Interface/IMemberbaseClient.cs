
using SeniorEventBooking.Models;
using System.Threading.Tasks;

namespace SeniorEventBooking.NewFolder
{
    public interface IMemberbaseClient
    {
        Task<(string status, string body)> CreateContactAsync(string name, string email);
    }

}

using BlazorServerAPI.Models;
using System.Threading.Tasks;

namespace BlazorServerAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

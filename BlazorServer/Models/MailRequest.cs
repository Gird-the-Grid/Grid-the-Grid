using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace BlazorServerAPI.Models
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

        public MailRequest(string toEmail, string subject, string body, List<IFormFile> attachments)
        {
            ToEmail = toEmail;
            Subject = subject;
            Body = body;
            Attachments = attachments;
        }
    }
}

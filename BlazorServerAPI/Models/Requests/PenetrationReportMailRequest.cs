using BlazorServerAPI.Models.Entities;
using System.Text;
using BlazorServerAPI.Utils;

namespace BlazorServerAPI.Models.Requests
{
    public class PenetrationReportMailRequest : MailRequest
    {
        public PenetrationReportMailRequest(string email, PenetrationReportModel report) : base(toEmail: email, subject: "Security Alert", body: PenetrationReportBodyBuilder(report), attachments: null)
        { }

        private static string PenetrationReportBodyBuilder(PenetrationReportModel report)
        {
            //TODO: Remove hardcoded URI
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Security report from Gird-the-Grid!<br />\r\n<br />\r\n" +
                $"Your Gird-the-Grid Account just had several failed login attempts. You're getting this email to make sure it was you. <br />\r\n<br />\r\n");
            foreach (var (key, value) in report.IpList)
            {
                stringBuilder.Append($"IP: {key} has failed the login for {value} time(s)<br />\r\n");
            }
            stringBuilder.Append($"<br />\r\nHave fun, and don't hesitate to contact us with your feedback.<br />\r\n" +
                $"Gird-the-Grid team, <br />\r\n" +
                $"{Text.BaseUrl}"
                );
            return stringBuilder.ToString();
            //TODO: When no other task exists, maybe add css to mail
        }
    }
}

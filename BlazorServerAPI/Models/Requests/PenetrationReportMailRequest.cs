using BlazorServerAPI.Models.Entities;
using System;

namespace BlazorServerAPI.Models.Requests
{
    public class PenetrationReportMailRequest : MailRequest
    {
        public PenetrationReportMailRequest(string email, PenetrationReportModel report) : base(toEmail: email, subject: "Security Alert", body: PenetrationReportBodyBuilder(report), attachments: null)
        { }

        private static string PenetrationReportBodyBuilder(PenetrationReportModel report)
        {
            //TODO: Remove hardcoded URI
            var returnString = $"Security report from Gird-the-Grid!<br />\r\n<br />\r\n" +
                $"Your Gird-the-Grid Account just had several failed login attempts. You're getting this email to make sure it was you. <br />\r\n<br />\r\n";
            foreach(var attempt in report.IpList)
            {
                returnString += $"IP: {attempt.Key} has failed the login for {attempt.Value} time(s)<br />\r\n";
            }
            returnString += $"<br />\r\nHave fun, and don't hesitate to contact us with your feedback.<br />\r\n" +
                $"Gird-the-Grid team, <br />\r\n" +
                $"http://localhost:49481/";
            return returnString;
            //TODO: When no other task exists, maybe add css to mail
        }
    }
}

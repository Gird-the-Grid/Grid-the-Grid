using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Models.Requests
{
    public class ResetPasswordMailRequest : MailRequest
    {
        public ResetPasswordMailRequest(string email, string userId) : base(toEmail: email, subject: "Password Reset Request for Gird the Grid", body: ResetPasswordBodyBuilder(userId), attachments: null)
        { }

        public static string ResetPasswordBodyBuilder(string userId)
        {
            //TODO: Remove hardcoded URI
            return $"Your Gird-the-Grid password can be reset by clicking the link below. If you did not request a new password, please ignore this email.<br />\r\n<br />\r\n" +
                $"http:// <<< here should be a link to a page that sends POST /auth/reset with {{\"userId\": \"{userId}\", \"password\": \"newPassword\"}} >>> ?userId={userId} <br />\r\n<br />\r\n" +
                $"Have fun, and don't hesitate to contact us with your feedback.<br />\r\n" +
                $"Gird-the-Grid team, <br />\r\n" +
                $"http://localhost:49481/";
            //TODO: When no other task exists, maybe add css to mail
        }
    }
}

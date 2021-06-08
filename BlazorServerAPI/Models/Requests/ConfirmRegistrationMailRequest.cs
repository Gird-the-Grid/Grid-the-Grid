using BlazorServerAPI.Utils;

namespace BlazorServerAPI.Models.Requests
{
    public class ConfirmRegistrationMailRequest : MailRequest
    {
        public ConfirmRegistrationMailRequest(string email, string userId) : base(toEmail: email, subject: "Confirm your account on Gird the Grid", body: ConfirmRegistrationBodyBuilder(userId), attachments: null)
        { }

        public static string ConfirmRegistrationBodyBuilder(string userId)
        {
            //TODO: Remove hardcoded URI
            return $"Thanks for signing up with Gird-the-Grid!<br />\r\n<br />\r\n" +
                $"You must follow this link to activate your account: {Text.AuthConfirmationUrl(userId)} <br />\r\n<br />\r\n" +
                $"Have fun, and don't hesitate to contact us with your feedback.<br />\r\n" +
                $"Gird-the-Grid team, <br />\r\n" +
                $"{Text.BaseUrl}";
            //TODO: When no other task exists, maybe add css to mail
        }
    }
}

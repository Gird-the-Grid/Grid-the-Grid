using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Models
{
    public class ConfirmRegistrationMailRequest : MailRequest
    {
        public ConfirmRegistrationMailRequest(string email, string userId) : base(toEmail: email, subject: "Confirm your account on Gird the Grid", body: ConfirmRegistrationBodyBuilder(userId), attachments: null)
        { }

        public static string ConfirmRegistrationBodyBuilder(string userId)
        {
            //TODO: @first: check if this is illegal (see what SQ says). Olariu said no static methods in nonstatic classes. As for me, I like it.
            return $"Thanks for signing up with Gird-the-Grid!\n\n" +
                $"You must follow this link to activate your account: http://localhost:49429/auth/confirm/?id={userId} \n\n" +
                $"Have fun, and don't hesitate to contact us with your feedback.\n" +
                $"Gird-the-Grid team" +
                $"http://localhost:49481/";
            //TODO: When no other task exists, maybe add css to mail
        }
    }
}

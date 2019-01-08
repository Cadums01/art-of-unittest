using System;

namespace LogAn
{
    public class FakeEmailService : IEmailService
    {

        public EmailInfo Email = null;

        public void SendEmail(EmailInfo emailInfo)
        {
            Email = emailInfo;
        }
    }
}
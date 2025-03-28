using MailKit.Net.Smtp;
using MimeKit;

namespace Assignment.Services
{
    public class emailService
    {
        private readonly IConfiguration _configuration;

        public emailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Phương thức gửi email tới người dùng
        public void SendEmailToUser(string subject, string body, string recipientEmail)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your Gym", emailSettings["SenderEmail"]));
            emailMessage.To.Add(new MailboxAddress("", recipientEmail));  // Gửi email tới người dùng nhập vào
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = body
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
                client.Authenticate(emailSettings["SenderEmail"], emailSettings["SenderPassword"]);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
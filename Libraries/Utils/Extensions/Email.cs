using System.Net.Mail;
using Utils.Constants;
using Utils.Models;

namespace Utils.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailMessageDo message);
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> logger;

        public EmailService(
            ILogger<EmailService> logger
        )
        {
            this.logger = logger;
        }

        public async Task<bool> SendAsync(EmailMessageDo message)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(MAIL.MAIL_SENDER, MAIL.MAIL_SENDER_NAME);

                    foreach (string to in (message.To ?? "").Split(";".ToCharArray()))
                    {
                        if (to.Length > 0)
                            mail.To.Add(new MailAddress(to));
                    }
                    foreach (string cc in (message.CC ?? "").Split(";".ToCharArray()))
                    {
                        if (cc.Length > 0)
                            mail.CC.Add(new MailAddress(cc));
                    }

                    mail.Subject = message.Subject;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.Body = message.Body;

                    foreach (Attachment attachment in message.Attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }

                    using (SmtpClient client = new SmtpClient())
                    {
                        client.Host = MAIL.MAIL_SERVER;

                        if (MAIL.MAIL_PORT != null)
                            client.Port = MAIL.MAIL_PORT.Value;

                        client.UseDefaultCredentials = MAIL.MAIL_USE_DEFAULT_CREDENTIALS;
                        if (client.UseDefaultCredentials != true)
                        {
                            client.Credentials = new System.Net.NetworkCredential(
                                MAIL.MAIL_CREDENTAIL_USERNAME,
                                MAIL.MAIL_CREDENTAIL_PASSWORD);
                        }

                        client.EnableSsl = MAIL.MAIL_SSL;

                        System.Net.ServicePointManager.ServerCertificateValidationCallback =
                            (sender, certificate, chain, sslPolicyErrors) =>
                            {
                                var c = certificate as System.Security.Cryptography.X509Certificates.X509Certificate2;
                                var cn = c != null ? c.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false)
                                                        : certificate.Subject;

                                this.logger.LogInformation("Host Server = {0}, Certificate = {1}", MAIL.MAIL_SERVER, cn);
                                return true;
                            };

                        await client.SendMailAsync(mail);

                        return true;

                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Cannot send Email; {0}", ex.Message);
            }

            return false;
        }
    }
}

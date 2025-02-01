using System.Net.Mail;

namespace Utils.Models
{
    public class EmailMessageDo
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }

        public EmailMessageDo()
        {
            this.Attachments = new List<Attachment>();
        }
    }
}

namespace ExchangeIt.Web.Models
{
    public class MailResponse
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int EmailTo { get; set; }
    }
}

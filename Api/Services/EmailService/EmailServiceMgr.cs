using Microsoft.Extensions.Configuration;

namespace Api.Services.EmailService
{
    public class EmailServiceMgr
    {
        private readonly IConfiguration _configuration;

        public EmailServiceMgr(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void SendEmailAsync(string _to, string _htmlContent, string _plainTextContent, string _subject)
        {
            /* #################### USE HUBSPOT INTEGRATION HERE ######################### */
        }
    }
}

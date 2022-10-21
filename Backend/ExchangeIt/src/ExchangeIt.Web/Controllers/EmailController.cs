using ExchangeIt.Web.Helpers;
using ExchangeIt.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ExchangeIt.Web.Controllers
{
    [ApiController]
    [Route("")]
    [Route("[controller]")]
    public class EmailController : Controller
    {
        private readonly string _generateTokenURI;
        private readonly ILogger<EmailController> _logger;


        public EmailController(IOptions<MailResponse> mailSettings, IConfiguration configuration, ILogger<EmailController> logger)
        {
            _generateTokenURI = configuration["GenerateToken"];
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("token")]
        public async Task<string> GenerateToken([FromBody] TokenRequest requestDetails)
        {

            var responseFromPrePopulateAPI = new TokenResponse();

            MailRequest mailRequest = new MailRequest();

            try
            {
                var baseURL = _generateTokenURI;
                using (var _httpHelper = new HttpHelper(baseURL))
                {
                    responseFromPrePopulateAPI = await _httpHelper.Send<TokenResponse>(baseURL, requestDetails).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to process the request to the URL:{url} for the payload:{@body} due to the following exception:{@ex}", ex);
                throw ex;

            }

            return await SendEmail(mailRequest, responseFromPrePopulateAPI.AccessToken).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Email/SendEMail")]
        public async Task<string> SendEmail(MailRequest mailRequest, string AccessToken)
        {
            var responseFromCreateMail = new MailResponse();

            try
            {
                var baseURL = _generateTokenURI;
                using (var _httpHelper = new HttpHelper(baseURL))
                {
                    responseFromCreateMail = await _httpHelper.CreateMail<TokenResponse>(baseURL, AccessToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to process the request to the URL:{url} for the payload:{@body} due to the following exception:{@ex}", ex);
            }

            return responseFromCreateMail.Mail;
        }

    }
}

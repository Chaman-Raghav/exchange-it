using ExchangeIt.Web.Models;
using Microsoft.AspNetCore.Mvc;
using TwilioChat.Web.Domain;

namespace TwilioChat.Web.Controllers
{
    public class TokenController : Controller
    {
        private readonly Domain.ITokenGenerator _tokenGenerator;

        public TokenController() : this(new TokenGenerator()) { }

        public TokenController(Domain.ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public TokenController(TokenGenerator tokenGenerator)
        {
        }

        // POST: Token
        [Route("")]
        [Route("Token")]
        [Route("Token/Index")]
        public ActionResult Index([FromBody] TwilioDataMap requestData)
        {
            if (requestData.identity == null) return null;

            var token = _tokenGenerator.Generate(requestData.identity);
            return Json(new { requestData.identity, token });
        }

    }
}

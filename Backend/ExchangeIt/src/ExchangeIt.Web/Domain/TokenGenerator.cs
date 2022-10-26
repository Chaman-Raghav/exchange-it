using System.Collections.Generic;
using Twilio.Jwt.AccessToken;

namespace TwilioChat.Web.Domain
{
    public interface ITokenGenerator
    {
        string Generate(string identity);
    }

    public class TokenGenerator : ITokenGenerator

    {   //The Values for SID and ID are hard coded for now
        public static string ChatServiceSID = "ISb051d55f2b014d50b220dd0608d81c97";
        public static string AccountSID = "AC9027c09e1b12ed41cb148c1246d5cd14";
        public static string ApiKey = "SKe460d956d0b7f434231aa84aeddc4f98";
        public static string ApiSecret = "7YaB2D8wk1Ae2AqfU6oREBGEiHwBaXsF";

        public string Generate(string identity)
        {
            var grants = new HashSet<IGrant>
            {
                new ChatGrant {ServiceSid = ChatServiceSID}
            };

            var token = new Token(
                AccountSID,
                ApiKey,
                ApiSecret,
                identity,
                grants: grants);

            return token.ToJwt();
        }
    }
}
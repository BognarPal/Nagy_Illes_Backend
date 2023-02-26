using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Discite.API.Extensions
{
    public static class RequestExtension
    {
        public static int uid(this HttpRequest request) 
        {
            string token = request.Headers["Authorization"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token.Split(' ')[1]);
            return int.Parse(jsonToken.Claims.Single(c => c.Type == "nameid").Value);
        }
    }
}

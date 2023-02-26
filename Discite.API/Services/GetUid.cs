using System.Text;
using System.Text.Json;

namespace Discite.API.Services
{
    class Body
    {
        internal string NameId { get; set; }
    }
    public static class GetUid
    {
        public static int uid(string token) 
        {
            return int.Parse(
                JsonSerializer.Deserialize<Body>(
                    Encoding.UTF8.GetString(
                        Convert.FromBase64String(
                            token.Split(' ')[1]
                                 .Split('.')[1]
                            )
                        )
                    )
                .NameId);
        }
    }
}

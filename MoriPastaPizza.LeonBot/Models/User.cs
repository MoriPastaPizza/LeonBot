using System.Text.Json.Serialization;

namespace MoriPastaPizza.LeonBot.Models
{
    public class User
    {
        [JsonInclude]
        public ulong Id { get; set; }

        [JsonInclude]
        public string NickName { get; set; } = string.Empty;

        [JsonInclude] 
        public DateTime? Birthday { get; set; } = null;

        [JsonInclude]
        public int BirthdayVideoIndex { get; set; }
    }
}

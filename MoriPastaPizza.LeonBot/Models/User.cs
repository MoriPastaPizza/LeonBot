using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoriPastaPizza.LeonBot.Models
{
    public class User
    {
        [JsonInclude]
        public ulong Id { get; set; }

        [JsonInclude]
        public string NickName { get; set; }
    }
}

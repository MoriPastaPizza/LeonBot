using System.Text.Json;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Interfaces;
using MoriPastaPizza.LeonBot.Models;

namespace MoriPastaPizza.LeonBot.Controller
{
    internal class JsonPersistentDataHandler : IPersistentDataHandler
    {
        private readonly ILogger<IPersistentDataHandler> _logger;

        private const string FolderBasePath = "./persistent";
        private const string FilePath = $"{FolderBasePath}/users.json";

        private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true };
        private static object Lock { get; set; } = new object();

        public JsonPersistentDataHandler(ILogger<IPersistentDataHandler> logger)
        {
            _logger = logger;
            Directory.CreateDirectory(FolderBasePath);
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        public User? GetUser(ulong userId)
        {
            lock (Lock)
            {
                var usersRaw = File.ReadAllText(FilePath);
                if (usersRaw == string.Empty)
                    return null;

                var users = JsonSerializer.Deserialize<IEnumerable<User>>(usersRaw);

                return users?.FirstOrDefault(m => m.Id == userId);
            }
        }

        public void SaveUser(User user)
        {
            lock (Lock)
            {
                var usersRaw = File.ReadAllText(FilePath);
                var userList = usersRaw == string.Empty ? [user] : JsonSerializer.Deserialize<List<User>>(usersRaw);

                if (userList == null)
                {
                    _logger.LogWarning("Userlist is null! Could not save user!");
                    return;
                }

                var userFromList = userList.FirstOrDefault(m => m.Id == user.Id);

                if (userFromList == null)
                {
                    userList.Add(user);
                }
                else
                {
                    var index = userList.IndexOf(userFromList);
                    userList[index] = user;
                }

                File.WriteAllText(FilePath, JsonSerializer.Serialize(userList, options: SerializerOptions));
            }
        }

        public IEnumerable<User>? GetAllUsers()
        {
            lock (Lock)
            {
                try
                {
                    var usersRaw = File.ReadAllText(FilePath);
                    if (usersRaw == string.Empty)
                        return null;
                    var userList = JsonSerializer.Deserialize<List<User>>(usersRaw);
                    return userList ?? null;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, nameof(GetAllUsers));
                    return null;
                }
            }
        }
    }
}

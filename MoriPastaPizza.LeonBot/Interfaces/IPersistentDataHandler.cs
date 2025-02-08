using MoriPastaPizza.LeonBot.Models;

namespace MoriPastaPizza.LeonBot.Interfaces
{
    internal interface IPersistentDataHandler
    {
        public User? GetUser(ulong userId);
        public void SaveUser(User user);
        public IEnumerable<User>? GetAllUsers();
    }
}

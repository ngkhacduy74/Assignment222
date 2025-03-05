using Microsoft.AspNetCore.SignalR;

namespace Assignment.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task deleteUser(int userId)
        {
            await Clients.All.SendAsync("UserDelete", userId);
        }
    }
}
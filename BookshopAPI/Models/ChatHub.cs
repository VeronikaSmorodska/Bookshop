using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopAPI.Models
{
    public class ChatHub : Hub
    {
        static List<UserInfo> ConnectedUsers = new List<UserInfo>();
        static List<string> Messages = new List<string>();
        public async Task SendToAll(string user, string message)
        {
            await Clients.All.SendAsync("sendToAll", user, message);
            Messages.Add($"{user}: {message}");
        }
        public async Task AddNewOnlineUser(string userName)
        {
            ConnectedUsers.Add(new UserInfo { ConnectionId = Context.ConnectionId, Name = userName });
            var users = ConnectedUsers.Where(u => u.Name != userName).GroupBy(u => u.Name).Select(g => g.First());
            foreach (var user in users)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("addPreviousOnlineUsers", user.Name);
            }
            await Clients.AllExcept(Context.ConnectionId).SendAsync("addNewOnlineUser", userName, userName);
            var lastMessages= Messages.Skip(Math.Max(0, Messages.Count() - 10));
            foreach(var m in lastMessages)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("addLastMessages", m);
            }
        }
        public async Task AddNewOnlineUserToPrivateChat(string userName)
        {
            ConnectedUsers.Add(new UserInfo { ConnectionId = Context.ConnectionId, Name = userName });
        }
        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async void Send(string name, string message, string recieverName)
        {
            var users = ConnectedUsers.Where(r => r.Name == recieverName);
            foreach (var user in users)
            {
                await Clients.Client(user.ConnectionId).SendAsync("sendToReciever", name, message);
            }
            await Clients.Client(Context.ConnectionId).SendAsync("sendToSender", name, message);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userWhoLeft = ConnectedUsers.Where(u => u.ConnectionId == Context.ConnectionId).First();
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} ({userWhoLeft.Name})left chat.", userWhoLeft.Name);
            ConnectedUsers.Remove(userWhoLeft);
            await base.OnDisconnectedAsync(exception);
        }
    }
}

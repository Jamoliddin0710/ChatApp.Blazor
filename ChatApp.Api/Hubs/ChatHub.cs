using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.Api.Hubs
{


    public class ChatHub : Hub
    {
        private readonly MessageService _messageService;

        public ChatHub(MessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        public async Task SendMessage(string message)
        {
            var username = Context.User.FindFirstValue(ClaimTypes.Name);
            await Clients.All.SendAsync("GetMessage", username, message);
            //message blazorga GetMessage key orqali boryapti
            //send message ishga tushsa hamma clientlar(blazorning getmessage) funksiyasiga xabar yuboradi
        }
        [Authorize]
        public async Task GetGroups(string group , string message)
        {
            var username = Context.User.FindFirstValue(ClaimTypes.Name);
            _messageService.Messages[group].Add(new Tuple<string, string>(username, message));
            await Clients.Groups(group).SendAsync("GetMessageToGroup", username, message);
           
            //message blazorga GetMessage key orqali boryapti
            //send message ishga tushsa hamma clientlar(blazorning getmessage) funksiyasiga xabar yuboradi
        }

        
        public async Task JoinToGroup(string group)
        {
           await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
    }
}


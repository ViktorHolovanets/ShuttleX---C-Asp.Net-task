using Microsoft.AspNetCore.SignalR;
using ShuttleX_task_api.Models;
using System.Text.RegularExpressions;
using System;

namespace ShuttleX_task_api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDb _context;

        public ChatHub(AppDb context)
        {
            _context = context;
        }

        public async Task SendMessage(Guid chatId, Guid userId, string message)
        {
            var chatMessage = new Message { ChatId = chatId, CreatedByUserId = userId, Content = message };
            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userId, message);
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }

}

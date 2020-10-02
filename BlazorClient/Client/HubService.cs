
using BlazorClient.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Client
{
    public class HubService : IHubService
    {
        private HubConnection _hubConnection;
        public EventHandler<ChatModel> OnChatRecieved;

        public async Task StartHub()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44393/chat").Build();
            await _hubConnection.StartAsync();

            _hubConnection.On<ChatModel>("ChatRecieved", (chat) =>
            {
                OnChatRecieved.Invoke(this, chat);
            });
        }

        public async Task SendChat(ChatModel chat)
        {
            await _hubConnection.SendAsync("AddChat", chat);
        }
    }
}


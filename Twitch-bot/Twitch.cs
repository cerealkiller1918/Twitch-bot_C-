using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Models.API;

namespace Twitch_bot{
    class Twitch{

        TwitchClient client;
        ConnectionCredentials credentials;
        Bot bot;

        public Twitch() {

            credentials = new ConnectionCredentials(TwitchInfo.UserName, TwitchInfo.OAuth);
            client = new TwitchClient(credentials, TwitchInfo.Channel, logging: true);

            client.ChatThrottler = new TwitchLib.Services.MessageThrottler(20/2, TimeSpan.FromSeconds(30));
            client.WhisperThrottler = new TwitchLib.Services.MessageThrottler(20 / 2, TimeSpan.FromSeconds(30));

            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnConnected += Client_OnConnected;
            client.OnMessageReceived += Client_OnMessageReceived;
            //client.OnLog += Client_OnLog;

            client.Connect();
            bot = new Bot(client);

            

        }

        

        private void Client_OnLog(object sender, OnLogArgs e) {
            Console.WriteLine($"Log {e.DateTime}: {e.Data}");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e) {
            //TODO: This is where the bot is going to live at.

            Console.WriteLine($"{e.ChatMessage.DisplayName}: {e.ChatMessage.Message}");
            bot.Controler(e);
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e) {
            Console.WriteLine($"Connected to Twitch ");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e) {
            Console.WriteLine($"You have joined {e.Channel}");
        }

    }
}

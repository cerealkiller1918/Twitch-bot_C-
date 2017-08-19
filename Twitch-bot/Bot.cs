using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;
using TwitchLib.Models.API.v5.Channels;

namespace Twitch_bot {
    class Bot {

        private TwitchClient client;

        public Bot(TwitchClient client) {
            this.client = client;
            TwitchAPI.Settings.ClientId = TwitchInfo.ClientID;
        }

        public void Controler(OnMessageReceivedArgs e) {
            var message = e.ChatMessage.Message.ToLowerInvariant();
            var userName = e.ChatMessage.DisplayName;

            if (!message.StartsWith("!")) return;

            switch (message) {
                case "!mods":
                    //TODO add mods.
                    client.SendMessage("This is a list of the Mods are being used.");
                    break;
                case "!followers":
                    Followers();
                    break;
                case "!uptime":
                    client.SendMessage(GetUptime()?.ToString() ?? "Offline");
                    break;
            }
        }

        private TimeSpan? GetUptime() {
            string userid = GetUserId(TwitchInfo.UserName);
            if (userid == null) return null;
            return TwitchAPI.Streams.v5.GetUptimeAsync(userid).Result;
            
        }

        private string GetUserId(string UserName) {
            User[] userList = TwitchAPI.Users.v5.GetUserByNameAsync(UserName).Result.Matches;
            if (userList == null || userList.Length == 0) return null;
            return userList[0].Id;
        }

        private void Followers() {
            try {
                ChannelFollowers follows = TwitchAPI.Channels.v5.GetChannelFollowersAsync(TwitchInfo.Channel).Result;
                foreach (var follow in follows.Follows) {
                    Console.WriteLine(follow.User);
                }
            }catch(Exception e) {
                Console.WriteLine($"Stacktrace: /n{e.StackTrace}");
            }
        }

        
    }
}

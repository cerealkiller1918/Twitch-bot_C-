using System;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Models.API;
using TwitchLib.Models.API.v5.Channels;
using TwitchLib.Services;
using TwitchLib.Events.Services.FollowerService;


namespace Twitch_bot { 
    class Followers {

        TwitchClient client;

        public Followers(TwitchClient client) {
            this.client = client;

            FollowerService follow = new FollowerService(clientId: TwitchInfo.ClientID);

            follow.OnNewFollowersDetected += Follow_OnNewFollowersDetected;
            

        }

        private void Follow_OnNewFollowersDetected(object sender, OnNewFollowersDetectedArgs e) {
            try {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"New Follower {e.NewFollowers}");
            }catch(Exception e1) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"StackTrace: \n{ e1.StackTrace}");
            }
        }

        public async void  GetFollowers() {
            try {
               
            }catch(Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"StackTrace: \n{ e.StackTrace}");
            }
        }
    }
}

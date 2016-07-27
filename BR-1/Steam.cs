using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace BR_1
{
    class Steam
    {
        static string APIKey;
        public static void Initialize()
        {
            Console.WriteLine("Enter your Steam API key:");
            APIKey = Console.ReadLine();
        }
        public static OwnedGames GetOwnedGames(long Id)
        {
            WebClient Client = new WebClient();
            var DataBuffer = Client.DownloadData($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={APIKey}&steamid={Id}&format=json");
            Client.Dispose();
            string Data = Encoding.ASCII.GetString(DataBuffer);
            return JsonConvert.DeserializeObject<OwnedGames>(Data);
        }
    }
    class AppNameDict
    {
        public static Dictionary<int, string> Name { get; set; }

        public static void Initialize()
        {
            Name = new Dictionary<int, string>();

            Console.WriteLine("Acquiring app IDs and names from Steam...");

            WebClient Client = new WebClient();
            var DownloadedDataBuffer = Client.DownloadData(@"https://api.steampowered.com/ISteamApps/GetAppList/v2/?key=5FA8837A4A20DC14807175C668B37175&format=json");
            Client.Dispose();
            string DownloadedData = Encoding.ASCII.GetString(DownloadedDataBuffer);
            AppListResponse Response = JsonConvert.DeserializeObject<AppListResponse>(DownloadedData);

            Console.WriteLine("Initializing the app/name dictionary...");

            foreach (App a in Response.applist.apps) { Name.Add(a.appid, a.name); }

            Console.WriteLine("App/name dictionary initialized.");
        }
    }
    class OwnedGame
    {
        public string Name { get; set; }
        public int appid { get; set; }
        public int playtime_forever { get; set; }
        public int playtime_2weeks { get; set; }
    }
    class OwnedGamesResponse
    {
        public int game_count { get; set; }
        public OwnedGame[] games { get; set; }
    }
    class OwnedGames
    {
        public OwnedGamesResponse response { get; set; }
    }
    class App
    {
        public int appid { get; set; }
        public string name { get; set; }
    }
    class AppList
    {
        public App[] apps { get; set; }
    }
    class AppListResponse
    {
        public AppList applist { get; set; }
    }
}

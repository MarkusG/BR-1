using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BR_1
{
    static class EventListeners
    {
        public static void CreateEventListeners()
        {
            // Log every message that is sent
            Program._client.MessageReceived += (s, e) =>
            {
                if (e.Message.Text != null)
                {
                    string msg = $"[{DateTime.Now}] [{e.Server.Name} - {e.Channel.Name}] {e.User?.Name ?? "Unknown User"} : {e.Message.RawText}";

                    Console.WriteLine(msg);
                    using (StreamWriter LogFile = new StreamWriter("log.txt", true))
                    {
                        LogFile.WriteLine(msg);
                        LogFile.Close();
                    }
                }

            };

            Program._client.ServerAvailable += (s, e) => { Program._planetside = Program._client.GetServer(166320083000492032); };
        }
    }
}

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
            Program._client.ServerAvailable += (s, e) => { Program._planetside = Program._client.GetServer(166320083000492032); };
        }
    }
}

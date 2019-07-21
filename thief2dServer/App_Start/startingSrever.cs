using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models;

namespace thief2dServer.App_Start
{
    public class startingSrever
    {
        public static void StartAll()
        {
            new PlayerListManager().LoadPlayersAtStart();
        }
       
    }
}
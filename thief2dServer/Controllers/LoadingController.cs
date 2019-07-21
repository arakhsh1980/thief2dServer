using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using thief2dServer.Models.blocks;
using thief2dServer.Models.utilities;
using System.Web.Script.Serialization;
using thief2dServer.Models;

namespace thief2dServer.Controllers
{
    public class LoadingController : Controller
    {

        private static Mutex AddNew = new Mutex();
        // POST: Loading/LoadPlayerData
        [HttpPost]
        public string LoadPlayerData(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
            PlayerForDataBase PlayerData = dataBase.Buildings.Find(id);            
            if (PlayerData == null)
            {
                AddNew.WaitOne();
                PlayerData = new Utlities().returnDefultBulding();
                string ss = new Random().NextDouble().ToString();
                int index = dataBase.Buildings.Count<PlayerForDataBase>() + 1;
                PlayerData.ID = index.ToString() + ss;
                
                dataBase.Buildings.Add(PlayerData);
                dataBase.SaveChanges();
                AddNew.ReleaseMutex();
                new PlayerListManager().AddPlayerInfo(PlayerData);
            }
            else
            {                
                PlayerData.UpdatePropertyByTime();
                new PlayerListManager().UpdatePlayerInfo(PlayerData);
            }
            
            PlayerForSerialize buildingForSerialize= new Utlities().ConvertBuildingDataBaseToSerialize(PlayerData);


            LogSystem.AddPlayerLog(PlayerData.ID, "player" + PlayerData.ID.ToString() + " added by " + PlayerData.ID + " ID");
            string uu = new JavaScriptSerializer().Serialize(buildingForSerialize);
            return uu;
        }

    }
}

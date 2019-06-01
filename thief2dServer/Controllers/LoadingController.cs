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
            PlayerForDataBase buildingData = dataBase.Buildings.Find(id);            
            if (buildingData == null)
            {
                AddNew.WaitOne();
                buildingData = new Utlities().returnDefultBulding();
                string ss = new Random().NextDouble().ToString();
                int index = dataBase.Buildings.Count<PlayerForDataBase>() + 1;
                buildingData.ID = index.ToString() + ss;
                
                dataBase.Buildings.Add(buildingData);
                dataBase.SaveChanges();
                AddNew.ReleaseMutex();
            }
            else
            {
                buildingData.UpdatePropertyByTime();
            }
            new buildingListManager().AddOrUpdateBuilding(buildingData);
            PlayerForSerialize buildingForSerialize= new Utlities().ConvertBuildingDataBaseToSerialize(buildingData);


            LogSystem.AddPlayerLog(buildingData.ID, "player" + buildingData.ID.ToString() + " added by " + buildingData.ID + " ID");
            string uu = new JavaScriptSerializer().Serialize(buildingForSerialize);
            return uu;
        }

    }
}

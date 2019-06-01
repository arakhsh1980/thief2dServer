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
using System.Data.Entity;

namespace thief2dServer.Controllers
{
    public class HomeEditController : Controller
    {

        [HttpPost]
        public string UpdateBuilding(string buildst)
        {
            string id = Request.Form["PlayerId"];
            string BulidngString = Request.Form["BulidngString"];
            Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
            PlayerForDataBase buildingData = dataBase.Buildings.Find(id);
           

            if (buildingData != null)
            {
                buildingData.buildingCode = BulidngString;
                dataBase.Entry(buildingData).State = EntityState.Modified;
                dataBase.SaveChanges();
                return true.ToString();
            }
            else
            {
                return false.ToString();
            }
        }

        [HttpPost]
        public string AttackRequst(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            string enemyId = new buildingListManager().NextIdForAtack(id);
            if (enemyId != null)
            {
                Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
                PlayerForDataBase buildingData = dataBase.Buildings.Find(id);
                PlayerForSerialize buildingForSerialize = new Utlities().ConvertBuildingDataBaseToSerialize(buildingData);
                LogSystem.AddPlayerLog(id, "player" + id.ToString() + " attacked " + buildingData.ID + " ID");
                string uu = new JavaScriptSerializer().Serialize(buildingForSerialize);
                return uu;
            }
            else
            {
                return false.ToString();
            }
        }
    }
}
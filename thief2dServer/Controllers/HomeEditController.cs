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
            PlayerForDataBase PlayerData = dataBase.Buildings.Find(id);
           

            if (PlayerData != null)
            {
                PlayerData.buildingCode = BulidngString;
                dataBase.Entry(PlayerData).State = EntityState.Modified;
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
            string enemyId = new PlayerListManager().NextIdForAtack(id);
            if (enemyId != null)
            {
                Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
                PlayerForDataBase PlayerData = dataBase.Buildings.Find(enemyId);
                if(PlayerData != null)
                {
                    PlayerForSerialize playerDataForSerialize = new Utlities().ConvertBuildingDataBaseToSerialize(PlayerData);
                    LogSystem.AddPlayerLog(id, "player" + id.ToString() + " attacked " + PlayerData.ID + " ID");
                    string uu = new JavaScriptSerializer().Serialize(playerDataForSerialize);
                    return uu;
                }
                return false.ToString();
            }
            else
            {
                return false.ToString();
            }
        }
    }
}
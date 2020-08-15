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
        Theif2dDataDBContext dataBase = new Theif2dDataDBContext();

        [HttpPost]
        public string UpdateBuilding(string buildst)
        {
            string id = Request.Form["PlayerId"];
            string BulidngString = Request.Form["BulidngString"];            
            PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(id);
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

        public string UpdateProductCode(string buildst)
        {
            string id = Request.Form["PlayerId"];
            string productString = Request.Form["ProductString"];
            long PointTime = long.Parse(Request.Form["PointTime"]);
            PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(id);
            if (PlayerData != null)
            {
                PlayerData.ProductCode = productString;
                PlayerData.PointTime = PointTime;
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
        public string HomeBuyingAssetRequst(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            int idOfBuyingAsset =Int32.Parse( Request.Form["idOfBuyingAsset"]);
            string TypOfBuyingAsset = Request.Form["TypOfBuyingAsset"];
            buyableType obType = new Convertor().StringTobuyableType(TypOfBuyingAsset);
            string newStringOfScene = Request.Form["newStringOfScene"];
            PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(id);
            Asset buyingAsset = new AssetManager().RetrunAsset(obType, idOfBuyingAsset);
            
            if (PlayerData != null)
            {

                if (PlayerData.BuyThisAssetForPlayer(buyingAsset) ){
                    //PlayerData.
                    dataBase.Entry(PlayerData).State = EntityState.Modified;
                    dataBase.SaveChanges();
                    return true.ToString();
                }
                else
                {
                    return false.ToString();

                }
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
            PlayerForDataBase thisPlayerData = dataBase.PlayerinDataBase.Find(id);
            thisPlayerData.UpdatePropertyByTime();
            if(0 < thisPlayerData.remaningTimeToNextAttack)
            {
                return false.ToString();
            }

            string enemyId = new PlayerListManager().NextIdForAtack(id);
            if (enemyId != null)
            {
                //Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
                PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(enemyId);
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

        [HttpPost]
        public string StoryRequst(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            PlayerForDataBase thisPlayerData = dataBase.PlayerinDataBase.Find(id);
            StoryLevel storyString = dataBase.storylevelsDataBase.Find(thisPlayerData.StoryDoneLevel + 1);
            return storyString.levelString;
        }



    }
}
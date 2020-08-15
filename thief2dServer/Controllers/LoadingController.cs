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
    public class LoadingController : Controller
    {

        private static Mutex AddNew = new Mutex();
        private static Mutex LoadAssetmutex = new Mutex();
        Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
        // POST: Loading/LoadPlayerData

        

        [HttpPost]
        public string LoadPlayerData(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            if (!AssetManager.isAssetsLoadedFromDataBase)
            {
                LoadAssetsFromDataBase();
            }
            ShipForDataBase findedShip = dataBase.AllShips.Find(id);            
            if (findedShip == null)
            {
                AddNew.WaitOne();
                findedShip = new Utlities().returnDefultShip();
                //AllShips.buildingCode = dataBase.ShipBaseDataBase.Find(1).BaseString;
                string ss = new Random().NextDouble().ToString();
                int index = dataBase.PlayerinDataBase.Count<PlayerForDataBase>() + 1;
                findedShip.OwnerID = index.ToString() + ss;                
                dataBase.AllShips.Add(findedShip);
                dataBase.SaveChanges();
                AddNew.ReleaseMutex();
               // new PlayerListManager().AddPlayerInfo(PlayerData);
            }
            else
            {
                findedShip.UpdatePropertyByTime();
                dataBase.Entry(findedShip).State = EntityState.Modified;
                dataBase.SaveChanges();
               // new PlayerListManager().UpdatePlayerInfo(PlayerData);
            }
            
            //PlayerForSerialize buildingForSerialize= new Utlities().ConvertBuildingDataBaseToSerialize(PlayerData);


            LogSystem.AddPlayerLog(findedShip.OwnerID, "Ship" + findedShip.OwnerID.ToString() + " added ");
            ShipForSerialize fors = new ShipForSerialize();
            fors.SetAccordingTodataBAse(findedShip);
            string uu = new JavaScriptSerializer().Serialize(fors);
            return uu;
        }


        /*
        [HttpPost]
        public string LoadSpecificShipData(FormCollection collection)
        {
            string Shipname = Request.Form["Shipname"];
            string Password = Request.Form["Password"];
            if (!AssetManager.isAssetsLoadedFromDataBase)
            {
                LoadAssetsFromDataBase();
            }
            PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(id);
            if (PlayerData == null)
            {
                AddNew.WaitOne();
                PlayerData = new Utlities().returnDefultBulding();
                PlayerData.buildingCode = dataBase.ShipBaseDataBase.Find(1).BaseString;
                string ss = new Random().NextDouble().ToString();
                int index = dataBase.PlayerinDataBase.Count<PlayerForDataBase>() + 1;
                PlayerData.ID = index.ToString() + ss;

                dataBase.PlayerinDataBase.Add(PlayerData);
                dataBase.SaveChanges();
                AddNew.ReleaseMutex();
                new PlayerListManager().AddPlayerInfo(PlayerData);
            }
            else
            {
                PlayerData.UpdatePropertyByTime();
                dataBase.Entry(PlayerData).State = EntityState.Modified;
                dataBase.SaveChanges();
                new PlayerListManager().UpdatePlayerInfo(PlayerData);
            }

            PlayerForSerialize buildingForSerialize = new Utlities().ConvertBuildingDataBaseToSerialize(PlayerData);


            LogSystem.AddPlayerLog(PlayerData.ID, "player" + PlayerData.ID.ToString() + " added by " + PlayerData.ID + " ID");
            string uu = new JavaScriptSerializer().Serialize(buildingForSerialize);
            return uu;
        }
        */



        private void LoadAssetsFromDataBase()
        {
            LoadAssetmutex.WaitOne();
            if (AssetManager.isAssetsLoadedFromDataBase) { return; }
            AssetForDataBase[] loadingAssets = dataBase.AssetinDataBase.ToArray();
            for (int i = 0; i < loadingAssets.Length; i++)
            {
                Asset newAsset = new Asset();
                newAsset.LoadFrom(loadingAssets[i]);
                new AssetManager().AddAsset(newAsset);
            }
            AssetManager.isAssetsLoadedFromDataBase = true;
            LoadAssetmutex.ReleaseMutex();
        }
    }
}

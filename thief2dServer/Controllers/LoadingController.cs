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
        public string LoadPlayerData1(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            string Shipname = Request.Form["Shipname"];
            string Password = Request.Form["Password"];
            string PasswordForCreation = "11111";
            string PasswordForEdit = "11111";


            new Theif2dDataDBContext().LoadForFisttimeIfNessecary();

            AddNew.WaitOne();

            if (Shipname == "PlayerShip") {
                DualString findedShip = dataBase.AllDualStrings.Find(id);
                if (findedShip == null)
                {
                    
                    findedShip = new DualString();
                    string ss = new Random().NextDouble().ToString();
                    int index = dataBase.PlayerinDataBase.Count<PlayerForDataBase>() + 1;
                    findedShip.key= index.ToString() + ss;
                    ShipForSerialize forsss = new ShipForSerialize();
                    //forsss.ProducersInShip = new long[1];
                    //forsss.ProducersInShip[0] = 11;
                    //forsss.Items.Add(223);
                    forsss.OwnerID = findedShip.key;
                    findedShip.value =  new JavaScriptSerializer().Serialize(forsss);
                    dataBase.AllDualStrings.Add(findedShip);
                    dataBase.SaveChanges();
                }
                AddNew.ReleaseMutex();
                return findedShip.value;
                
            }
            else
            {
                string code = Shipname + Password;
                DualString findedShip = dataBase.AllDualStrings.Find(id);
                if (findedShip == null)
                {
                    if (Password == PasswordForCreation)
                    {
                        findedShip = new DualString();
                        string ss = new Random().NextDouble().ToString();
                        int index = dataBase.PlayerinDataBase.Count<PlayerForDataBase>() + 1;
                        findedShip.key = code;
                        ShipForSerialize forsss = new ShipForSerialize();
                        forsss.OwnerID = findedShip.key;
                        findedShip.value = new JavaScriptSerializer().Serialize(forsss);
                        dataBase.AllDualStrings.Add(findedShip);
                        dataBase.SaveChanges();
                    }
                    else
                    {
                        AddNew.ReleaseMutex();
                        return "shipNotFinded  & CreationPaswordIsWrong";
                    }
                }
                AddNew.ReleaseMutex();
                return findedShip.value;                
            }
            
        }



        [HttpPost]
        public string LoadPlayerData2(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            string Shipname = Request.Form["Shipname"];
            string Password = Request.Form["Password"];
            string PasswordForCreation = "11111";
            string PasswordForEdit = "11111";
            new Theif2dDataDBContext().LoadForFisttimeIfNessecary();
            if (Shipname == "PlayerShip")
            {
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
                LogSystem.AddPlayerLog(findedShip.OwnerID, "Ship" + findedShip.OwnerID.ToString() + " added ");
                ShipForSerialize fors = new ShipForSerialize();
                fors.SetAccordingTodataBAse(findedShip);
                string uu = new JavaScriptSerializer().Serialize(fors);
                return uu;
            }
            else
            {
                string code = Shipname + Password;
                ShipForDataBase findedShip = dataBase.AllShips.Find(code);
                if (findedShip == null)
                {
                    if (Password == PasswordForCreation)
                    {
                        return "UnderConstuction";
                    }
                    else
                    {
                        return "shipNotFinded  & CreationPaswordIsWrong";
                    }
                }
                else
                {
                    return "UnderConstuction";
                }
            }

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



    }
}

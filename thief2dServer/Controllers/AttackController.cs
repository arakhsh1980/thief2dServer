using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thief2dServer.Models;

using System.Threading;
using thief2dServer.Models.blocks;
using thief2dServer.Models.utilities;
using System.Web.Script.Serialization;
using System.Data.Entity;


namespace thief2dServer.Controllers
{
    public class AttackController : Controller
    {
        private static Mutex AddNew = new Mutex();
        Theif2dDataDBContext dataBase = new Theif2dDataDBContext();

        [HttpPost]
        public string AttackResult(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            string opponentid = Request.Form["opponenId"];
            int gatheredCoin = Int32.Parse( Request.Form["gatheredCoin"]);
            int gatheredElixir = Int32.Parse(Request.Form["gatheredElixir"]);

            
            PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(id);
            PlayerForDataBase opponentData = dataBase.PlayerinDataBase.Find(opponentid);
            if (PlayerData != null && opponentData !=null)
            {
                PlayerData.UpdatePropertyByTime();
                opponentData.UpdatePropertyByTime();
                AddNew.WaitOne();
                opponentData.coin -= gatheredCoin;
                opponentData.elixir -= gatheredElixir;
                PlayerData.coin += gatheredCoin;
                PlayerData.elixir += gatheredElixir;
                dataBase.Entry(PlayerData).State = EntityState.Modified;
                dataBase.Entry(opponentData).State = EntityState.Modified;
                dataBase.SaveChanges();
                AddNew.ReleaseMutex();
                new PlayerListManager().UpdatePlayerInfo(PlayerData);
                new PlayerListManager().UpdatePlayerInfo(opponentData);
                return true.ToString();
            }
            else
            {
                return false.ToString();
            }
        }


        [HttpPost]
        public string AttackStarted(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            string opponentid = Request.Form["opponenId"];         

            
            PlayerForDataBase PlayerData = dataBase.PlayerinDataBase.Find(id);
            PlayerForDataBase opponentData = dataBase.PlayerinDataBase.Find(opponentid);
            if (PlayerData != null && opponentData != null)
            {
                AddNew.WaitOne();
                PlayerData.remaningTimeToNextAttack = Constants.TimeToNextAttackDefult;
                opponentData.remaningShialdInSecond = Constants.shildDefultTime;
                dataBase.Entry(PlayerData).State = EntityState.Modified;
                dataBase.Entry(opponentData).State = EntityState.Modified;
                dataBase.SaveChanges();
                AddNew.ReleaseMutex();
                new PlayerListManager().UpdatePlayerInfo(PlayerData);
                new PlayerListManager().UpdatePlayerInfo(opponentData);
                return true.ToString();
            }
            else
            {
                return false.ToString();
            }
        }

    }
}
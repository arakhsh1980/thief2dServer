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
        [HttpPost]
        public string AttackResult(FormCollection collection)
        {
            string id = Request.Form["PlayerId"];
            string opponentid = Request.Form["opponenId"];
            int gatheredCoin = Int32.Parse( Request.Form["gatheredCoin"]);
            int gatheredElixir = Int32.Parse(Request.Form["gatheredElixir"]);

            Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
            PlayerForDataBase PlayerData = dataBase.Buildings.Find(id);
            PlayerForDataBase opponentData = dataBase.Buildings.Find(opponentid);
            if (PlayerData != null && opponentData !=null)
            {
                AddNew.WaitOne();
                opponentData.coin -= gatheredCoin;
                opponentData.elixir -= gatheredElixir;
                opponentData.remaningShialdInSecond =Constants.shildDefultTime;
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

    }
}
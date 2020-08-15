using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.blocks;
using thief2dServer.Models.utilities;

namespace thief2dServer.Models
{
    class playerData
    {
        public playerData()
        {

            coin = 100;
            elixir = 100;
            addingCoinRate = 1;
            addingElixirRate = 1;
            lastTimeVariableUpdate = DateTime.UtcNow.ToBinary();
            remaningShialdInSecond = 0;
            
        }


        public string ID { get; set; }
        public int coin { get; set; }
        public int elixir { get; set; }
        public float addingCoinRate { get; set; }
        public float addingElixirRate { get; set; }
        public long lastTimeVariableUpdate { get; set; }
        public int remaningShialdInSecond { get; set; }

        public void UpdatePropertyByTime()
        {
            double ff = DateTime.Now.Subtract(DateTime.FromBinary(lastTimeVariableUpdate)).TotalSeconds;
            coin += (int)Math.Floor(addingCoinRate * ff);
            elixir += (int)Math.Floor(addingElixirRate * ff);
            if (0 < remaningShialdInSecond)
            {
                remaningShialdInSecond -= (int)Math.Floor(ff);
                if (remaningShialdInSecond < 0) { remaningShialdInSecond = 0; }
            }
            lastTimeVariableUpdate = DateTime.UtcNow.ToBinary();
        }

    }
    public class PlayerListManager
    {
        private static List<playerData> playersList = new List<playerData>();

        public void ClearPlayerList()
        {
            playersList.Clear();
        }

        public void UpdatePlayerInfo(PlayerForDataBase newB)
        {            
            bool isFind = false;
            foreach (playerData bForD in playersList) if (bForD.ID == newB.ID && !isFind)
                {
                    bForD.addingCoinRate = newB.addingCoinRate;
                    bForD.addingElixirRate = newB.addingElixirRate;
                    bForD.coin = newB.coin;
                    bForD.elixir = newB.elixir;
                    bForD.lastTimeVariableUpdate = newB.lastTimeVariableUpdate;
                    bForD.remaningShialdInSecond = newB.remaningShialdInSecond;
                    isFind = true;
                }
            if (!isFind)
            {
                ErrorSystem.AddBigError("playerListManager.UpdatePlayerInfo. player not find");
            }
        }


        public void AddPlayerInfo(PlayerForDataBase newB)
        {
            bool isFind = false;
            foreach (playerData bForD in playersList) if (bForD.ID == newB.ID && !isFind) { isFind = true; }
            if (isFind)
            {
                ErrorSystem.AddBigError("playerListManager.AddPlayerInfo. player is in the data base");
            }
            else
            {
                playerData newbuilding = ConvertBuildingDataBaseVersionToBuilding(newB);
                playersList.Add(newbuilding);
            }
               
        }




        public string NextIdForAtack(string attaker)
        {           
            string nextforA = null;
            int maxCoin = 0;
            foreach (playerData bb in playersList) 
            {
                bb.UpdatePropertyByTime();
                if (maxCoin < bb.coin && bb.remaningShialdInSecond == 0 && bb.ID != attaker)
                {
                    nextforA = bb.ID;
                    maxCoin = bb.coin;
                }
            }
            return nextforA;
        }



        playerData ConvertBuildingDataBaseVersionToBuilding(PlayerForDataBase newB)
        {
            playerData bForD = new playerData();
            bForD.addingCoinRate = newB.addingCoinRate;
            bForD.addingElixirRate = newB.addingElixirRate;
            bForD.coin = newB.coin;
            bForD.elixir = newB.elixir;
            bForD.ID = newB.ID;
            bForD.lastTimeVariableUpdate = newB.lastTimeVariableUpdate;
            return bForD;
        }



        public void LoadPlayersAtStart()
        {           
            Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
            PlayerForDataBase[] allBuildings = dataBase.PlayerinDataBase.ToArray();
            for (int i = 0; i < allBuildings.Length; i++)
            {
                AddPlayerInfo(allBuildings[i]);
            }            
        }

    }
}
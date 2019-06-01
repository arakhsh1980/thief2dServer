using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.blocks;

namespace thief2dServer.Models
{
    class Building
    {
        public Building()
        {

            coin = 100;
            elixir = 100;
            addingCoinRate = 1;
            addingElixirRate = 1;
            lastTimeVariableUpdate = DateTime.Now;
            remaningShialdInSecond = 0;
        }


        public string ID { get; set; }
        public int coin { get; set; }
        public int elixir { get; set; }
        public float addingCoinRate { get; set; }
        public float addingElixirRate { get; set; }
        public DateTime lastTimeVariableUpdate { get; set; }
        public int remaningShialdInSecond { get; set; }

        public void UpdatePropertyByTime()
        {
            double ff = DateTime.Now.Subtract(lastTimeVariableUpdate).TotalSeconds;
            coin += (int)Math.Floor(addingCoinRate * ff);
            elixir += (int)Math.Floor(addingElixirRate * ff);
            if (0 < remaningShialdInSecond)
            {
                remaningShialdInSecond -= (int)Math.Floor(ff);
                if (remaningShialdInSecond < 0) { remaningShialdInSecond = 0; }
            }
            lastTimeVariableUpdate = DateTime.Now;
        }

    }
    public class buildingListManager
    {
        private static List<Building> buildingList = new List<Building>();
                

        public void AddOrUpdateBuilding(PlayerForDataBase newB)
        {            
            bool isFind = false;
            foreach (Building bForD in buildingList) if (bForD.ID == newB.ID && !isFind)
                {
                    bForD.addingCoinRate = newB.addingCoinRate;
                    bForD.addingElixirRate = newB.addingElixirRate;
                    bForD.coin = newB.coin;
                    bForD.elixir = newB.elixir;
                    bForD.lastTimeVariableUpdate = newB.lastTimeVariableUpdate;
                    isFind = true;
                }
            if (!isFind)
            {

                Building newbuilding = ConvertBuildingDataBaseVersionToBuilding(newB);
                buildingList.Add(newbuilding);
            }
        }

       



        public string NextIdForAtack(string attaker)
        {           
            string nextforA = null;
            int maxCoin = 0;
            foreach (Building bb in buildingList) 
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



        Building ConvertBuildingDataBaseVersionToBuilding(PlayerForDataBase newB)
        {
            Building bForD = new Building();
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
            PlayerForDataBase[] allBuildings = dataBase.Buildings.ToArray();
            for (int i = 0; i < allBuildings.Length; i++)
            {
                AddOrUpdateBuilding(allBuildings[i]);
            }            
        }

    }
}
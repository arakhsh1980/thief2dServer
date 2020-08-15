using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.blocks;

namespace thief2dServer.Models.utilities
{
    public class Utlities
    {
        public PlayerForDataBase returnDefultBulding()
        {
            PlayerForDataBase bb = new PlayerForDataBase();
            return bb;
        }

        public ShipForDataBase returnDefultShip()
        {
            ShipForDataBase newShip = new ShipForDataBase();
            newShip.CoinInTheShip = 100;            

            return newShip;
        }


        /*
        public BuildingForDataBase   ConvertBuildingToDataBaseVersion(Building newB)
        {
            BuildingForDataBase bForD = new BuildingForDataBase();
            bForD.addingCoinRate = newB.addingCoinRate;
            bForD.addingElixirRate = newB.addingElixirRate;
            bForD.buildingCode =  Constants.DefultBuildingString;
            bForD.coin = newB.coin;
            bForD.elixir = newB.elixir;
            bForD.ID = newB.ID;
            bForD.lastTimeVariableUpdate = newB.lastTimeVariableUpdate;

            bForD.remaningShialdInSecond = newB.remaningShialdInSecond;
            return bForD;

        }
        */

        public PlayerForSerialize  ConvertBuildingDataBaseToSerialize(PlayerForDataBase newB)
        {
            PlayerForSerialize bForD = new PlayerForSerialize();
            bForD.addingCoinRate = newB.addingCoinRate;
            //bForD.PointTime = newB.PointTime;
            //bForD.ProductCode = newB.ProductCode;
            bForD.addingElixirRate = newB.addingElixirRate;
            bForD.buildingCode = newB.buildingCode;
            bForD.coin = newB.coin;
            bForD.elixir = newB.elixir;
            bForD.ID = newB.ID;
            bForD.remaningShialdInSecond = newB.remaningShialdInSecond;
            bForD.remaningTimeToNextAttack = newB.remaningTimeToNextAttack;
            return bForD;
        }


    }
}
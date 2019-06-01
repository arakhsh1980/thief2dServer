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
            bForD.addingElixirRate = newB.addingElixirRate;
            bForD.buildingCode = newB.buildingCode;
            bForD.coin = newB.coin;
            bForD.elixir = newB.elixir;
            bForD.ID = newB.ID;
            return bForD;
        }


    }
}
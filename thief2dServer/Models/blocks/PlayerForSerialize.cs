using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace thief2dServer.Models.blocks
{
    [Serializable]
    public struct PlayerForSerialize
    {

        public string ID;
        public string buildingCode;
        public int coin;
        public int elixir;
        public float addingCoinRate;
        public float addingElixirRate;
        public int remaningShialdInSecond;
    }
}
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
        //public string ProductCode;
       // public long PointTime;
        public int coin;
        public int elixir;
        public string ItemIds;
        public float addingCoinRate;
        public float addingElixirRate;
        public int remaningShialdInSecond;
        public int remaningTimeToNextAttack ;
        public int ThePlayersDwarfs;
        public int ThePlayersLadder;
        public int Grenades;
        public int StickyGrenades;
        public int bouncyBall;
        public int stickybomb;
        public int shipWorker;
        public long TimePointOfLastUpdate;

        
    }
}
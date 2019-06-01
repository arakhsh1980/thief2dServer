using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.utilities;
using System.ComponentModel.DataAnnotations;

namespace thief2dServer.Models.blocks
{
    public class PlayerForDataBase
    {
        public PlayerForDataBase()
        {
            buildingCode = Constants.DefultBuildingString;
            coin = 100;
            elixir = 100;
            addingCoinRate = 1;
            addingElixirRate = 1;
            lastTimeVariableUpdate = DateTime.Now;
            remaningShialdInSecond = 0;
        }

        [Key]
        public string ID { get; set; }
        public string buildingCode { get; set; }
        public int coin { get; set; }
        public int elixir { get; set; }
        public float addingCoinRate { get; set; }
        public float addingElixirRate { get; set; }
        public DateTime lastTimeVariableUpdate { get; set; }
        public int remaningShialdInSecond { get; set; }

        public void UpdatePropertyByTime()
        {
            double ff =DateTime.Now.Subtract(lastTimeVariableUpdate).TotalSeconds;
            coin += (int)Math.Floor(addingCoinRate * ff);
            elixir += (int)Math.Floor(addingElixirRate * ff);
            if (0 < remaningShialdInSecond) {
                remaningShialdInSecond -= (int)Math.Floor(ff);
                if (remaningShialdInSecond < 0) { remaningShialdInSecond = 0; }
            }
            lastTimeVariableUpdate = DateTime.Now;
        }

    }
}
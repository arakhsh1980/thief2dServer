using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.utilities;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace thief2dServer.Models.blocks
{
    public class PlayerForDataBase
    {
        public PlayerForDataBase()
        {
          // buildingCode = Constants.DefultBuildingString;
            coin = 150;
            elixir = 100;
            addingCoinRate = 0.003f;
            addingElixirRate = 0.003f;
            lastTimeVariableUpdate = DateTime.UtcNow.ToBinary(); 
            remaningShialdInSecond = 0;
            remaningTimeToNextAttack = 0;
            StoryDoneLevel = 0;
        }

        [Key]
        public string ID { get; set; }
        public string buildingCode { get; set; }
        public string ProductCode { get; set; }
        public long PointTime { get; set; }
        public int coin { get; set; }
        public int level { get; set; }
        public int elixir { get; set; }
        public float addingCoinRate { get; set; }
        public float addingElixirRate { get; set; }
        public long lastTimeVariableUpdate { get; set; }
        public int remaningShialdInSecond { get; set; }
        public int remaningTimeToNextAttack { get; set; }

        public int StoryDoneLevel { get; set; }

        public void UpdatePropertyByTime()
        {
            double ff =DateTime.UtcNow.Subtract(DateTime.FromBinary(lastTimeVariableUpdate)).TotalSeconds;
            coin += (int)Math.Floor(addingCoinRate * ff);
            elixir += (int)Math.Floor(addingElixirRate * ff);
            if (0 < remaningShialdInSecond) {
                remaningShialdInSecond -= (int)Math.Floor(ff);
                if (remaningShialdInSecond < 0) { remaningShialdInSecond = 0; }
            }
            if (0 < remaningTimeToNextAttack)
            {
                remaningTimeToNextAttack -= (int)Math.Floor(ff);
                if (remaningTimeToNextAttack < 0) { remaningTimeToNextAttack = 0; }
            }
            lastTimeVariableUpdate = DateTime.UtcNow.ToBinary();
        }

        public void SubtractProperty(Property price)
        {
            coin -= price.coin;            
        }

        public bool HaveEnoghProperty(Property price)
        {
            if (price.coin < coin)
            {                
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool BuyThisAssetForPlayer(Asset buyingAsset)
        {
           // BuildingDataForSerializable buidling = new JavaScriptSerializer().Deserialize<BuildingDataForSerializable>(buildingCode);
           // int PlayerNumberOfAsset = buidling.ReturnNumberOfType(buyingAsset.typeOfbuyAbleObj, buyingAsset.Id);
            //if( buyingAsset.MaxNumberInEachLevel[level]<= PlayerNumberOfAsset)
            //{
            //    return false;
            //}
            if(buyingAsset.price.coin > coin)
            {
                return false;
            }
            coin -= buyingAsset.price.coin;
            return true;            
        }

    }
}
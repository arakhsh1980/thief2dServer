using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace thief2dServer.Models.blocks
{
    [Serializable]
    public class ShipForDataBase
    {
        [Key]
        public string OwnerID { get; set; }
        public string Items { get; set; }
        public string objectsInShip { get; set; }
        public string ProducersInShip { get; set; }
        public string TimeProducersInShip { get; set; }
        public string ProducersNextProductTimePoint { get; set; }
        public string AttactingToolsInShip { get; set; }
        public long timePointOfNextrequiredUpdate { get; set; }
        public long timePointOfNextRaidReturn { get; set; }
        public int raidSycleDurationInMuinte { get; set; }
        public int raidAvarageIncome { get; set; }
        public long PreshopCode { get; set; }
        public bool isPreshopProducer { get; set; }
        public long ObjectInShopPlace { get; set; }
        public long TimePointOfLastUpdate { get; set; }
        public float addingCoinRate { get; set; }
        public float addingElixirRate { get; set; }
        public int remaningShialdInSecond { get; set; }
        public int remaningTimeToNextAttack { get; set; }
        public int backGroundId { get; set; }
        public float PositionOfFirstSnap_x { get; set; }
        public float PositionOfFirstSnap_y { get; set; }
        public string stringOfPreorderInfo { get; set; }
        public int CoinInTheShip { get; set; }

        public int StoryDoneLevel { get; set; }

        public void UpdatePropertyByTime()
        {
            
        }

        public void SubtractProperty(int price)
        {
            CoinInTheShip -= price;
        }

        public bool HaveEnoghProperty(Property price)
        {
            if (price.coin < CoinInTheShip)
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
            if (buyingAsset.price.coin > CoinInTheShip)
            {
                return false;
            }
            CoinInTheShip -= buyingAsset.price.coin;
            return true;
        }

        public void UpdateShip()
        {
            DateTime lastTimeUpdate = DateTime.FromBinary(TimePointOfLastUpdate);
            long TimeSpendedInSec = (long)(lastTimeUpdate - DateTime.UtcNow).TotalSeconds;
            TimePointOfLastUpdate = DateTime.UtcNow.ToBinary();



        }
    }

    [Serializable]
    public class ShipForSerialize
    {
        public string OwnerID;
        public long[] objectsInShip;
        public long[] ProducersInShip;
        public long[] TimeProducersInShip;
        public long[] ProducersNextProductTimePoint;
        public long[] AttactingToolsInShip;
        public long[] Items;
        public long timePointOfNextrequiredUpdate;
        public long timePointOfNextRaidReturn;
        public int raidSycleDurationInMuinte;
        public int raidAvarageIncome;
        public long PreshopCode;
        public bool isPreshopProducer;
        public long ObjectInShopPlace;
        public long TimePointOfLastUpdate;
        public float addingCoinRate;
        public float addingElixirRate;
        public int remaningShialdInSecond;
        public int remaningTimeToNextAttack;
        public int backGroundId;
        public float PositionOfFirstSnap_x;
        public float PositionOfFirstSnap_y;
        public string stringOfPreorderInfo;
        public int CoinInTheShip;


        public void UpdateShip()
        {
            DateTime lastTimeUpdate = DateTime.FromBinary(TimePointOfLastUpdate);
            long TimeSpendedInSec = (long)(lastTimeUpdate - DateTime.UtcNow).TotalSeconds;
            TimePointOfLastUpdate = DateTime.UtcNow.ToBinary();



        }
        public void SetAccordingTodataBAse(ShipForDataBase sorce)
        {
            OwnerID = sorce.OwnerID;
            CoinInTheShip = sorce.CoinInTheShip;
        }


    }

}
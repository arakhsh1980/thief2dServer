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

        public void SetAccordingTothis(ShipForSerialize thisship)
        {
            

        }

        public ShipForSerialize ReturnSerializeVersion()
        {
            ShipForSerialize newship = new ShipForSerialize();


            return newship;
        }
    }


    [Serializable]
    public class ShipForSerialize
    {
        public ShipForSerialize()
        {
            objectsInShip = new long[0];
            ProducersInShip = new long[0];
            ProducersNextProductTimePoint = new long[0];
            AttactingToolsInShip = new long[0];
            Items = new List<long>();
            ItemsFromAttac = new long[0];
        }
        public string OwnerID;
        public long[] objectsInShip;
        public long[] ProducersInShip;
        public long[] ProducersNextProductTimePoint;
        public long[] AttactingToolsInShip;//for grnade & dwrfs & smallDwrfs and other thing that you bring to attack scineOf home
        public List<long> Items;
        public long timePointOfNextrequiredUpdate;
        public long timePointOfNextRaidReturn;
        public int raidSycleDurationInMuinte;
        public int raidAvarageIncome;
        public int PreshopType;
        public int PreshopLevel;
        public bool isShopDone;
        public long TimePointOfLastUpdate;
        public int remaningShialdInSecond;
        public int remaningTimeToNextAttack;
        public int baseShipID;
        public int CoinInTheShip;
        public int CoinFromAttack;
        public long[] ItemsFromAttac;

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
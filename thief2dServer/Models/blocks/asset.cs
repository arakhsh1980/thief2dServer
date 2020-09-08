using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.utilities;
using System.ComponentModel.DataAnnotations;

[Serializable]
public struct Property
{
    // public int level;
    public int coin;
}
public enum ShipProductionType { fastDwarf, heavyDwarf, grnade }
public enum ShipObjectType { normalCeiling, strongCeiling, fastDwarfFactory, heavyDwarfFactory, grnadeFactory, bouncingWeapon, needleWeapon, horizontalWeapon, treasurBox, fastDwarfCage, HeavyDwarfCage, shipWorker }


public enum ShipItemType { ganjItem, greenganjItem, blueganjItem, blackganjItem }


public enum buyableType { weapon, ceiling, treasure, door, shipWorker, sleepPlace, eatingPlace, factory, none }
namespace thief2dServer.Models.blocks
{
    
    public class Asset
    {
        public int keyInDadabase;
        public buyableType typeOfbuyAbleObj;
        public string name;
        public int Id;
        public Property price;
        public short[] MaxNumberInEachLevel;
        public void LoadFrom(AssetForDataBase data)
        {
            typeOfbuyAbleObj = new Convertor().StringTobuyableType(data.typeOfbuyAbleObj);
            name = data.name;
            Id = data.Id;
            price = new Convertor().LongToProperty(data.price);
            MaxNumberInEachLevel  = new Convertor().StringToShortArray(data.MaxNumberInEachLevel);
        }
        public AssetForDataBase ReturnDataBaseFormat()
        {
            AssetForDataBase dataBaseFormat = new AssetForDataBase();
            dataBaseFormat.key = keyInDadabase;
            dataBaseFormat.typeOfbuyAbleObj = typeOfbuyAbleObj.ToString();
            dataBaseFormat.Id = Id;
            dataBaseFormat.MaxNumberInEachLevel = new Convertor().ShortArrayToSrting(MaxNumberInEachLevel);
            dataBaseFormat.name = name;
            dataBaseFormat.price = new Convertor().PropertyToLong(price);
            return dataBaseFormat;
        } 

    }


    public class DualString
    {
        [Key]
        public string key { get; set; }

        //public long TimeOfLastUpdate { get; set; }
        public string value { get; set; }
    }



    [Serializable]



    public class AssetForDataBase
    {
        [Key]
        public int key { get; set; }    
        public string typeOfbuyAbleObj { get; set; }
        public string name { get; set; }
        public int Id { get; set; }
        public long price { get; set; }
        public string MaxNumberInEachLevel { get; set; }
    }

    [Serializable]
    public class ShipObjectInfo
    {
        public string NameCode;
        public ShipObjectType shipObjType;
        public bool isAproducer;
        public int buyingPrice;
        public int[] upgradeRate;
        public int[] numberOfObjThatCanHaveInEachLevel;
        public int[] upgradePrice;
        public int[] shipLevelAccteptableUpgrade;
        public string upgradeRate_s;
        public string numberOfObjThatCanHaveInEachLevel_s;
        public string upgradePrice_s;
        public string shipLevelAccteptableUpgrade_s;
        public ShipProductionType ProcudtionType;
        public int ProcudtionLevel;
        public int minuteToProduce;

        public void FillarrayFromString()
        {
            upgradeRate = Convertor.StringToIntArrayWithChar(upgradeRate_s, '^');
            numberOfObjThatCanHaveInEachLevel = Convertor.StringToIntArrayWithChar(numberOfObjThatCanHaveInEachLevel_s, '^');
            upgradePrice = Convertor.StringToIntArrayWithChar(upgradePrice_s, '^');
            shipLevelAccteptableUpgrade = Convertor.StringToIntArrayWithChar(shipLevelAccteptableUpgrade_s, '^');
        }

        public void FillStringFromarray()
        {
            upgradeRate_s = Convertor.IntArrayToSrtingWithChar(upgradeRate, '^');
            numberOfObjThatCanHaveInEachLevel_s = Convertor.IntArrayToSrtingWithChar(numberOfObjThatCanHaveInEachLevel, '^');
            upgradePrice_s = Convertor.IntArrayToSrtingWithChar(upgradePrice, '^');
            shipLevelAccteptableUpgrade_s = Convertor.IntArrayToSrtingWithChar(shipLevelAccteptableUpgrade, '^');
        }

    }


    [Serializable]
    public class ShipInfo
    {
        public string NameCode;
        public int PositionOfFirstSnap_x;
        public int PositionOfFirstSnap_y;
        public int level;
        public int numberOfXInArow;
        public int numberOfYInColumn;
        public float storeRoomPosition_X;
        public float storeRoomPosition_Y;
        public string nameOfbackGround;
        public int idOfShip;
    }


    public enum ClassDataType { ShipInfo, ShipObjectData }

    public class ClassData
    {
        [Key]
        public string nameCode { get; set; }
        public int type { get; set; }
        public long TimeOfLastUpdate { get; set; }
        public string innerData { get; set; }

        public string ClassCodeToString()
        {
            string sum = type.ToString() + "+" + nameCode + "+" + TimeOfLastUpdate.ToString() + "+" + innerData;
            return sum;
        }
    }

}
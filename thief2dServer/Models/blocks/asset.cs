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

}
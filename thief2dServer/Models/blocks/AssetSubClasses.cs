using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thief2dServer.Models.blocks
{
    /*
    [Serializable]
    
    public class BuildingDataForSerializable
    {

        public List<ceilingClassForSerilize> allKindsOfCeilings = new List<ceilingClassForSerilize>();
        public List<TreasureboxForSerialize> Treasure = new List<TreasureboxForSerialize>();
        public List<DoorClassForSerilize> Door = new List<DoorClassForSerilize>();
        public List<weaponClassForSerialze> weapen = new List<weaponClassForSerialze>();
        public List<shipWorkerClassForSerilize> workers = new List<shipWorkerClassForSerilize>();

        public List<factoryClassForSerilize> factorysMachine = new List<factoryClassForSerilize>();

        public int ReturnNumberOfType(buyableType t , int id)
        {
            switch (t)
            {
                case buyableType.weapon:
                    return weapen.FindAll(x => x.idNum == id).Count;
                    break;
                case buyableType.ceiling:
                    return allKindsOfCeilings.FindAll(x => x.idNum == id).Count;
                    break;
                case buyableType.treasure:
                    return Treasure.FindAll(x => x.idNum == id).Count;
                    break;
                case buyableType.door:
                    return Door.FindAll(x => x.idNum == id).Count;
                    break;
                case buyableType.shipWorker:
                    break;
                case buyableType.none:
                    break;
                default:
                    break;
            }
            return 0;
        }
       

    }
    */
    [Serializable]
    public class weaponClassForSerialze
    {
        public float aimX;
        public float aimY;
        public float pointX;
        public float pointY;
        public int idNum;
        public bool downWard;
        public int numberOfBulet;
    }
    [Serializable]
    public class shipWorkerClassForSerilize
    {
        public float pointX;
        public float pointY;
        public float aimX;
        public float aimY;
        public int idNum;
        public bool isBusy;
        public bool haveTarget;
    }

    [Serializable]
    public class factoryClassForSerilize
    {
        public float pointX;
        public float pointY;
        public int idNum;
        public float numberOfProductsManufactured;
        public int factoryNumber;
        public work WorkOfThisFactory;
    }

    public struct work
    {
        public float timeToBuilldObject;
        public int countOfObjectBuildInTime;
    }
    [Serializable]
    public class TreasureboxForSerialize
    {
        public float pointX;
        public float pointY;
        public int elixir;
        public int coin;
        public int idNum;
        public int item;
        public int TboxNumber;
    }

    [Serializable]
    public class DoorClassForSerilize
    {
        public float pointX;
        public float pointY;
        public int idNum;
    }

    [Serializable]
    public class ceilingClassForSerilize
    {
        public float pointX;
        public float pointY;
        public int idNum;
    }


    public class AssetSubClasses
    {

    }
}
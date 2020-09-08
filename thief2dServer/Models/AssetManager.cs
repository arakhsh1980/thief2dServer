using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.blocks;
using thief2dServer.Models.utilities;
using System.Threading;
using System.Web.Script.Serialization;

namespace thief2dServer.Models
{
    public class AssetManager
    {
        const int arraylengh = 100;
        private static List<Asset> AssetsList = new List<Asset>();
        private static bool isDataBaseLoaded = false;
        //private static int ClassDatalistCounter = 0;
        private static List<ClassData> ClassDataList = new List<ClassData>();
        private static List<ClassData> ShipObjectList = new List<ClassData>();
        private static List<ClassData> ShipProductionList = new List<ClassData>();
        private static List<ClassData> ShipItemList = new List<ClassData>();
        private static List<ShipInfo> ShipInfoList = new List<ShipInfo>();
        private static List<ShipObjectInfo> ShipObjectInfoList = new List<ShipObjectInfo>();


        public List<string> ReturnClassAddedClassesAfterThisTime(long timeOfLastUpdate)
        {
            List<string> classesThatShouldUpdate = new List<string>();
            foreach (ClassData classss in ClassDataList) if (timeOfLastUpdate < classss.TimeOfLastUpdate)
                {
                classesThatShouldUpdate.Add(classss.ClassCodeToString());
            }            
            return classesThatShouldUpdate;
        }



        public void AddClassDataToAssets(ClassData CD, bool IsLoadedFromDatabase)
        {

            bool isclassDataNewOrUpdated = true;
            //foreach (ClassData classData in ClassDataList)
            //{
            //    if (classData.nameCode == CD.nameCode && classData.type == CD.type && classData.innerData == CD.innerData)
            //    {

            //        isclassDataNewOrUpdated = false;
            //        return;
            //    }
            //}
            if (!IsLoadedFromDatabase)
            {

                CD.TimeOfLastUpdate = DateTime.UtcNow.ToBinary();
                new Theif2dDataDBContext().AddClassDataToDataBase(CD);
            }
            ClassDataList.Add(CD);
            ClassDataType type = (ClassDataType)(CD.type);
            switch (type)
            {
                case ClassDataType.ShipInfo:
                    ShipInfo newShipInfo = new JavaScriptSerializer().Deserialize<ShipInfo>(CD.innerData);
                    ShipInfoList.Add(newShipInfo);
                    break;
                case ClassDataType.ShipObjectData:
                    ShipObjectInfo newShipObjectInfo = new JavaScriptSerializer().Deserialize<ShipObjectInfo>(CD.innerData);
                    newShipObjectInfo.FillarrayFromString();
                    ShipObjectInfoList.Add(newShipObjectInfo);
                    break;
                default:
                    break;
            }
            

        }



        public int AddAsset(Asset newasset)
        {
            int key = 0;
            Asset SameAsset = AssetsList.Find(x => x.Id == newasset.Id && x.typeOfbuyAbleObj == newasset.typeOfbuyAbleObj);
            if(SameAsset != null)
            {
                key = SameAsset.keyInDadabase;
                AssetsList.Remove(SameAsset);
            }
            else
            {
                key = AssetsList.Count+1;
            }
            newasset.keyInDadabase = key;
            AssetsList.Add(newasset);
            return key;
        }

        public Asset RetrunAsset(string name, int Id)
        {
            Asset SameAsset = AssetsList.Find(x => x.Id == Id && x.name == name);
            if(SameAsset == null)
            {
                ErrorSystem.AddBigError("assetManager.returnAsset. asset not finded");
            }
            return SameAsset;
        }


        public Asset RetrunAsset(buyableType Btype, int Id)
        {
            Asset SameAsset = AssetsList.Find(x => x.Id == Id && x.typeOfbuyAbleObj == Btype);
            if (SameAsset == null)
            {
                ErrorSystem.AddBigError("assetManager.returnAsset. asset not finded");
            }
            return SameAsset;
        }




        public static bool isAssetsLoadedFromDataBase = false;
        
    }
}
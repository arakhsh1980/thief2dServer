using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.blocks;
using thief2dServer.Models.utilities;
using System.Threading;

namespace thief2dServer.Models
{
    public class AssetManager
    {
        private static List<Asset> AssetsList = new List<Asset>();
       
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
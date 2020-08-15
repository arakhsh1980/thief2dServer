using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using thief2dServer.Models.blocks;
using thief2dServer.Models.utilities;
using System.Web.Script.Serialization;
using thief2dServer.Models;
using System.Data.Entity;


namespace thief2dServer.Controllers
{
    public class AdminController : Controller
    {
        Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
        [HttpPost]
        public string ResetAllPlayersInfos(FormCollection collection)
        {

            foreach (PlayerForDataBase p in dataBase.PlayerinDataBase)
            {
                dataBase.PlayerinDataBase.Remove(p);
            }
            dataBase.SaveChanges();
            PlayerListManager iiii = new PlayerListManager();
            iiii.ClearPlayerList();
            return true.ToString();
        }
        private static Mutex LoadAssetmutex = new Mutex();

        [HttpPost]
        public string UpdateBuyableAsset(FormCollection collection)
        {
            LoadAssetmutex.WaitOne();
            if (!AssetManager.isAssetsLoadedFromDataBase)
            {
                LoadAssetsFromDataBase();
            }
           
            Asset newAsset = new Asset();
            newAsset.Id = Int32.Parse(Request.Form["IdOfBuyable"]);
            newAsset.name = Request.Form["nameOfBuyable"];
            newAsset.price = new Property();
            newAsset.price.coin = Int32.Parse(Request.Form["priceOfBuyable"]);
            newAsset.typeOfbuyAbleObj = new Convertor().StringTobuyableType(Request.Form["typeOfBuyable"]);
            //newAsset.MaxNumberInEachLevel = new JavaScriptSerializer().Deserialize<short[]>(Request.Form["numberThatYouCanHaveInEachLevel"]);
            string kk = Request.Form["numberThatYouCanHaveInEachLevel"];
            string[]  nn =kk.Split('*');
           
                newAsset.MaxNumberInEachLevel = new short[nn.Length];
            for (int i = 0; i < nn.Length; i++)
            {
                newAsset.MaxNumberInEachLevel[i] = short.Parse(nn[i]);
            }
            newAsset.keyInDadabase= new AssetManager().AddAsset(newAsset);
            AssetForDataBase addedAsset = newAsset.ReturnDataBaseFormat();
            AddAssetToDataBase(addedAsset);
            dataBase.SaveChanges();
            LoadAssetmutex.ReleaseMutex();
            return newAsset.name+ " Updated";
        }


        private void LoadAssetsFromDataBase()
        {
            LoadAssetmutex.WaitOne();
            if (AssetManager.isAssetsLoadedFromDataBase) { return; }
            AssetForDataBase[] loadingAssets = dataBase.AssetinDataBase.ToArray();
            for (int i = 0; i < loadingAssets.Length; i++)
            {
                Asset newAsset = new Asset();
                newAsset.LoadFrom(loadingAssets[i]);
                new AssetManager().AddAsset(newAsset);
            }
            AssetManager.isAssetsLoadedFromDataBase = true;
            LoadAssetmutex.ReleaseMutex();
        }

        public void AddAssetToDataBase(AssetForDataBase p)
        {
            AssetForDataBase pp = dataBase.AssetinDataBase.Find(p.key);
            if (pp == null)
            {
                dataBase.AssetinDataBase.Add(p);
                dataBase.SaveChanges();
            }
            else
            {
                
                pp.MaxNumberInEachLevel = p.MaxNumberInEachLevel;
                pp.name = p.name;
                pp.price = p.price;
                dataBase.Entry(pp).State = EntityState.Modified;
                dataBase.SaveChanges();
            }
        }


    }
}
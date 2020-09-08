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
        
        public static Mutex AddAdminControlermutex = new Mutex();

        

        [HttpPost]
        public string AddClassData(FormCollection collection)
        {
            
            AddAdminControlermutex.WaitOne();
            new Theif2dDataDBContext().LoadForFisttimeIfNessecary();
            string AdminCode = Request.Form["AdminCode"];
            if (AdminCode != Constants.AdminCode)
            {
                AddAdminControlermutex.ReleaseMutex();
                return "wrong Admin Code";
            }
            //AssetManager.assentsLoaded.WaitOne();
            ClassData newClassData = new ClassData();
            newClassData.nameCode = Request.Form["CodeName"];
            newClassData.innerData = Request.Form["innerData"];
            newClassData.type = Int32.Parse(Request.Form["type"]);
            new AssetManager().AddClassDataToAssets(newClassData, false);
            AddAdminControlermutex.ReleaseMutex();
            return "ClassData Added Loaded" + newClassData.nameCode;
        }



    }
}
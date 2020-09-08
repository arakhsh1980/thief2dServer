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
    public class MakeStoryController : Controller
    {

       private static Mutex Story = new Mutex();
        Theif2dDataDBContext dataBase = new Theif2dDataDBContext();
        // POST: Loading/LoadPlayerData
        //[HttpPost]
        //public string LoadNewSceneData(FormCollection collection)
        //{
        //    Story.WaitOne();
        //    StoryLevel newSL = new StoryLevel();            
        //    int levelNumber = int.Parse( Request.Form["LevelNumber"]);
        //    int LevelShipBase = int.Parse(Request.Form["ShipBaseNumber"]);
        //    StoryLevel SL = dataBase.storylevelsDataBase.Find(levelNumber);
        //    if(SL!= null)
        //    {
        //        Story.ReleaseMutex();
        //        return "Error . this level existed";
        //    }
        //    else
        //    {
                
        //        newSL.level = levelNumber;
        //        newSL.shipBase = LevelShipBase;
        //        if(dataBase.ShipBaseDataBase.Find(LevelShipBase)== null)
        //        {
        //            Story.ReleaseMutex();
        //            return "Error . this ship bace is not existed";
        //        }
        //        else
        //        {
        //            newSL.levelString = dataBase.ShipBaseDataBase.Find(LevelShipBase).BaseString;
        //            dataBase.storylevelsDataBase.Add(newSL);
        //            dataBase.SaveChanges();
        //        }
        //    }
        //    Story.ReleaseMutex();
        //    return newSL.levelString;
        //}


        
        //[HttpPost]
        //public string LoadSceneData(FormCollection collection)
        //{
        //    Story.WaitOne();
        //    StoryLevel newSL = new StoryLevel();
        //    int levelNumber = int.Parse(Request.Form["LevelNumber"]);
        //    //int LevelShipBase = int.Parse(Request.Form["ShipBaseNumber"]);
        //    StoryLevel SL = dataBase.storylevelsDataBase.Find(levelNumber);
        //    if (SL == null)
        //    {
        //        Story.ReleaseMutex();
        //        return "Error . this level not existed";
        //    }
        //    else
        //    {
        //        Story.ReleaseMutex();
        //        return SL.levelString;
        //    }            
        //}


        //[HttpPost]
        //public string UpdateBuilding(FormCollection collection)
        //{
        //    Story.WaitOne();
        //     StoryLevel newSL = new StoryLevel();
        //    int levelNumber = int.Parse(Request.Form["LevelNumber"]);
        //    string buildingString = Request.Form["buildingString"];
        //    //int LevelShipBase = int.Parse(Request.Form["ShipBaseNumber"]);
        //    StoryLevel SL = dataBase.storylevelsDataBase.Find(levelNumber);
        //    if (SL == null)
        //    {
        //        Story.ReleaseMutex();
        //        return "Error . this level not existed";
        //    }
        //    else
        //    {
        //        SL.levelString = buildingString;
        //        dataBase.Entry(SL).State = EntityState.Modified;
        //        Story.ReleaseMutex();
        //        dataBase.SaveChanges();
        //        return "level "+ levelNumber.ToString() + " string Updated";
        //    }
        //}


    }
}
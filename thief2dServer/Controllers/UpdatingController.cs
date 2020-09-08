using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using thief2dServer.Models.blocks;
using thief2dServer.Models;

namespace thief2dServer.Controllers
{
    public class UpdatingController : Controller
    {
        //string filename = "test2";
        string Bundlespath = "D:/N/";
        public ActionResult ReturnBundle(string filename)
        {
            string path = Bundlespath + filename;

            if (System.IO.File.Exists(path))
            {
                byte[] fileBytes = GetFile(path);
                return File(fileBytes, "bondle");
            }
            return null;
        }
        private Theif2dDataDBContext dataBase = new Theif2dDataDBContext();

       
        [HttpPost]
        public string UpdateClasses(FormCollection collection)
        {
            new Theif2dDataDBContext().LoadForFisttimeIfNessecary();
            string lastTimeUpdate = Request.Form["lastTimeUpdate"];
            long lastTimeUpdateCode;
            if (!long.TryParse(lastTimeUpdate, out lastTimeUpdateCode))
            {
                lastTimeUpdateCode = long.MinValue;
            }
            List<string> ClassForUpdates = new AssetManager().ReturnClassAddedClassesAfterThisTime(lastTimeUpdateCode);
            if (ClassForUpdates.Count == 0)
            {
                return "NoNewUpdate";
            }
            else
            {
                return ClassesListToSingleString(ClassForUpdates, DateTime.UtcNow.ToBinary().ToString());
            }
        }

        string ClassesListToSingleString(List<string> inputs, string Time)// shared with client . change client if changed
        {
            string summOfAll = "";
            for (int i = 0; i < inputs.Count; i++)
            {
                summOfAll += inputs[i] + "*";
            }
            summOfAll += Time;
            return summOfAll;
        }

        string[] SingleStringToClasses(string sumOfClasses)// shared with client . change client if changed
        {
            return sumOfClasses.Split('*');
        }



       

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\folder\myfile.ext");
            string fileName = "myfile.ext";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

       






    }

}
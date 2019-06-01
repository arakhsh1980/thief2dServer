﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thief2dServer.Models.utilities
{
    public class LogMassage
    {        
        public int LogIndex { get; set; }
        public string MassageLog { get; set; }
        public string LogTime { get; set; }
        public string PlayerConnectionTime { get; set; }
        public string MatchCreationTime { get; set; }
    }

    public class LogSystem
    {
        //private static Log3DBContext logDataBase = new Log3DBContext();

        static string[] logs = new string[1000];
        static int Counter = 0;
        static int RuternLogCounter = 0;



        public static void AddLog(string LogBody)
        {

            LogMassage NewMassage = new LogMassage();
            NewMassage.MassageLog = LogBody;
            NewMassage.LogTime = "";
            //NewMassage.LogTime = DateTime.Now.ToString();

            AddStringLog(NewMassage.LogTime, LogBody);
            //logDataBase.GameLog3.Add(NewMassage);
            //logDataBase.SaveChanges();
        }

        public static void AddPlayerLog(string PlayerId, string LogBody)
        {


            LogMassage NewMassage = new LogMassage();
            NewMassage.MassageLog = LogBody;
            NewMassage.LogTime = "";
            //NewMassage.LogTime = DateTime.Now.ToString();
            NewMassage.PlayerConnectionTime = ""; // ConnectedPlayersList.ReturnPlayerConnectionTime(PlayerId).ToString();
            AddStringLog(" palyer Id " + PlayerId + ". ", LogBody);
            //logDataBase.GameLog3.Add(NewMassage);
            //logDataBase.SaveChanges();
        }
    
        private static void AddStringLog(string logTime, string LogBody)
        {
            if (Counter >= 1000) { Counter = 0; }
            logs[Counter] = logTime + " " + LogBody;
            Counter++;
        }

        private static string LogToString(LogMassage logMass)
        {
            string st;
            st = "in " + logMass.LogTime.ToString() + " : " + logMass.MassageLog;
            return st;
        }

        public static string RuternLog()
        {
            if (RuternLogCounter >= 1000) { RuternLogCounter = 0; }
            if (RuternLogCounter == Counter)
            {
                return "NoNew";
            }
            else
            {
                RuternLogCounter++;
                if (RuternLogCounter >= 1001) { RuternLogCounter = 1; }
                return logs[RuternLogCounter - 1];
            }
        }



    }
}
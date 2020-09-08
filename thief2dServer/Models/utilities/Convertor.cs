using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace thief2dServer.Models.utilities
{
    public class Convertor
    {
        public Property LongToProperty(long input)
        {
            Property newp = new Property();
            newp.coin = (int)input;
            return newp;
        }

        public long PropertyToLong(Property input)
        {
            return (long)input.coin;
        }

        public buyableType StringTobuyableType(string input)
        {
            buyableType type  ;
            switch (input)
            {
                case"weapon":
                    type = buyableType.weapon;
                    break;
                case "ceiling":
                    type = buyableType.ceiling;
                    break;
                case "treasure":
                    type = buyableType.treasure;
                    break;
                case "door":
                    type = buyableType.door;
                    break;
                case "shipWorker":
                    type = buyableType.shipWorker;                    
                    break;
                case "sleepPlace":
                    type = buyableType.sleepPlace;
                    break;
                case "eatingPlace":
                    type = buyableType.eatingPlace;
                    break;
                case "factory":
                    type = buyableType.factory;
                    break;
                default:
                    type = buyableType.none;
                    break;
            }
            return type;
        }

        public short[] StringToShortArray(string ar)
        {
            short[] kk = new JavaScriptSerializer().Deserialize<short[]>(ar);
            return kk;
        }

        public string ShortArrayToSrting(short[] ar)
        {
            string uu;
            uu = new JavaScriptSerializer().Serialize(ar);
            return uu;
        }



        public static string LongArrayToSrtingWithChar(long[] ar, Char ch)
        {
            if (ar.Length < 1)
            {
                return "";
            }
            string uu = "";
            uu = ar[0].ToString();
            for (int i = 1; i < ar.Length; i++)
            {
                uu += ch + ar[i].ToString();
            }
            return uu;
        }

        public static long[] StringToLongArrayWithChar(string ar, Char ch)
        {
            string[] splitted = ar.Split(ch);
            long[] res = new long[splitted.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = long.Parse(splitted[i]);
            }
            return res;
        }


        public static string IntArrayToSrtingWithChar(int[] ar, Char ch)
        {
            if (ar.Length < 1)
            {
                return "";
            }
            string uu = "";
            uu = ar[0].ToString();
            for (int i = 1; i < ar.Length; i++)
            {
                uu += ch + ar[i].ToString();
            }
            return uu;
        }

        public static int[] StringToIntArrayWithChar(string ar, Char ch)
        {
            if (ar.Length < 2) { return null; }
            string[] splitted = ar.Split(ch);
            int[] res = new int[splitted.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = int.Parse(splitted[i]);
            }
            return res;
        }



    }
}
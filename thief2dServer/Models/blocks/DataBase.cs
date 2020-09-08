using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using thief2dServer.Models.blocks;
using System.Threading;

namespace thief2dServer.Models.blocks
{
    public class Theif2dDataDBContext : DbContext
    {
        

        public static Mutex LoadAssetmutex = new Mutex();
        static Theif2dDataDBContext()
        {
            Database.SetInitializer<Theif2dDataDBContext>(new CreateDatabaseIfNotExists<Theif2dDataDBContext>());
            //Database.SetInitializer<Theif2dDataDBContext>(new DropCreateDatabaseAlways<Theif2dDataDBContext>());


        }
        public DbSet<PlayerForDataBase> PlayerinDataBase { get; set; }

        public DbSet<ClassData> allClassesData { get; set; }

        public DbSet<DualString> AllDualStrings { get; set; }

        // public DbSet<StoryLevel> storylevelsDataBase { get; set; }

        //public DbSet<ShipBase> ShipBaseDataBase { get; set; }

        

        public DbSet<ShipForDataBase> AllShips { get; set; }


        public void AddClassDataToDataBase(ClassData CD)
        {

            ClassData pp = allClassesData.Find(CD.nameCode);
            if (pp == null)
            {

                allClassesData.Add(CD);
                SaveChanges();
            }
            else
            {
                allClassesData.Remove(pp);
                SaveChanges();
                allClassesData.Add(CD);
                SaveChanges();
            }
        }

        bool isLoaded = false;

        public void LoadForFisttimeIfNessecary()
        {

            if (isLoaded) { return; }
            LoadAssetmutex.WaitOne();
            ClassData[] allClassesDataArray = allClassesData.ToArray();
            for (int i = 0; i < allClassesDataArray.Length; i++)
            {
                new AssetManager().AddClassDataToAssets(allClassesDataArray[i], true);
            }
            isLoaded = true;
            LoadAssetmutex.ReleaseMutex();
        }
    }

    public class databaseGameInitial : System.Data.Entity.DropCreateDatabaseIfModelChanges<Theif2dDataDBContext>
    {
        protected override void Seed(Theif2dDataDBContext context)
        {
            context.SaveChanges();
        }
    }

    
}
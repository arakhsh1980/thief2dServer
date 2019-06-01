using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using thief2dServer.Models.blocks;

namespace thief2dServer.Models.blocks
{
    public class Theif2dDataDBContext : DbContext
    {
        static Theif2dDataDBContext()
        {
            Database.SetInitializer<Theif2dDataDBContext>(new CreateDatabaseIfNotExists<Theif2dDataDBContext>());
            //Database.SetInitializer<Theif2dDataDBContext>(new DropCreateDatabaseAlways<Theif2dDataDBContext>());
           
            
        }
        public DbSet<PlayerForDataBase> Buildings { get; set; }
    }

    public class databaseGameInitial : System.Data.Entity.DropCreateDatabaseIfModelChanges<Theif2dDataDBContext>
    {
        protected override void Seed(Theif2dDataDBContext context)
        {
            context.SaveChanges();
        }
    }
}
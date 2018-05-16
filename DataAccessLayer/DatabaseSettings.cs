using System.Data.Entity;


namespace DataAccessLayer
{
   public class DatabaseSettings
    {
        public static void SetDataBase()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SalesERPDAL>());
        }
    }
}

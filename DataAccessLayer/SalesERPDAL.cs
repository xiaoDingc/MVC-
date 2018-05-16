using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class SalesERPDAL : DbContext
    {
        public SalesERPDAL() : base("name=SalesERPDAL")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }

        //增加该方法后,数据库不在报错.
        public void FixEfProviderServicesProblem()
        { //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer' //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. //Make sure the provider assembly is available to the running application. //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information. 
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
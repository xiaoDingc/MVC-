using MVC学习.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC学习.Data_Access_Layer
{
    public class SalesERPDAL:DbContext
    {
        public SalesERPDAL() : base("name=SalesERPDAL")
        {
       

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }

      public  DbSet<Employee> Employees { get; set; }

    }
}
using DataAccessLayer;
using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public UserStatus IsValidUser(UserDetails u)
        {
            if (u.UserName=="Admin"&&u.Password=="Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if(u.UserName=="Sukesh"&& u.Password == "Sukesh")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
              
        }

        public Employee SaveEmployee(Employee e)
        {
            using (var salesDal=new SalesERPDAL())
            {
                salesDal.Employees.Add(e);
                salesDal.SaveChanges();
                return e;
            }
        }
       
        public void UploadEmployee(List<Employee> employees)
        {
            try
            {
                SalesERPDAL salesErp = new SalesERPDAL();
                salesErp.Employees.AddRange(employees);
                salesErp.SaveChanges();
            }
            catch (DbEntityValidationException dEx)
            {

                Console.WriteLine(dEx.Message);
            }
           
        }
    }
}
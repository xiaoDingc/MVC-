using MVC学习.Data_Access_Layer;
using MVC学习.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC学习.ViewModels
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
       
    }
}
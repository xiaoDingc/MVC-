using MVC学习.Filter;
using MVC学习.Models;
using MVC学习.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVC学习.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Test
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public string GetString()
        {
            return "this is test GetString";
        }
        public Customer GetCustomer()
        {
            Customer c = new Customer();
            c.CustomerName = "Customer 1";
            c.Address = "Address1";
            return c;
        }
        [NonAction]
        public string SimpleMethod()
        {
            return "great simple";
        }
        [Authorize]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            //employeeListViewModel.UserName = User.Identity.Name;
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName= "StepByStepSchools";//Can be set to dynamic value
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();

            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();

            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            //employeeListViewModel.UserName = "Admin";
            return View(employeeListViewModel);
        }
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            //employeeListViewModel.UserName = User.Identity.Name;
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();

            return View("AddNew", employeeListViewModel);
        }
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e,string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        //vm.FooterData = new FooterViewModel();
                        //vm.FooterData.CompanyName = "StepByStepSchools";
                        //vm.FooterData.Year = DateTime.Now.Year.ToString();
                        //vm.UserName = User.Identity.Name;

                        if (e.Salary.ToString() !="")
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
                        return View("AddNew",vm);
                    }
                  
                case "Cancel":
                    return RedirectToAction("AddNew");
            }
            return new EmptyResult();
        }
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}

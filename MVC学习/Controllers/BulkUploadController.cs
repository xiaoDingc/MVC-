using MVC学习.Filter;
using MVC学习.Models;
using MVC学习.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace MVC学习.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }
        public async Task<ActionResult>  Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await 
                Task.Factory.StartNew<List<Employee>>(() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployee(employees);
            return RedirectToAction("Index", "Employee");

        }
        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employee = new List<Employee>();
            StreamReader csvReader = new StreamReader(model.fileUpload.InputStream);
            csvReader.ReadLine();
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                var values = line.Split(new char[] { ',' });
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                //e.EmployeeId = int.Parse(values[3]);
                employee.Add(e);
            }
            return employee;
            
        }
    }
}
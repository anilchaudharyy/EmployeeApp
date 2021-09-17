using EmployeeCrudNoAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrudNoAuthentication.Data;
using EmployeeCrudNoAuthentication.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using EmployeeCrudNoAuthentication.APIClient;

namespace EmployeeCrudNoAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public EmployeeDbContext dbobje;
        private readonly HttpClient _httpClient;
        private ApiClient apiObj;
        public HomeController(EmployeeDbContext _dbobj)
        {
            dbobje = _dbobj;
            _httpClient = new HttpClient();
            apiObj = new ApiClient();
        }
        public async Task<IActionResult> Index2()
        {
            Employee emp = new Employee();
          //  var employees = dbobje.Employee.ToList();
            var response = await _httpClient.GetAsync("http://localhost:58144/api/home/GetEmployees", HttpCompletionOption.ResponseHeadersRead);
            // var status =  response.;
            var data = await response.Content.ReadAsStringAsync();
            var empl =  JsonConvert.DeserializeObject<List<Employee>>(data);
            return View(empl);
        }
        public IActionResult Index()
        {
            Employee emp = new Employee();
             var employees = dbobje.Employee.ToList();
            return View(employees);
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            var emp = dbobje.Employee.Where(x => x.Id == id).SingleOrDefault();
            return View(emp);
        }
        public IActionResult editAjax(int id)
        {
            try{
                var emp = dbobje.Employee.Where(x => x.Id == id).SingleOrDefault();
                
              return View("edit", emp);
               
            }
            catch
            {
                return null;
            }
            
        }

       

        [HttpPost]
        public async Task<IActionResult> Edit(Employee empDetail)
        {
            var emp = dbobje.Employee.Where(x => x.Id == empDetail.Id).SingleOrDefault();
            emp.EmployeeName = empDetail.EmployeeName;
            emp.EmployeeAddress = empDetail.EmployeeAddress;
            emp.EmployeeSalary = empDetail.EmployeeSalary;
            dbobje.Entry(emp).State = EntityState.Modified;
            dbobje.SaveChanges();

            //edit in student Db 
            Student st = new Student();
            st.Id = 1;
            st.Name = "hhh";
         var students =  await apiObj.PostAsync<List<Student>>("http://localhost:58144/api/home/EditStudent", st);
           return  RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

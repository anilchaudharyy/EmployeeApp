using EmployeeCrudNoAuthentication.APIClient;
using EmployeeCrudNoAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudNoAuthentication.Controllers
{
    public class StudentController : Controller
    {
        private ApiClient apiObj;
        public StudentController()
        {
            apiObj = new ApiClient();
        }
        public async Task<IActionResult> StudentList()
        {
            var students = await apiObj.GetTAsync<List<Student>>("http://localhost:58144/api/home/GetEmployees");
            return View();
        }

    }
}

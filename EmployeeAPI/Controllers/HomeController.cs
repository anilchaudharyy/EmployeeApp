using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public StudentDbContext dbobj;

        public HomeController(StudentDbContext _dbobj)
        {
            dbobj = _dbobj;
        }
        public IEnumerable<Student> GetEmployees()
        {
            var students = dbobj.Student.ToList();
            return students;
        }
        [HttpPost]
        public IEnumerable<Student> EditStudent(Student st)
        {
            var student = dbobj.Student.Where(x=>x.Id==st.Id).FirstOrDefault();
            student.Name = st.Name;
            student.Address = st.Address;
            dbobj.Entry(student).State = EntityState.Modified;
            dbobj.SaveChanges();
            var students = dbobj.Student.ToList();           
            
            return students;
        }
    }
}

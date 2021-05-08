using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Angular___CRUD.Configurations;
using Angular___CRUD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Angular___CRUD.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public IActionResult GetList()
        {
            List<Employee> oEmployeeList = _context.Employees.AsNoTracking().ToList();
            return Ok(new JsonResult(oEmployeeList));
        }

        [HttpGet]
        [Route("GetEmployee/{Id}")]
        public IActionResult GetItem(int id)
        {
            if (id > 0)
            {
                Employee oEmployeeItem = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
                return Ok(new JsonResult(oEmployeeItem));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult Insert(Employee oEmployee)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (oEmployee.EmployeeId > 1) oEmployee.EmployeeId = 0;

            var oEntity = _context.Employees.Add(oEmployee);
            _context.SaveChanges();
            var oDbEmployee = 0;

            return CreatedAtAction("Insert", oEmployee, oDbEmployee);
        }

        [HttpPut]
        [Route("UpdateEmployee/{Id}")]
        public IActionResult Update(Employee oEmployee)
        {
            if (!ModelState.IsValid) return BadRequest();

            _context.Employees.Update(oEmployee);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete]
        [Route("DeleteEmployee/{Id}")]
        public IActionResult Delete(Employee oEmployee)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (oEmployee.EmployeeId > 0)
            {
                var oEntity = _context.Employees.Remove(oEmployee);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();

        }

        [HttpPost]
        [Route("SaveFile")]
        public IActionResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _environment.ContentRootPath + "/Photos/" + filename;

                using var stream = new FileStream(physicalPath, FileMode.Create);
                postedFile.CopyTo(stream);

                return new JsonResult(filename);
            }
            catch(Exception)
            {
                return new JsonResult("anonomous.png");
            }
        }
    }
}

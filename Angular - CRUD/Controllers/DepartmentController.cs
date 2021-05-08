using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular___CRUD.Configurations;
using Angular___CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Angular___CRUD.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
   
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public IActionResult GetList()
        {
            List<Department> oDepartmentList = _context.Departments.AsNoTracking().ToList();
            return Ok(new JsonResult(oDepartmentList));
        }
        
        [HttpGet]
        [Route("GetDepartment/{Id}")]
        public IActionResult GetItem(int id)
        {
            if (id > 0)
            {
                Department oDepartmentItem = _context.Departments.FirstOrDefault(x => x.DepartmentId == id);
                return Ok(new JsonResult(oDepartmentItem));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("AddDepartment")]
        public IActionResult Insert(Department oDepartment)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (oDepartment.DepartmentId > 1) oDepartment.DepartmentId = 0;
            
            var oEntity = _context.Departments.Add(oDepartment);
            _context.SaveChanges();

            Department oDbDepartment = new Department(oEntity.Entity.DepartmentId, oEntity.Entity.DepartmentName);

            return CreatedAtAction("Insert", oDepartment, oDbDepartment);
        }

        [HttpPut]
        [Route("UpdateDepartment/{Id}")]
        public IActionResult Update(Department oDepartment)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            _context.Departments.Update(oDepartment);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete]
        [Route("DeleteDepartment/{Id}")]
        public IActionResult Delete(Department oDepartment)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (oDepartment.DepartmentId > 0)
            {
                var oEntity = _context.Departments.Remove(oDepartment);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();

        }

    }
}

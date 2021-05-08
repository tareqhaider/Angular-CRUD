using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular___CRUD.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<Employee> EmployeeList { get; set; }

        public Department(int id, string name)
        {
            DepartmentId = id;
            DepartmentName = name;
        }
    }
}

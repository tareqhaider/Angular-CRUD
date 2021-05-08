using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular___CRUD.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}

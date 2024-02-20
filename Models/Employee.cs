using System.Collections.Generic;

namespace Coursework.Models
{
    public class Employee
    {
        public int EmployeeId {get; set;}
        public string Name {get; set;}
        public int DepartmentId {get; set;}

        public Department Department {get; set;}
    }
    
}
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coursework.Models
{
    public class Employee
    {
        public int EmployeeId {get; set;}
        public string Name {get; set;}
        public int? DepartmentId {get; set;}
        [JsonIgnore]
        public Department? Department {get; set;}
    }
    
}
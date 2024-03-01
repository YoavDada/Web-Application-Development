using System.Collections.Generic;

namespace Coursework.Models
{
    public class Project
    {
        public int ProjectId {get; set;}
        public string ProjectName {get; set;}
        public string StartDate {get; set;}
        public string EndDate {get; set;}
        public int? ProjectManagerId {get; set;}
        public string? ClientName {get; set;}

        public Employee? Manager {get; set;}
        public Client? Client {get; set;}

    }
}
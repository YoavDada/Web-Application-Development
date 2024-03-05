using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Employee? Manager {get; set;}
        [JsonIgnore]
        public Client? Client {get; set;}

    }
}
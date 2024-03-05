using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coursework.Models
{
    public class Expense
    {
        public int ExpenseId {get; set;}
        public int? ProjectId {get; set;}
        public int Amount {get; set;}
        public string ExpenseDate {get; set;}
        [JsonIgnore]
        public Project? Project {get; set;}

    }
}
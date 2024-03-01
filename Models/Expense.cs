using System.Collections.Generic;

namespace Coursework.Models
{
    public class Expense
    {
        public int ExpenseId {get; set;}
        public int? ProjectId {get; set;}
        public int Amount {get; set;}
        public string ExpenseDate {get; set;}

        public Project? Project {get; set;}

    }
}
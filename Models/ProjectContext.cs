using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Coursework.Models
{
    public class ProjectContext : IdentityDbContext<IdentityUser>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients {get; set;}
        public DbSet<Department> Departments {get; set;}
        public DbSet<Employee> Employees {get; set;}
        public DbSet<Expense> Expenses {get; set;}
        public DbSet<Project> Projects {get; set;}
    }
}
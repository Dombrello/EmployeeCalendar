using EmployeeCallendar.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Windows.Security.Cryptography.Certificates;

namespace EmployeeCallendar.Controllers;

public class EmployeesCallendarDbContext : DbContext
{
    public DbSet<CallendarModel> Callendar { get; set; }
    public DbSet<UserModel> User { get; set; }

    public EmployeesCallendarDbContext(DbContextOptions<EmployeesCallendarDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source = X299; Initial Catalog = EmployeesCallendar; Integrated Security = True; Trust Server Certificate = True");
    }
}
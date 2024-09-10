namespace EmployeeCallendar.Models;

using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string username { get; set; }
    public string role { get; set; }
}
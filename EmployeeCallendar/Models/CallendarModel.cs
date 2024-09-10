using System.Diagnostics.CodeAnalysis;

namespace EmployeeCallendar.Models;

public class CallendarModel
{
    public int Id { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    [NotNull]
    public UserModel User { get; set; }
}
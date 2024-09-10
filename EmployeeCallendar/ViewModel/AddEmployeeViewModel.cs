using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using EmployeeCallendar.Controllers;
using EmployeeCallendar.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCallendar.ViewModel;

public class AddEmployeeViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public EmployeesCallendarDbContext DBContext { get; set; }
    public List<UserModel> Users { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string ObFirstName
    {
        get
        {
            return FirstName;
        }
        set
        {
            FirstName = value;
            OnPropertyChanged();
        }
    }
    public string ObLastName
    {
        get
        {
            return LastName;
        }
        set
        {
            LastName = value;
            OnPropertyChanged();
        }
    }
    public string ObEmail
    {
        get
        {
            return Email;
        }
        set
        {
            Email = value;
            OnPropertyChanged();
        }
    }
    public string ObPhone
    {
        get
        {
            return Phone;
        }
        set
        {
            Phone = value;
            OnPropertyChanged();
        }
    }
    public string ObUsername
    {
        get
        {
            return Username;
        }
        set
        {
            Username = value;
            OnPropertyChanged();
        }
    }
    public string ObPassword {
        get
        {
            return Password;
        }
        set
        {
            Password = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<UserModel> ObUsers {get; set; }
    public ICommand RegisterEmployeeCommand { get; }

    public AddEmployeeViewModel()
    {
        DBContext = new EmployeesCallendarDbContext(new DbContextOptions<EmployeesCallendarDbContext>());
        ObUsers = new ObservableCollection<UserModel>();
        RegisterEmployeeCommand = new Command<object>((o) =>
        {
            RegisterEmployee();
        }, (object parameter) => true);
        InitViewModel();
    }

    private async void InitViewModel()
    {
        await DBContext.Database.EnsureCreatedAsync();
        GetUsersList();
    }

    private void GetUsersList()
    {
        var uList = DBContext.User.Where(o => o.role == "employee").ToList<UserModel>();
        uList.ForEach(user => { ObUsers.Add(user); });
    }

    public void RegisterEmployee()
    {
        UserModel u = new UserModel
        {
            FirstName = FirstName,
            LastName = LastName,
            email = Email,
            phone = Phone,
            role = "employee",
            username = Username,
            password = BCrypt.Net.BCrypt.HashPassword(Password)
        };

        DBContext.User.Add(u);
        int saved = DBContext.SaveChanges();
        if (saved != 0)
        {
            ObUsers.Add(u);
        }
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
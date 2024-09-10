using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EmployeeCallendar.Controllers;
using Microsoft.EntityFrameworkCore;
using Plugin.Maui.Calendar.Models;
using EmployeeCallendar.Models;
using WinRT;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using System.Windows.Forms;

namespace EmployeeCallendar.ViewModel;

public class EmployeeCallendarViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private EventCollection AllUserEvents { get; set; }

    public EventCollection ObAllUserEvents
    {
        get
        {
            return AllUserEvents;
        }
        set
        {
            AllUserEvents = value;
            OnPropertyChanged();
        }
    }
    public EventCollection SingleUserEvents { get; set; }
    public EmployeesCallendarDbContext DBContext { get; set; }
    public ObservableCollection<UserModel> Users { get; set; }
    public DateTime DataOd { get; set; }
    public DateTime DataDo {get; set; }
    public UserModel selectedUser { get; set; }
    public DateTime minDate { get; set; }

    public ICommand DodajUrlop { get; set; }

    public EmployeeCallendarViewModel()
    {
        DBContext = new EmployeesCallendarDbContext(new DbContextOptions<EmployeesCallendarDbContext>());
        Users = new ObservableCollection<UserModel>();
        minDate = DateTime.Now;

        ObAllUserEvents = new();

        DodajUrlop = new Command(() =>
        {
            UserModel selUser = selectedUser.As<UserModel>();
            DBContext.Callendar.Add(new CallendarModel
            {
                DateFrom = DateOnly.FromDateTime(DataOd),
                DateTo = DateOnly.FromDateTime(DataDo),
                User = selUser
            });
            DBContext.SaveChanges();


            foreach (DateTime day in EachDay(DataOd, DataDo))
            {

                if (!ObAllUserEvents.ContainsKey(day))
                {
                    ObAllUserEvents.Add(day, new List<CallendarEventModel>
                    {
                        new CallendarEventModel
                        {
                            fname = selUser.FirstName,
                            lname = selUser.LastName,
                            uid = selUser.Id
                        }
                    });
                }
                else
                {

                    List<CallendarEventModel> UserEvents = ObAllUserEvents[day] as List<CallendarEventModel>;
                    UserEvents.Add(new CallendarEventModel
                    {
                        fname = selUser.FirstName,
                        lname = selUser.LastName,
                        uid = selUser.Id
                    });
                    ObAllUserEvents[day] = UserEvents;
                }

            }

        }, () => true);

        InitEmployeesEvents();
    }

    public async void InitEmployeesEvents()
    {
        await DBContext.Database.EnsureCreatedAsync();
        if (DBContext.User != null)
        {
            foreach (var users in DBContext.User)
            {
                Users.Add(users);
            }
        }
        

        foreach (var events in DBContext.Callendar)
        {
            foreach (DateTime day in EachDay(events.DateFrom.ToDateTime(TimeOnly.Parse("10:00 PM")), events.DateTo.ToDateTime(TimeOnly.Parse("10:00 PM"))))
            {
                if (!ObAllUserEvents.ContainsKey(day))
                {
                    ObAllUserEvents.Add(day, new List<CallendarEventModel>
                    {
                        new CallendarEventModel
                        {
                            fname = events.User.FirstName,
                            lname = events.User.LastName,
                            uid = events.User.Id,
                        }
                    });
                }
                else
                {

                    List<CallendarEventModel> UserEvents = ObAllUserEvents[day] as List<CallendarEventModel>;
                    UserEvents.Add(new CallendarEventModel
                    {
                        fname = events.User.FirstName,
                        lname = events.User.LastName,
                        uid = events.User.Id
                    });
                    ObAllUserEvents[day] = UserEvents;
                }
                
            }
        }
    }

    public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            yield return day;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime;
using System.Xml.Linq;
public class Person
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }

    public Person(string? name, string? surname, DateTime birthday)
    {
        Name = name;
        Surname = surname;
        Age = DateTime.Now.Year - birthday.Year;
    }

    public override string ToString()
    {
        return $"\nName:{Name}\nSurname:{Surname}\nAge:{Age}\b";
    }
}
static public class Specialities
{
    static public string Pediatoloq { get; set; } = "Pediatoloq";
    static public string Travmatoloq { get; set; } = "Travmatoloq";
    static public string Stamotoloq { get; set; } = "Stamotoloq";
}
public struct WorkTime
{
    public int StartHours { get; }
    public int StartMinutes { get; }
    public int EndHours { get; }
    public int EndMinutes { get; }
    public WorkTime(int starthours, int startminutes, int endhours, int endminutes)
    {
        StartHours = starthours;
        StartMinutes = startminutes;
        EndHours = endhours;
        EndMinutes = endminutes;
    }
    public override string ToString()
    {
        return $"{StartHours:D2}:{StartMinutes:D2}-{EndHours:D2}:{EndMinutes:D2}";
    }
    public static bool operator ==(WorkTime w1, WorkTime w2)
    {
        return w1.StartHours == w2.StartHours;
    }
    public static bool operator !=(WorkTime w1, WorkTime w2)
    {
        return !(w1.StartHours == w2.StartHours);
    }
}

public class Doctor : Person
{
    public string? Speciality { get; set; }
    public int ExperienceYear { get; set; }

    private WorkTime[] worktimes = new WorkTime[3];

    public WorkTime[] WorkTimes
    {
        get { return worktimes; }
        set { worktimes = value; }
    }


    private WorkTime[] busytimes = new WorkTime[0];

    public WorkTime[] BusyTimes
    {
        get { return busytimes; }
        set { busytimes = value; }
    }



    public void AddBusyTime(WorkTime newBusyTime)
    {
        Array.Resize(ref busytimes, busytimes.GetLength(0) + 1);
        busytimes[busytimes.GetLength(0) - 1] = newBusyTime;
    }
    public Doctor(string? name, string? surname, DateTime birthday, string? speciality, int experienceYear)
    : base(name, surname, birthday)
    {

        Name = name;
        Surname = surname;
        Speciality = speciality;
        ExperienceYear = experienceYear;
        worktimes[worktimes.GetLength(0) - 3] = new WorkTime(9, 00, 11, 00);
        worktimes[worktimes.GetLength(0) - 2] = new WorkTime(12, 00, 14, 00);
        worktimes[worktimes.GetLength(0) - 1] = new WorkTime(15, 00, 17, 00);
    }
    public override string ToString()
    {
        return $"\nName:{Name}\nSurname:{Surname}\nAge:{Age}\nExperience:{ExperienceYear} years\nBranch:{Speciality}\nWorktimes:\n{WorkTimes[0]}\n{WorkTimes[1]}\n{WorkTimes[2]}\n-----------";
    }

}

public class User : Person
{
    public string? ChoicedBranch { get; set; }
    public Doctor? ChoicedDoctor { get; set; }
    public WorkTime DateTimeWithDoctor { get; set; }
    public User(string? name, string? surname, DateTime birthday)
    : base(name, surname, birthday)
    {

    }

    public override string ToString()
    {
        return $"\nPatient info.\nName:{Name}\nSurname:{Surname}\nChoiced Branch:{ChoicedBranch}\nChoiced Datetime:{DateTimeWithDoctor}\n\nThe choiced Doctors info.{ChoicedDoctor}";
    }
}
public class Hospital
{
    private Doctor[] pediatriyaDoctors = new Doctor[0];
    public Doctor[] PediatriyaDoctors
    {
        get { return pediatriyaDoctors; }
        set { pediatriyaDoctors = value; }
    }

    private Doctor[] travmatologiyaDoctors = new Doctor[0];
    public Doctor[] TravmatologiyaDoctors
    {
        get { return travmatologiyaDoctors; }
        set { travmatologiyaDoctors = value; }
    }

    private Doctor[] stamotologiyaDoctors = new Doctor[0];
    public Doctor[] StamotologiyaDoctors
    {
        get { return stamotologiyaDoctors; }
        set { stamotologiyaDoctors = value; }
    }

    private User[] users = new User[0];
    public User[] Users
    {
        get { return users; }
        set { users = value; }
    }

    public void AddDoctor(Doctor newdoctor)
    {
        if (newdoctor.Speciality == Specialities.Pediatoloq)
        {
            Array.Resize(ref pediatriyaDoctors, pediatriyaDoctors.GetLength(0) + 1);
            pediatriyaDoctors[pediatriyaDoctors.GetLength(0) - 1] = newdoctor;
        }
        if (newdoctor.Speciality == Specialities.Travmatoloq)
        {
            Array.Resize(ref travmatologiyaDoctors, travmatologiyaDoctors.GetLength(0) + 1);
            travmatologiyaDoctors[travmatologiyaDoctors.GetLength(0) - 1] = newdoctor;
        }
        if (newdoctor.Speciality == Specialities.Pediatoloq)
        {
            Array.Resize(ref stamotologiyaDoctors, stamotologiyaDoctors.GetLength(0) + 1);
            stamotologiyaDoctors[stamotologiyaDoctors.GetLength(0) - 1] = newdoctor;
        }
    }

    public void AddUser(User newUser)
    {
        Array.Resize(ref users, users.GetLength(0) + 1);
        users[users.GetLength(0) - 1] = newUser;
    }

    public void Registration()
    {
        Console.Clear();
        string? Name;
        string? Surname;
        DateTime Birthday;
        Console.Write("Name:"); Name = Console.ReadLine();
        Console.Write("Surame:"); Surname = Console.ReadLine();
        Console.Write("Birthday:(Year (spacebar) Month (spacebar) Day (enter) ):"); Birthday = Convert.ToDateTime(Console.ReadLine());
        AddUser(new User(Name, Surname, Birthday));
    }
    public void UserChoiceBranch(User userprofile)
    {
        Console.Clear();
        Console.Write("Choice branche:\n1:Pediatoloq\n2:Travmatoloq\n3:Stomotoloq\n");
        int choiceBranch = int.Parse(Console.ReadLine());
        switch (choiceBranch)
        {
            case 1:
                userprofile.ChoicedBranch = Specialities.Pediatoloq;
                break;
            case 2:
                userprofile.ChoicedBranch = Specialities.Travmatoloq;
                break;
            case 3:
                userprofile.ChoicedBranch = Specialities.Stamotoloq;
                break;
        }
    }
    public void UserChoiceDoctor(User userprofile)
    {
        Console.Clear();
        if (userprofile.ChoicedBranch == Specialities.Pediatoloq)
        {
            Console.WriteLine("There is all pediatoloq Doctors:");
            for (int i = 0; i < pediatriyaDoctors.GetLength(0); i++)
            {
                Console.Write($"{i + 1}:");
                Console.WriteLine(pediatriyaDoctors[i]);
            }
            Console.Write("Coice one of them:");
            int cdoctorIndex = int.Parse(Console.ReadLine());
            userprofile.ChoicedDoctor = pediatriyaDoctors[cdoctorIndex - 1];
        }
        if (userprofile.ChoicedBranch == Specialities.Travmatoloq)
        {
            Console.WriteLine("There is all pediatoloq Doctors:");
            for (int i = 0; i < travmatologiyaDoctors.GetLength(0); i++)
            {
                Console.Write($"{i + 1}:");
                Console.WriteLine(travmatologiyaDoctors[i]);
            }
            Console.Write("Coice one of them:");
            int cdoctorIndex = int.Parse(Console.ReadLine());
            userprofile.ChoicedDoctor = travmatologiyaDoctors[cdoctorIndex - 1];
        }
        if (userprofile.ChoicedBranch == Specialities.Stamotoloq)
        {
            Console.WriteLine("There is all pediatoloq Doctors:");
            for (int i = 0; i < stamotologiyaDoctors.GetLength(0); i++)
            {
                Console.Write($"{i + 1}:");
                Console.WriteLine(stamotologiyaDoctors[i]);
            }
            Console.Write("Coice one of them:");
            int cdoctorIndex = int.Parse(Console.ReadLine());
            userprofile.ChoicedDoctor = stamotologiyaDoctors[cdoctorIndex - 1];
        }
    }
    public void UserChoiceMeetTime(User userprofile)
    {
        Console.Clear();
        Console.WriteLine($"Choice meet time:\n1:{userprofile.ChoicedDoctor.WorkTimes[0]}\n2:{userprofile.ChoicedDoctor.WorkTimes[1]}\n3:{userprofile.ChoicedDoctor.WorkTimes[2]}\n");
        int choiceMeetTime = int.Parse(Console.ReadLine());
        for (int i = 0; i < userprofile.ChoicedDoctor.BusyTimes.GetLength(0); i++)
        {
            if (userprofile.ChoicedDoctor.BusyTimes[i] == userprofile.ChoicedDoctor.WorkTimes[choiceMeetTime - 1])
            {
                Console.WriteLine($"Doctor {userprofile.ChoicedDoctor.Name} at this time busy please select another time");
                Thread.Sleep(4000);
                UserChoiceMeetTime(userprofile);
            }
        }
        userprofile.DateTimeWithDoctor = userprofile.ChoicedDoctor.WorkTimes[choiceMeetTime - 1];
        userprofile.ChoicedDoctor.AddBusyTime(userprofile.DateTimeWithDoctor);
    }

    public void UserChoices(User userprofile)
    {
        UserChoiceBranch(userprofile);
        UserChoiceDoctor(userprofile);
        UserChoiceMeetTime(userprofile);
    }

    public void start()
    {
        Console.Clear();
        Registration();
        UserChoices(users[users.GetLength(0) - 1]);
        start();
    }
}


internal class Program
{
    static void Main()
    {
        Hospital hospital = new Hospital();
        hospital.AddDoctor(new Doctor("Irfan", "Irfanov", new DateTime(1975, 1, 12), Specialities.Pediatoloq, 11));
        hospital.AddDoctor(new Doctor("Qadir", "Nazimov", new DateTime(1975, 1, 12), Specialities.Pediatoloq, 9));
        hospital.AddDoctor(new Doctor("Sefer", "Irfanov", new DateTime(1975, 1, 12), Specialities.Travmatoloq, 5));
        hospital.AddDoctor(new Doctor("Lale", "Irfanova", new DateTime(1975, 1, 12), Specialities.Travmatoloq, 9));
        hospital.AddDoctor(new Doctor("Lazim", "Seferov", new DateTime(1975, 1, 12), Specialities.Stamotoloq, 19));
        hospital.AddDoctor(new Doctor("Qazim", "Qadirov", new DateTime(1975, 1, 12), Specialities.Stamotoloq, 7));
        hospital.start();

    }
}
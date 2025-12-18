using Employee_CFA.Data;
using Employee_CFA.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;
using System.Diagnostics.Metrics;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                Console.WriteLine("........CRUD OPERATIONS............");

                Console.WriteLine("1.ADD Employee Details");
                Console.WriteLine("2.READ Employee Details");
                Console.WriteLine("3.UPDATE Employee Details");
                Console.WriteLine("4.DELETE Employee Details");
                Console.WriteLine("5.EXIT");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Add(context);
                        break;

                    case "2":
                        Read(context);
                        break;

                    case "3":
                        Update(context);
                        break;

                    case "4":
                        Delete(context);
                        break;

                    case "5":
                        Console.WriteLine("...BYE....");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }

    }

    public static void Add(AppDbContext context)
    {

        Console.Write("Enter Employee Name:");
        string name = Console.ReadLine();

        Console.Write("Enter Employee Salary:");
        int salary = Convert.ToInt32(Console.ReadLine());

        var newemployee = new Employee(name, salary);

        context.Employees.Add(newemployee);
        context.SaveChanges();

        Console.WriteLine("New Employee Added");

    }

    public static void Read(AppDbContext context)
    {

        var getemployee = context.Employees.ToList();

        Console.WriteLine("Employees in the Database");

        foreach (var employee in getemployee)
        {
            Console.WriteLine($" {employee.Id} - {employee.Name},{employee.Salary}");
        }
    }


    public static void Update(AppDbContext context)
    {

        var employeeupdate = context.Employees.FirstOrDefault(employee => employee.Id == 1);

        if (employeeupdate != null)
        {
            employeeupdate.Salary = 45000;
            context.SaveChanges();
        }
    }

    public static void Delete(AppDbContext context)
    {

        var employeeupdate = context.Employees.FirstOrDefault(employee => employee.Id == 1);

        if (employeeupdate != null)
        {
            context.Employees.Remove(employeeupdate);
            context.SaveChanges();
        }
    }



}








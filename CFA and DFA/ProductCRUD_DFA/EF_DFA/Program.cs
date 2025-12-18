using EF_DFA.Models;
using Microsoft.EntityFrameworkCore;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var context = new DfaContext())
        {
            while (true)
            {
                Console.WriteLine("........CRUD OPERATIONS............");

                Console.WriteLine("1.ADD Product Details");
                Console.WriteLine("2.READ Product Details");
                Console.WriteLine("3.UPDATE Product Details");
                Console.WriteLine("4.DELETE Product Details");
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

    public static void Add(DfaContext context)
    {

        Console.Write("Enter Product Name:");
        string name = Console.ReadLine();

        Console.Write("Enter Enter Product Price:");
        int price = Convert.ToInt32(Console.ReadLine());

        var newproduct = new Product { Name=name,Price=price};

        context.Products.Add(newproduct);
        context.SaveChanges();

        Console.WriteLine("New Product Added");

    }

    public static void Read(DfaContext context)
    {

        var getproduct = context.Products.ToList();

        Console.WriteLine("Products in the Database");

        foreach (var pro in getproduct)
        {
            Console.WriteLine($" {pro.Id} - {pro.Name},{pro.Price}");
        }
    }


    public static void Update(DfaContext context)
    {
        Console.Write("Enter product id to update");
        int productid=Convert.ToInt32(Console.ReadLine());

        var productupdate = context.Products.FirstOrDefault(pro => pro.Id == productid);


        if (productupdate != null)
        {
            Console.WriteLine($"Current Product: {productupdate.Name} - ${productupdate.Price}");
            Console.WriteLine("What would you like to update?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Price");
            Console.Write("Enter choice (1-2): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter new name: ");
                    productupdate.Name = Console.ReadLine();
                    break;

                case 2:
                    Console.Write("Enter new price: ");
                    productupdate.Price = Convert.ToInt32(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            context.SaveChanges();
            Console.WriteLine("Product updated successfully!");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public static void Delete(DfaContext context)
    {
        Console.Write("Enter product id to Delete");
        int productid = Convert.ToInt32(Console.ReadLine());

        var productdelete = context.Products.FirstOrDefault(pro=> pro.Id == productid);

        if (productdelete != null)
        {
            context.Products.Remove(productdelete);
            context.SaveChanges();
            Console.WriteLine("Product Deleted successfully!");
        }
    }


}
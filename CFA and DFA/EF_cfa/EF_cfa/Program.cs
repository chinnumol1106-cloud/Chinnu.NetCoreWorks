using EF_cfa.Data;
using EF_cfa.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var context = new AppDbContext())// create object creation of dbcontext using:databasilot kerenda karyanl aanu cheyunnath so connection automatically close aakanm 
        {


            // Add a new product
            var newProduct = new Product { Name = "Cake", Price = 1500 };
            context.Products.Add(newProduct);
            context.SaveChanges();
            Console.WriteLine("New Product Added!");

            // Fetch and display all products
            var products = context.Products.ToList();
            Console.WriteLine("Products in Database:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} - {product.Name} - ${product.Price}");
            }
        }
        
    }
}
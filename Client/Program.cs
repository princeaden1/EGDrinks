using EGDrinksCore.Entities;
using System.Net.Http.Json;

class Program
{
    private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };

    static async Task Main(string[] args)
    {
        // Fetch and display all products
        await GetProductsAsync();

        // Add a new product
        await AddProductAsync(new Product { Name = "Coke", Price = 1.5M });

        // Fetch and display products again
        await GetProductsAsync();

        // Remove a product (assuming the ID is known, e.g., ID = 1)
        Console.WriteLine("Removing a product...");
        await RemoveProductAsync(1);

        // Fetch and display products one last time
        await GetProductsAsync();
    }

    private static async Task GetProductsAsync()
    {
        Console.WriteLine("\nFetching products...");
        var products = await client.GetFromJsonAsync<IEnumerable<Product>>("api/products");
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
        }
    }

    private static async Task AddProductAsync(Product product)
    {
        Console.WriteLine("\nAdding a new product...");
        var response = await client.PostAsJsonAsync("api/products", product);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Product added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add the product.");
        }
    }

    private static async Task RemoveProductAsync(int id)
    {
        Console.WriteLine($"\nRemoving product with ID {id}...");
        var response = await client.DeleteAsync($"api/products/{id}");
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Product removed successfully.");
        }
        else
        {
            Console.WriteLine("Failed to remove the product.");
        }
    }
}
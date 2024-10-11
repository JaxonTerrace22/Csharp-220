using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; private set; }
    
    public int ProductId { get; private set; }
    
    public double Price { get; private set; }
    
    public int Quantity { get; private set; }

    public Product(string name, int productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public double TotalCost()
    {
        return Price * Quantity;
    }
}

class Customer
{
    public string Name { get; private set; }
    
    public Address Address { get; private set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

class Address
{
    public string Street { get; private set; }
    
    public string City { get; private set; }
    
    public string State { get; private set; }
    
    public string Country { get; private set; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public string FullAddress()
    {
        return $"{Street}\n{City}, {State}\n{Country}";
    }
}

class Order
{
    public List<Product> Products { get; private set; }
    
    
public Customer Customer { get; private set; }

public Order(Customer customer)
    {
        Customer = customer;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public double TotalPrice()
    {
        double total = 0;
        foreach (var product in Products)
        {
            total += product.TotalCost();
        }
        double shippingCost = Customer.IsInUSA() ? 5.0 : 35.0;
        return total + shippingCost;
    }

    public string PackingLabel()
    {
        string label = "";
        foreach (var product in Products)
        {
            label += $"Item Name: {product.Name}, Item ID: {product.ProductId}\n";
        }
        return label;
    }

    public string ShippingLabel()
    {
        return $"Name: {Customer.Name}\n{Customer.Address.FullAddress()}";
    }
}

class Program
{
    static void Main()
    {
        Address address1 = new Address("5607 Terrace Ct.", "Roseville", "CA", "USA");
        
        Customer customer1 = new Customer("Peter Quill", address1);
        
        Order order1 = new Order(customer1);
        
        order1.AddProduct(new Product("Eggs", 001, 3.99, 2));
        
        order1.AddProduct(new Product("Coke Oreos", 002, 1.99, 1));

        Address address2 = new Address("5678 Lava Ct.", "Silent Hill", "CO", "USA");
        
        Customer customer2 = new Customer("Big Nate", address2);
        
        Order order2 = new Order(customer2);
        
        order2.AddProduct(new Product("Cookie Crisps", 003, 8.99, 10));
        
        order2.AddProduct(new Product("Oranges", 004, 1.57, 5));

        List<Order> orders = new List<Order> { order1, order2 };

        foreach (var order in orders)
        {
            Console.WriteLine("Packing Label:");
            
            Console.WriteLine(order.PackingLabel());
            
            Console.WriteLine("Shipping Label:");
            
            Console.WriteLine(order.ShippingLabel());
            
            Console.WriteLine($"Price: ${order.TotalPrice():F2}\n");
        }
    }
}

using System;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите задание (1-3): ");
        int task = int.Parse(Console.ReadLine());

        switch (task)
        {
            case 1:
                var productRepo = new ProductRepository();
                productRepo.Add(new Product { Id = 1, Name = "Laptop", Price = 1000m });
                productRepo.Add(new Product { Id = 2, Name = "Phone", Price = 500m });

                var customerRepo = new CustomerRepository();
                customerRepo.Add(new Customer { Id = 1, Name = "byer1", Address = "123 Main St" });
                customerRepo.Add(new Customer { Id = 2, Name = "byer2", Address = "456 Elm St" });

                Console.WriteLine("Products:");
                foreach (var product in productRepo.GetAll())
                {
                    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                }

                Console.WriteLine("Customers:");
                foreach (var customer in customerRepo.GetAll())
                {
                    Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Address: {customer.Address}");
                }
                break;

            case 2:
                Point point = new Point(3, 4);
                Rectangle rectangle = new Rectangle(2, 4);

                Point clonedPoint = point.Clone();
                Rectangle clonedRectangle = rectangle.Clone();

                Console.WriteLine($"Original Point: {point}, Cloned Point: {clonedPoint}");
                Console.WriteLine($"Original Rectangle: {rectangle}, Cloned Rectangle: {clonedRectangle}");
                break;

            case 3:
                ComplexNumber complex1 = new ComplexNumber(3, 4);
                ComplexNumber complex2 = new ComplexNumber(1, 2);

                RationalNumber rational1 = new RationalNumber(1, 2);
                RationalNumber rational2 = new RationalNumber(3, 4);

                Console.WriteLine($"Compare Complex Numbers: {complex1.Compare(complex1, complex2)}");
                Console.WriteLine($"Compare Rational Numbers: {rational1.Compare(rational1, rational2)}");
                break;

            default:
                Console.WriteLine("Invalid task number.");
                break;
        }
    }
}

public interface IEntity
{
    int Id { get; }
}

public interface IRepository<T> where T : IEntity
{
    void Add(T item);
    void Delete(T item);
    T FindById(int id);
    IEnumerable<T> GetAll();
}

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

public class ProductRepository : IRepository<Product>
{
    private List<Product> _products = new List<Product>();

    public void Add(Product item) => _products.Add(item);

    public void Delete(Product item) => _products.Remove(item);

    public Product FindById(int id) => _products.Find(p => p.Id == id);

    public IEnumerable<Product> GetAll() => _products;
}

public class CustomerRepository : IRepository<Customer>
{
    private List<Customer> _customers = new List<Customer>();

    public void Add(Customer item) => _customers.Add(item);

    public void Delete(Customer item) => _customers.Remove(item);

    public Customer FindById(int id) => _customers.Find(c => c.Id == id);

    public IEnumerable<Customer> GetAll() => _customers;
}

public interface IClonable<T> where T : IClonable<T>
{
    T Clone();
}

public class Point : IClonable<Point>
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(Point other) : this(other.X, other.Y) { }

    public Point Clone() => new Point(this);

    public override string ToString() => $"Point(X: {X}, Y: {Y})";
}

public class Rectangle : IClonable<Rectangle>
{
    public int Width { get; }
    public int Height { get; }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Rectangle(Rectangle other) : this(other.Width, other.Height) { }

    public Rectangle Clone() => new Rectangle(this);

    public override string ToString() => $"Rectangle(Width: {Width}, Height: {Height})";
}

public interface IComparer<T> where T : struct
{
    int Compare(T x, T y);
}

public struct ComplexNumber : IComparer<ComplexNumber>
{
    public double Real { get; }
    public double Imaginary { get; }

    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public int Compare(ComplexNumber x, ComplexNumber y)
    {
        double magnitudeX = Math.Sqrt(x.Real * x.Real + x.Imaginary * x.Imaginary);
        double magnitudeY = Math.Sqrt(y.Real * y.Real + y.Imaginary * y.Imaginary);
        return magnitudeX.CompareTo(magnitudeY);
    }

    public override string ToString() => $"ComplexNumber(Real: {Real}, Imaginary: {Imaginary})";
}

public struct RationalNumber : IComparer<RationalNumber>
{
    public int Numerator { get; }
    public int Denominator { get; }

    public RationalNumber(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public int Compare(RationalNumber x, RationalNumber y)
    {
        double valueX = (double)x.Numerator / x.Denominator;
        double valueY = (double)y.Numerator / y.Denominator;
        return valueX.CompareTo(valueY);
    }

    public override string ToString() => $"RationalNumber(Numerator: {Numerator}, Denominator: {Denominator})";
}



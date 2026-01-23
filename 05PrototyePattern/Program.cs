using Mapster;

Console.WriteLine("Prototype Pattern");

var product1 = new Product();
product1.Name = "Masaüstü";
product1.Price = 500;

Product product2 = product1.Adapt<Product>();  //product1.Clone();
product2.Name = "Laptop";

//product1.Adapt<Product>() yeni product oluştur ve product 1 değerlerini ona kopyala
//product1.Adapt(product2); product 1'i güncelle ve product2 değerlerini ona kopyala

Console.ReadLine();
interface IClone<T>
    where T : class
{
    T Clone();
}
class Product : IClone<Product>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }

    public Product Clone()
    {
        return new Product { Name = Name, Price = Price };
    }
}
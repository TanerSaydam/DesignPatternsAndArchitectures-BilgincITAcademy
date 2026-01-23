var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

class CustomerService
{
    public void Create(string customerName)
    {
        //customer oluştur

        //outbox yapısını oluştur
        CustomerOutbox customerOutbox = new() { CustomerName = customerName };
        //db ye kayıt et
        //işlemi sonlandır
    }
}

class CustomerOutbox
{
    public string CustomerName { get; set; } = default!;
    public bool IsCompleted { get; set; }
}
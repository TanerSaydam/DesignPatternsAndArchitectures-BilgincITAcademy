using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Abstract Factory Pattern");

#region Problem
//ProductService productService = new();
//productService.Create("Bilgisayar");
#endregion

#region Old Solution
//ProductAbstractFactory productAbstractFactory = new(DbTypeEnum.MySql);
//productAbstractFactory._productService.Create("Bilgisayar");
#endregion

#region DI Solution
ServiceCollection services = new();
//services.AddTransient((factory) =>
//{
//    IDbContext _dbContext = new MySqlDbContext();
//    ProductRepository productRepository = new(_dbContext);
//    UnitOfWork unitOfWork = new(_dbContext);
//    return new ProductService(productRepository, unitOfWork);
//});
services.AddKeyedTransient(DbTypeEnum.MySql, (factory, key) =>
{
    IDbContext _dbContext = new MySqlDbContext();
    ProductRepository productRepository = new(_dbContext);
    UnitOfWork unitOfWork = new(_dbContext);
    return new ProductService(productRepository, unitOfWork);
});

services.AddKeyedTransient(DbTypeEnum.PostgreSql, (factory, key) =>
{
    IDbContext _dbContext = new PostgreSqlDbContext();
    ProductRepository productRepository = new(_dbContext);
    UnitOfWork unitOfWork = new(_dbContext);
    return new ProductService(productRepository, unitOfWork);
});

services.AddKeyedTransient<ProductAbstractFactory>(DbTypeEnum.PostgreSql);


var srv = services.BuildServiceProvider();
var productService = srv.GetRequiredKeyedService<ProductService>(DbTypeEnum.MySql);
productService.Create("Bilgisayar");
#endregion

#region Setup
interface IDbContext
{
    void Connection();
    void Add(string productName);
    void SaveChanges();
}
class MySqlDbContext : IDbContext
{
    public MySqlDbContext()
    {
        Connection();
    }

    public void Connection() { }

    public void Add(string productName) { }
    public void SaveChanges() { }
}

class PostgreSqlDbContext : IDbContext
{
    public PostgreSqlDbContext()
    {
        Connection();
    }

    public void Connection() { }

    public void Add(string productName) { }
    public void SaveChanges() { }
}

class ProductRepository(IDbContext dbContext)
{
    public void Create(string productName)
    {
        dbContext.Add(productName);
    }
}

class UnitOfWork(IDbContext dbContext)
{
    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}

class ProductService(
    ProductRepository _productRepository,
    UnitOfWork _unitOfWork
    )
{
    public void Create(string productName)
    {
        //validation kontrol

        _productRepository.Create(productName);
        _unitOfWork.SaveChanges();
    }
}

enum DbTypeEnum
{
    MySql,
    PostgreSql
}
#endregion

#region Old Solution
class ProductAbstractFactory
{
    public readonly ProductService _productService;
    private readonly IDbContext _dbContext;
    public ProductAbstractFactory(DbTypeEnum dbType)
    {
        switch (dbType)
        {
            case DbTypeEnum.MySql:
                _dbContext = new MySqlDbContext();
                break;
            case DbTypeEnum.PostgreSql:
                _dbContext = new PostgreSqlDbContext();
                break;
            default:
                throw new ArgumentException("Geçersiz db seçimi");
        }

        ProductRepository productRepository = new(_dbContext);
        UnitOfWork unitOfWork = new(_dbContext);
        _productService = new(productRepository, unitOfWork);
    }
}
#endregion

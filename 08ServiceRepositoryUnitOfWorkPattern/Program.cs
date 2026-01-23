using _08ServiceRepositoryUnitOfWorkPattern.Context;
using _08ServiceRepositoryUnitOfWorkPattern.Dtos;
using _08ServiceRepositoryUnitOfWorkPattern.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("MyDb"));
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ProductRepository>();

var app = builder.Build();

app.MapPost("/product-create", async (
    CreateProductDto request,
    ProductService productService,
    CancellationToken cancellationToken) =>
{
    var res = await productService.Create(request, cancellationToken);
    if (!res) return Results.BadRequest(new { Error = "Geçerli bir ürün adı girin" });

    return Results.Ok(new { Message = "Ürün başarıyla kaydedildi" });
});

app.MapGet("/product-getall", async (ProductRepository productRepository, CancellationToken cancellationToken) =>
{
    //var res = productRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);
    var res = await productRepository.GetAllAsync(cancellationToken);
    return res;
});

app.Run();

//public interface IProductService
//{
//    Task<bool> Create(CreateProductDto request, CancellationToken cancellationToken = default);
//}

public class ProductService(ProductRepository productRepository, ApplicationDbContext dbContext, UnitOfWork unitOfWork) //: IProductService //.NET8 //Service Pattern
{
    public async Task<bool> Create(CreateProductDto request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            return false;
        } //business rules

        Product product = request.Adapt<Product>();

        //productRepository.Create(product);
        //await unitOfWork.SaveChangesAsync(cancellationToken);

        dbContext.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class Repository<T>(ApplicationDbContext dbContext)
    where T : class
{
    public void Create(T entity)
    {
        dbContext.Add(entity);
    }

    public IQueryable<T> GetAll()
    {
        return dbContext.Set<T>().AsQueryable();
    }
}
public class ProductGenericRepository : Repository<Product>//generic repository
{
    public ProductGenericRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
    //public void Create(Product product)
    //{
    //    dbContext.Products.Add(product);
    //}
}

public class ProductRepository(ApplicationDbContext dbContext)//normal repository
{
    public void Create(Product product)
    {
        dbContext.Products.Add(product);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products.OrderBy(p => p.Name).ToListAsync(cancellationToken);
    }
}
public class UnitOfWork(ApplicationDbContext dbContext)
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

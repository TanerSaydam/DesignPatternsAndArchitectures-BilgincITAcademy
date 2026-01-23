using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ProductService>();

Test test = new();
Console.WriteLine(test.ToString());

var app = builder.Build();

app.MapGet("/success", () =>
{
    var res = Result<string>.Success("Başarılı");
    return Results.Ok(res);
});

app.MapGet("/fail", () =>
{
    var res = Result<string>.Failure("Something went wrong");
    return Results.Ok(res);
});


app.MapGet("/list", (ProductService productService) =>
{
    var res = productService.GetAll();
    return Results.Ok(res);
});

app.MapGet("/my-exception", () =>
{
    try
    {
        throw new ArgumentException("My exception");
    }
    catch (Exception ex)
    {
        var res = Result<string>.Failure(ex.Message);
        return Results.BadRequest(res);
    }

    return Results.Ok();
});

app.MapGet("/app-exception", () =>
{

    try
    {
        var x = 0;
        var y = 0;
        var a = x / y;
        return Results.Ok(a);
    }
    catch (Exception ex)
    {
        var res = Result<string>.Failure(ex.Message);
        return Results.BadRequest(res);
    }
});

app.Run();

class Result<T>
{
    public T? Data { get; set; }
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }

    private Result(T data)
    {
        Data = data;
        IsSuccessful = true;
    }

    private Result(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = false;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(errorMessage);
    }

    public static implicit operator Result<T>(T data)
    {
        return new Result<T>(data);
    }

}

class ProductService
{
    public Result<List<string>> GetAll()
    {
        var list = new List<string>() { "Name1", "Name2" };
        //return Result<List<string>>.Success(list);
        return list;
    }
}

class Test
{
    public string Name { get; set; } = default!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
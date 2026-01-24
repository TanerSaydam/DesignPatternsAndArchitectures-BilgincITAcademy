namespace CleanArchitecture.WebAPI;

public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; set; } = default!;
    public static void AddServiceTool(this IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
    }
}

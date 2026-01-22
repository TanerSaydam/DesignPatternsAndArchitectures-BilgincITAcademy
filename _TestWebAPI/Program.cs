using Microsoft.AspNetCore.Mvc;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

var builder = WebApplication.CreateBuilder(args);
#region Vault Example
//var con = builder.Configuration.GetSection("SqlConnection").Value;

//VaultService vaultService = new();
//await vaultService.GetSecrets();
//var con = VaultService.Datas["SqlConnection"];
//Console.WriteLine(con);

//secret manager
#endregion

#region Factory Pattern Service Registration
builder.Services.AddTransient<INotification, SmsNotificationSystem>();
builder.Services.AddKeyedTransient<INotification, SmsNotificationSystem>("sms");
builder.Services.AddKeyedTransient<INotification, EmailNotificationSystem>("email");
#endregion

builder.Services.AddControllers();

#region Singleton Example
builder.Services.AddSingleton<DIClass>();
#endregion

var app = builder.Build();

#region Singleton Example
app.MapGet("/singleton", ([FromServices] DIClass diclass) =>
{
    return "Hello World!";
});
#endregion

#region Factory Example
app.MapGet("/factory", ([FromKeyedServices("email")] INotification notification) =>
{
    notification.Send("Hello world");
    return "Hello World!";
});
#endregion

app.MapControllers();

app.Run();

#region Vault Example
//class Test(IConfiguration configuration)
//{

//}

public class VaultService
{
    public static Dictionary<string, object> Datas = new();
    public async Task GetSecrets()
    {
        var vaultToken = "root";
        var vaultUri = "http://127.0.0.1:8200";
        var vaultTokenInfo = new TokenAuthMethodInfo(vaultToken);
        var vaultClientSettings = new VaultClientSettings(vaultUri, vaultTokenInfo);
        var vaultClient = new VaultClient(vaultClientSettings);

        var secrets = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(
            path: "mysecrets",
            mountPoint: "secret");

        Datas = secrets.Data.Data.ToDictionary();
    }
}

#endregion

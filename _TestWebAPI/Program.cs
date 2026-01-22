using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

var builder = WebApplication.CreateBuilder(args);
//var con = builder.Configuration.GetSection("SqlConnection").Value;

VaultService vaultService = new();
await vaultService.GetSecrets();
var con = VaultService.Datas["SqlConnection"];
Console.WriteLine(con);

//secret manager

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

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

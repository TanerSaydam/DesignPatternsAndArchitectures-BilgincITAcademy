var builder = WebApplication.CreateBuilder(args);


//Service Register // DI 
//ServiceCollection
//builder.Services.AddTransient<Test>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
// ServiceCollection => hangi classın hangi yaşam süresince hayatta kalacağına karar verdiğimiz kayıt dosyası
// Service Provider => Service Collection üzerinde kayıtlı classları kayıtlı yaşam süreleri ile instance türeten yapı


var app = builder.Build(); //uygulama instance

//var srv = builder.Services.BuildServiceProvider();

//var test = app.Services.GetRequiredService<Test>();

app.UseAuthentication();
app.UseAuthorization();
app.Use((context, next) =>
{
    //kendi kontrollerim
    return next(context);
});
//Middleware

app.Run();

//class Test(IHttpClientFactory httpClient)
//{
//    // string Name { get; set; }
//    //private readonly string _msg;
//    //public Test(string msg) //inject
//    //{
//    //    msg = msg;
//    //}
//    public void Write() //dependency
//    {
//        //httpClient.CreateClient();
//        ////HttpClient httpClient1 = new();
//        //httpClient.GetAsync("");
//        //_msg = "asdasd";
//        Console.WriteLine(msg);
//    }
//}
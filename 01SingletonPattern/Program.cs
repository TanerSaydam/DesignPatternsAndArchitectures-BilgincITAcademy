using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Singleton Pattern");

//var singletonClass1 = SingletonClass.Instance;
//var singletonClass2 = SingletonClass.Instance;
//var singletonClass3 = SingletonClass.Instance;
//var singletonClass4 = SingletonClass.Instance;
//var singletonClass5 = SingletonClass.Instance;
//singletonClass1.TCNoKontrol("111");

//StaticClass.TCNoKontrol("111");

//string tcno = "111";
//tcno.TCNoKontrol();


ServiceCollection services = new();
services.AddSingleton<DIClass>();

//DIClass diclass = new();//dependency injection yapıldığında newlenebiliyor

var srv = services.BuildServiceProvider();

var diClass1 = srv.GetRequiredService<DIClass>();
var diClass2 = srv.GetRequiredService<DIClass>();
var diClass3 = srv.GetRequiredService<DIClass>();
var diClass4 = srv.GetRequiredService<DIClass>();
var diClass5 = srv.GetRequiredService<DIClass>();
var diClass6 = srv.GetRequiredService<DIClass>();

diClass1.TCNoKontrol("111");


#region Problem and Old Solution
class SingletonClass
{
    private static object _lock = new();
    //private static SingletonClass? _instance;
    public static SingletonClass Instance
    {
        get
        {
            lock (_lock)
            {
                //if (_instance is null)
                //{
                //    _instance = new SingletonClass();
                //}
                //return _instance;

                if (field is null) //.NET 10 çözümü
                {
                    field = new SingletonClass();
                }
                return field;

            }
        }
    }
    private SingletonClass() { }
    public bool TCNoKontrol(string tcNo)
    {
        return true;
    }
}
#endregion

#region Static Solution
static class StaticClass
{
    public static bool TCNoKontrol(string tcNo)
    {
        return true;
    }
}
#endregion

#region Extensions Solution
static class ExtensionClass
{
    public static bool TCNoKontrol(this string tcNo)
    {
        return true;
    }

    //extension(string tcNo) //.NET 10 çözümü
    //{
    //    public static bool TCNoKontrol()
    //    {
    //        Console.WriteLine(tcNo);
    //        return true;
    //    }

    //    public static bool TCNoKontrol()
    //    {
    //        Console.WriteLine(tcNo);
    //        return true;
    //    }

    //    public static bool TCNoKontrol()
    //    {
    //        Console.WriteLine(tcNo);
    //        return true;
    //    }
    //}
}
#endregion

#region DI Solution
class DIClass
{

    public DIClass()
    {
        Console.WriteLine("Instance created...");
    }
    public bool TCNoKontrol(string tcNo)
    {
        return true;
    }
}
#endregion
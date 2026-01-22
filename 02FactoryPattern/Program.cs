using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Factory Pattern");

#region Problem
//EmailNotificationSystem emailNotification = new();
//emailNotification.Send("Hello world");

//SmsNotificationSystem smsNotification = new();
//smsNotification.Send("Hello world");
#endregion

#region Old Solution
//NotificationFactory notificationFactory = new("email");
//INotification notification = NotificationFactory.Create("sms");
//notification.Send("Hello world");
#endregion

ServiceCollection services = new();
services.AddTransient<INotification, EmailNotificationSystem>();
services.AddKeyedTransient<INotification, SmsNotificationSystem>("sms");
services.AddKeyedTransient<INotification, EmailNotificationSystem>("email");

var srv = services.BuildServiceProvider();
var notification1 = srv.GetRequiredService<INotification>();
var notification2 = srv.GetRequiredKeyedService<INotification>("sms");

notification1.Send("Hello world");
notification2.Send("Hello world");

#region Setup
interface INotification
{
    void Send(string msg);
}
class EmailNotificationSystem : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Email sending... {0}", msg);
    }
}

class SmsNotificationSystem : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Sms sending... {0}", msg);
    }
}
#endregion

#region Old Solution
static class NotificationFactory
{
    //private INotification _notification;
    //private NotificationFactory(string type)
    //{
    //    switch (type)
    //    {
    //        case "email":
    //            _notification = new EmailNotificationSystem();
    //            break;
    //        case "sms":
    //            _notification = new SmsNotificationSystem();
    //            break;
    //        default:
    //            throw new ArgumentException("Geçersiz notification type girdin!");
    //    }

    //    //if (type == "email")
    //    //{
    //    //    _notification = new EmailNotificationSystem();
    //    //}
    //    //else if (type == "sms")
    //    //{
    //    //    _notification = new SmsNotificationSystem();
    //    //}
    //    //else
    //    //{
    //    //    throw new ArgumentException("Geçersiz notification type girdin!");
    //    //}
    //}

    public static INotification Create(string type)
    {
        switch (type)
        {
            case "email":
                return new EmailNotificationSystem();
            case "sms":
                return new SmsNotificationSystem();
            default:
                throw new ArgumentException("Geçersiz notification type girdin!");
        }
    }

}

#endregion
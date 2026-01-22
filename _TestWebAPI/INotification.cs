public interface INotification
{
    void Send(string msg);
}


public class EmailNotificationSystem : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Email sending... {0}", msg);
    }
}

public class SmsNotificationSystem : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Sms sending... {0}", msg);
    }
}
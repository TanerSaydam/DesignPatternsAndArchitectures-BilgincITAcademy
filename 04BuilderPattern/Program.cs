Console.WriteLine("Builder Pattern");

//EmailService emailService1 = new("tanersaydam@gmail.com", "info@mybusiness.com", "Hello", "Hello, how are you?", []);
EmailService emailService1 = new();
emailService1
    .To("tanersaydam@gmail.com")
    .From("info@mybusiness.com")
    .Subject("Hello")
    .Body("Hello, how are you?")
    .Send();

//EmailService emailService2 = new("tanersaydam@gmail.com", "info@mybusiness.com", "");
//EmailService emailService2 = new();
EmailService emailService2 = new();
emailService2
    .To("tanersaydam@gmail.com")
    .From("info@mybusiness.com")
    .Subject("Hello")
    .Send();

emailService2.Send();

class EmailService
{
    private string _to = string.Empty;
    private string _from = string.Empty;
    private string _subject = string.Empty;
    private string _body = string.Empty;
    private string[]? _attachments;
    public EmailService To(string to)
    {
        _to = to;
        return this;
    }

    public EmailService From(string from)
    {
        _from = from;
        return this;
    }

    public EmailService Subject(string subject)
    {
        _subject = subject;
        return this;
    }

    public EmailService Body(string body)
    {
        _body = body;
        return this;
    }

    public EmailService Attachements(string[] attachments)
    {
        _attachments = attachments;
        return this;
    }

    public void Send()
    {
        Console.WriteLine(
            @"
            I sending email...
            To: {0}
            From: {1}
            Subject: {2}
            Body: {3}
            Attacment Count: {4}"
            , _to, _from, _subject, _body, _attachments?.Count());
    }
}
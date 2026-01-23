Console.WriteLine("Facade Pattern");

string productName = "Bilgisayar";
FacadeOrder facadeOrder = new();
facadeOrder.CreateOrder(productName, "Taner Saydam", "Kayseri", "111");

#region Setup
class ProductSystem
{
    public bool IsProductHave(string productName)
    {
        return true;
    }
}

class PaymentSystem
{
    public bool IsPaymentSuccessful(string cardNumber)
    {
        return true;
    }
}

class OrderSystem
{
    public bool IsOrderSuccessful(string productName, string customerName)
    {
        return true;
    }
}

class DeliverySystem
{
    public bool IsDelivered(string productName, string customerName, string address)
    {
        return true;
    }
}

#endregion

#region Solution
class FacadeOrder
{
    public void CreateOrder(string productName, string customerName, string address, string cardNumber)
    {
        ProductSystem productSystem = new();
        var productRes = productSystem.IsProductHave(productName);
        if (productRes)
        {
            PaymentSystem paymentSystem = new();
            var paymentRes = paymentSystem.IsPaymentSuccessful(cardNumber);
            if (paymentRes)
            {
                OrderSystem orderSystem = new();
                var orderRes = orderSystem.IsOrderSuccessful(productName, customerName);
                if (orderRes)
                {
                    DeliverySystem deliverySystem = new();
                    var deliveryRes = deliverySystem.IsDelivered(productName, customerName, address);
                    if (deliveryRes)
                    {
                        Console.WriteLine("Order is delivered successfuly");
                    }
                    else
                    {
                        throw new ArgumentException("Something went wrong");
                    }
                }
                else
                {
                    throw new ArgumentException("Something went wrong");
                }
            }
            else
            {
                throw new ArgumentException("Something went wrong");
            }
        }
        else
        {
            throw new ArgumentException("Something went wrong");
        }
    }
}
#endregion
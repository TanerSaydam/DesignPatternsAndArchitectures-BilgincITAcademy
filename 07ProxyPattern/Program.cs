Console.WriteLine("Proxy Pattern");

ProductProxy productProxy = new();
productProxy.Create("Bilgisayar");

class ProductSystem
{
    public void Create(string productName)
    {
        //db ye kayıt
    }
}

class ProductProxy
{
    //[ValidationAttribute] //Cross-Cutting Concerns
    public void Create(string productName)
    {
        //business rules => validation, log, cachle, authentication, authorization vb. 
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentNullException(nameof(productName));
        } //validation

        ProductSystem productSystem = new();
        productSystem.Create(productName);
    }
}
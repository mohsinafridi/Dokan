namespace Dokan.Service.Basket.Exceptions;

public class BasetNotFoundException : NotFoundException
{
    public BasetNotFoundException(string userName) : base($"Basket {userName}")
    {
    }
}    

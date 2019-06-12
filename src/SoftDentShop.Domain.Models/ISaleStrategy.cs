namespace SoftDentShop.Domain.Models
{
    public interface ISaleStrategy
    {
        decimal Calculate(decimal basePrice, CustomerAccountType accoutType);
    }
}
namespace SoftDentShop.Domain.Models.Shop
{
    public interface ISaleStrategy
    {
        decimal Calculate(decimal basePrice, CustomerAccountType accoutType);
    }
}
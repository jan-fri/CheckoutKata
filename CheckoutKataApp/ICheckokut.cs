namespace CheckoutKataApp
{
    public interface ICheckokut
    {
        void Scan(string item);
        int GetTotalPrice(Dictionary<string, int>? newItemPrices, Dictionary<string, SpecialPrice>? newSpecialPrice);
    }
}
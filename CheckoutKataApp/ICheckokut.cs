namespace CheckoutKataApp
{
    public interface ICheckokut
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
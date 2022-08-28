namespace CheckoutKataApp
{
    public class Checkout : ICheckokut
    {
        private int _totalPrice = 0;
        private List<string> _scannedItems = new List<string>();
        private Dictionary<string, int> _itemsPriceDictionary = new Dictionary<string, int>
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };

        public void Scan(string item)
        {
            _scannedItems.Add(item);
        }

        public int GetTotalPrice()
        {
            foreach (var item in _scannedItems)
            {
                _itemsPriceDictionary.TryGetValue(item, out int itemPrice);
                _totalPrice += itemPrice;
            }

            return _totalPrice;
        }
    }
}

namespace CheckoutKataApp
{
    public class Checkout : ICheckokut
    {
        private int _totalPrice = 0;
        private Dictionary<string, int> _itemsAmountRegister = new Dictionary<string, int>();
        private Dictionary<string, int> _itemsPriceDictionary = new Dictionary<string, int>
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };
        private Dictionary<string, SpecialPrice> _specialPriceDictionary = new Dictionary<string, SpecialPrice>
        {
            {"A", new SpecialPrice { MinItemsToDiscount = 3, ReducedPrice = 130 } },
            {"B", new SpecialPrice { MinItemsToDiscount = 2, ReducedPrice = 45 } },
        };

        public void Scan(string item)
        {
            if (!_itemsAmountRegister.ContainsKey(item))
            {
                _itemsAmountRegister.Add(item, 1);
            }
            else
            {
                _itemsAmountRegister[item]++;
            }        
        }

        public int GetTotalPrice(Dictionary<string, int>? itemPricesDict, Dictionary<string, SpecialPrice>? specialPriceDict)
        {
            CheckIfPricingRulesChanged(itemPricesDict, specialPriceDict);

            foreach (var item in _itemsAmountRegister)
            {
                var itemName = item.Key;
                var itemAmount = item.Value;

                _itemsPriceDictionary.TryGetValue(itemName, out int regularItemPrice);
                if (_specialPriceDictionary.TryGetValue(itemName, out SpecialPrice specialPrice))
                {
                    var multibuyReduced = 0;
                    if (itemAmount >= specialPrice.MinItemsToDiscount)
                    {
                        multibuyReduced = itemAmount / specialPrice.MinItemsToDiscount;
                    }

                    var multibuyRegular = itemAmount % specialPrice.MinItemsToDiscount;

                    _totalPrice += multibuyReduced * specialPrice.ReducedPrice + multibuyRegular * regularItemPrice;
                }
                else
                {
                    _totalPrice += itemAmount * regularItemPrice;
                }
            }

            return _totalPrice;
        }

        private void CheckIfPricingRulesChanged(Dictionary<string, int>? newItemPrices, Dictionary<string, SpecialPrice>? newSpecialPriceDict)
        {
            if (newItemPrices != null)
            {
                _itemsPriceDictionary = new Dictionary<string, int>(newItemPrices);
            }

            if (newSpecialPriceDict != null)
            {
                _specialPriceDictionary = new Dictionary<string, SpecialPrice>(newSpecialPriceDict);
            }
        }
    }
}

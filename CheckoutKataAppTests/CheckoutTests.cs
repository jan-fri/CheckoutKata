using CheckoutKataApp;

namespace CheckoutKataAppTests
{
    public class CheckoutTests
    {
        [TestCase("", 0)]
        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void ScanItem_ShouldReturnExpectedTotalPrice(string item, int expectedPrice)
        {
            //Arrange
            var checkout = new Checkout();

            //Act
            checkout.Scan(item);

            //Assert
            Assert.That(checkout.GetTotalPrice(null, null), Is.EqualTo(expectedPrice));
        }

        [TestCase("AB", 80)]
        [TestCase("CCC", 60)]
        [TestCase("DDD", 45)]
        [TestCase("CBDA", 115)]
        [TestCase("ABCD", 115)]
        [TestCase("DCBA", 115)]
        public void ScanMultipleItems_ShouldReturnExpectedTotalPrice(string itemList, int expectedPrice)
        {
            //Arrange
            var checkout = new Checkout();

            //Act
            foreach (var item in itemList)
            {
                checkout.Scan(item.ToString());
            }

            //Assert
            Assert.That(checkout.GetTotalPrice(null, null), Is.EqualTo(expectedPrice));
        }

        [TestCase("AAA", 130)]
        [TestCase("BB", 45)]
        [TestCase("AAAAAA", 260)]
        [TestCase("AAAA", 180)]
        [TestCase("BBB", 75)]
        [TestCase("BAB", 95)]
        [TestCase("ABCABACCDAB", 330)] // 4*A + 3*B + 4*C + 1*D = 180 + 75 + 60 + 15 = 330
        public void ScanMultipleItems_WhenItemHasSpecialPrice_ThenReturnSpecialPrice(string itemList, int expectedPrice)
        {
            //Arrange
            var checkout = new Checkout();

            //Act
            foreach (var item in itemList)
            {
                checkout.Scan(item.ToString());
            }

            //Assert
            Assert.That(checkout.GetTotalPrice(null, null), Is.EqualTo(expectedPrice));
        }

        [TestCase("AABBB", 145)]
        public void CalculateTotalPriceUsingNewPricingRules(string itemList, int expectedPrice)
        {
            //Arrange
            var checkout = new Checkout();
            var newPricing = new Dictionary<string, int>
            {
                { "A", 60 },
                { "B", 20 },
            };

            var newSpecialPricing = new Dictionary<string, SpecialPrice>
            {
                {"A", new SpecialPrice { MinItemsToDiscount = 2, ReducedPrice = 100 } },
                {"B", new SpecialPrice { MinItemsToDiscount = 3, ReducedPrice = 45 } },
            };

            //Act
            foreach (var item in itemList)
            {
                checkout.Scan(item.ToString());
            }

            //Assert
            Assert.That(checkout.GetTotalPrice(newPricing, newSpecialPricing), Is.EqualTo(expectedPrice));
        }
    }
}
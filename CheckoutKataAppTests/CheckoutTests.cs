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
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedPrice));
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
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedPrice));
        }

        [TestCase("AAA", 130)]
        [TestCase("BB", 45)]
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
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedPrice));
        }
    }
}
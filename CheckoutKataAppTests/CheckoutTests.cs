using CheckoutKataApp;

namespace CheckoutKataAppTests
{
    public class CheckoutTests
    {
        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void ScanItem_ShouldReturnTotalPrice(string item, int expectedPrice)
        {
            //Arrange
            var checkout = new Checkout();

            //Act
            checkout.Scan(item);

            //Assert
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedPrice));
        }
    }
}
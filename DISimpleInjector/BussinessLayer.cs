namespace DISimpleInjector
{
    public class BussinessLayer
    {
        private ICart _iCart;

        public BussinessLayer(ICart iCart)
        {
            _iCart = iCart;
        }

        public void InsertToCart()
        {
            _iCart.AddToCart();
        }
    }
}
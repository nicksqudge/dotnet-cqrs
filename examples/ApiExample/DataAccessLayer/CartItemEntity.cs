namespace ApiExample.DataAccessLayer
{
    public class CartItemEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public double Price { get; set; }
        public double SubPrice { get; set; }

        public void UpdateSubPrice()
        {
            SubPrice = Price * Quantity;
        }
    }
}

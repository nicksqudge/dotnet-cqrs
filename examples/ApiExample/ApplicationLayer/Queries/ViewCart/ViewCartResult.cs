namespace ApiExample.ApplicationLayer.Queries.ViewCart
{
    public class ViewCartResult
    {
        public class CartItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public string ItemPrice { get; set; }
            public string SubTotal { get; set; }
        }

        public IReadOnlyList<CartItem> Items { get; set; }
        public string Total { get; set; }
    }
}

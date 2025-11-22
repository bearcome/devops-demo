namespace Domain
{
      public class Order
      {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }

            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
      }
      public class OrderItem
      {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
      }
}

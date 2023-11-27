namespace Book_Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int Checkout { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }

    }
}

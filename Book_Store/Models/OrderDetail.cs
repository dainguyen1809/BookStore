namespace Book_Store.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

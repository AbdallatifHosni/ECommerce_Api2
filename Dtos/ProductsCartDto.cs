namespace ECommerce_Api2.Dtos
{
    public  class ProductsCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get;set; }
        public string  Image { get; set; }
    }
}

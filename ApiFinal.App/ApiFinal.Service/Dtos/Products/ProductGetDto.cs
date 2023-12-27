namespace ApiFinal.Service.Dtos.Products
{
    public record ProductGetDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

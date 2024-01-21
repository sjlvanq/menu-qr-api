namespace MenuApi.Models
{
    public class Supply
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Product>? Products { get; set; } = [];
    }
}

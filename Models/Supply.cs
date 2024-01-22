using System.Text.Json.Serialization;
namespace MenuApi.Models
{
    public class Supply
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}

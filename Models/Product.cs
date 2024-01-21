using System.Text.Json.Serialization;
namespace MenuApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsCombo { get; set; } = false;

        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        public List<Supply>? Supplies { get; } = [];
    }
}

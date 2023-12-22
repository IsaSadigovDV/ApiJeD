using System.ComponentModel.DataAnnotations;

namespace ApiFinal.App.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
    }
}

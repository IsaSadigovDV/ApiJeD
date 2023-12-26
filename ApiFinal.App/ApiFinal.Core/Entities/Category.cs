using System.ComponentModel.DataAnnotations;

namespace ApiFinal.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}

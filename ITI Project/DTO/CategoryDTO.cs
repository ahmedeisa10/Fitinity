using System.ComponentModel.DataAnnotations;

namespace ITI_Project.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string  { get; set; }
    }
}

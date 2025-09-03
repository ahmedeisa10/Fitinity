//using ITI_Project.Models.FoodStore_mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace ITI_Project.Models
{
	public class Category
	{
		
			public int Id { get; set; }
			[Required]
			[MaxLength(40)]
			public string GenreNmae { get; set; }
			public List<Product> Products { get; set; }
		
	}
}


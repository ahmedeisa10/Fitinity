using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_Project.Models
{
	public class Stock
	{
		
		
			public int Id { get; set; }

			[Required, ForeignKey("ShoppingCart")]
			public int ShoppingCart_Id { get; set; }   
			public ShoppingCart ShoppingCart { get; set; } 

			[Required]
			public int ProductId { get; set; }   
			public Product Product { get; set; } 

			[Required]
			public int Quantity { get; set; }

			[Required]
			public double UnitPrice { get; set; }
		
	}
}

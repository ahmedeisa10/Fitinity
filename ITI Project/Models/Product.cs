using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_Project.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string? productNmae { get; set; }
		[MaxLength(200)]
		public string? Description {  get; set; }
		public string? Image { get; set; }         
		public double Price { get; set; }
		[Required]
		public int ProductId { get; set; }           
		public Category Category { get; set; }

		public Stock Stock {  get; set; } 
		public bool IsHealthy { get; set; }
		[NotMapped]
		public string productName { get; set; }        
		[NotMapped]
		public int Quantity { get; set; }

	}
}

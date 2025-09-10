using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITI_Project.Models
{
	public class Order
	{
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public string? SessionId {  get; set; }
        public string? PaymentIntentId {  get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }


        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string? Email { get; set; }
        [Required]
        public string? MobileNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PaymentMethod { get; set; }
        public bool IsPaid { get; set; }

        [Required]
        public int OrderStatusId { get; set; }//FK
        public OrderStatus OrderStatus { get; set; } //Nav Property

        public List<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string PaymentStatus => IsPaid ? "Paid" : "Not Paid";
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Web_App_Core__MVC_.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        
        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Agent is required")]
        [Display(Name = "Agent")]
        public int AgentID { get; set; }
        
        [Display(Name = "User")]
        public int? UserID { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; } = "Pending";
        
        [Display(Name = "Total Amount")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal TotalAmount { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        // Navigation properties
        [ForeignKey("AgentID")]
        public virtual Agent? Agent { get; set; }
        
        [ForeignKey("UserID")]
        public virtual User? User { get; set; }
        
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}


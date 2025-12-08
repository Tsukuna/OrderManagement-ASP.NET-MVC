using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Web_App_Core__MVC_.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Order")]
        public int OrderID { get; set; }
        
        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10000")]
        public int Quantity { get; set; }
        
        [Required]
        [Display(Name = "Unit Amount")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal UnitAmount { get; set; }
        
        [Display(Name = "Total Amount")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [NotMapped]
        public decimal TotalAmount => Quantity * UnitAmount;
        
        // Navigation properties
        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }
        
        [ForeignKey("ItemID")]
        public virtual Item? Item { get; set; }
    }
}


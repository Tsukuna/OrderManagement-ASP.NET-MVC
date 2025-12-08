using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Web_App_Core__MVC_.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(100)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Size { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        [Display(Name = "Price ($)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Display(Name = "Stock Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be positive")]
        public int StockQuantity { get; set; }
        
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}


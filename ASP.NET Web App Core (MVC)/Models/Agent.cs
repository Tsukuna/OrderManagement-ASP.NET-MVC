using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Web_App_Core__MVC_.Models
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        
        [Required(ErrorMessage = "Agent name is required")]
        [StringLength(100)]
        [Display(Name = "Agent Name")]
        public string AgentName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Address is required")]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
        
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string? Email { get; set; }
        
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<Order>? Orders { get; set; }
    }
}


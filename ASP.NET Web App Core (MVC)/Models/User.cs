using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Web_App_Core__MVC_.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        [Display(Name = "Is Locked")]
        public bool IsLocked { get; set; } = false;
        
        [StringLength(50)]
        public string Role { get; set; } = "User";
        
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [Display(Name = "Last Login")]
        public DateTime? LastLoginDate { get; set; }
        
        // Navigation property
        public virtual ICollection<Order>? Orders { get; set; }
    }
}


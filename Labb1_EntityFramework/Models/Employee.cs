using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Labb1_EntityFramework.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int EmployeeID { get; set; } = 0;

        [Required]
        [StringLength(50)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [StringLength(50)]
        public string? Role { get; set; }

        [DisplayName("Hire date")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        public virtual ICollection<LeaveList>? Leaves { get; set; } // navigation
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Labb1_EntityFramework.Models
{
    public class LeaveList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveListID { get; set; } = 0;

        // Foreign key to TypeOfLeave table
        [ForeignKey("TypeOfLeaves")]
        public int FK_LeaveTypeID { get; set; }
        public virtual TypeOfLeave? TypeOfLeaves { get; set; } // navigation

        // Foreign key to Employee table
        [ForeignKey("Employees")]
        public int FK_EmployeeID { get; set; }
        public virtual Employee? Employees { get; set; }  // navigation

        [Required]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }

        [NotMapped]
        [DisplayName("Number of days off")]
        public int NumberOfDays => (EndDate - StartDate).Days;

        [DisplayName("Registered date")]
        public DateTime RegisteredDate { get; set; }
    }
}

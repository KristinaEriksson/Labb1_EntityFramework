using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Labb1_EntityFramework.Models
{
    public class TypeOfLeave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveTypeID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Leave type")]
        public string LeaveType { get; set; }

        public virtual ICollection<LeaveList>? LeaveLists { get; set; }  // navigation
    }
}

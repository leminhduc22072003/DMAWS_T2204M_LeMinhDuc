using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_TranHung.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string EmployeeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EmployeeDOB { get; set; }

        [Required]
        [StringLength(255)]
        public string EmployeeDepartment { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}

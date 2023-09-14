using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_TranHung.Models
{
    public class ProjectEmployee
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public string Tasks { get; set; }

        public virtual Employee Employees { get; set; }

        public virtual Project Projects { get; set; }
    }
}

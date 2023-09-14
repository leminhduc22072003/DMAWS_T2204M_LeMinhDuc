using DMAWS_T2204M_TranHung.Models;

namespace DMAWS_T2204M_TranHung.DTOs
{
    public class ProjectEmployeeDTO
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public string Tasks { get; set; }

        public virtual Employee Employees { get; set; }

        public virtual Project Projects { get; set; }
    }
}

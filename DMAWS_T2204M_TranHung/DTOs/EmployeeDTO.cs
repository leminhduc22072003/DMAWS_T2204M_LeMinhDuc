using DMAWS_T2204M_TranHung.Models;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_TranHung.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime EmployeeDOB { get; set; }

        public string EmployeeDepartment { get; set; }

        public virtual ICollection<ProjectEmployeeDTO> ProjectEmployees { get; set; }
    }
}

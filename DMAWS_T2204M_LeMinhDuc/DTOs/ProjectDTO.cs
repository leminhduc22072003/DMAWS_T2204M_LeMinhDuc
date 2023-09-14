using DMAWS_T2204M_TranHung.Models;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_TranHung.DTOs
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public virtual ICollection<ProjectEmployeeDTO> ProjectEmployees { get; set; }
    }
}

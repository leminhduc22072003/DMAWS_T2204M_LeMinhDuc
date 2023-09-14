using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_TranHung.Models
{
    public class Project
    {

        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ProjectName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProjectStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ProjectEndDate { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}

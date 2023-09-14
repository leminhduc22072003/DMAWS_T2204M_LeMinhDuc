using DMAWS_T2204M_TranHung.Models;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_TranHung.ViewModels
{
    public class ProjectCreateModel
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ProjectName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProjectStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ProjectEndDate { get; set; }
    }
}

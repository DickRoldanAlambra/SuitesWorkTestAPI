using System.ComponentModel.DataAnnotations;

namespace SuiteWorksTestAPI.DTOs.Departments
{
    public class CreateDepartmentDTO
    {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

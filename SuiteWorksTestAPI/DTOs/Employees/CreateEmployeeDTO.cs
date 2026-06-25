using System.ComponentModel.DataAnnotations;

namespace SuiteWorksTestAPI.DTOs.Employees
{
    public class CreateEmployeeDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public DateTime DateHired { get; set; }
    }
}

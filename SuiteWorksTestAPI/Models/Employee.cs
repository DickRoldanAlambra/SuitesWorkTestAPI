using System.ComponentModel.DataAnnotations;

namespace SuiteWorksTestAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int PositionId { get; set; }
        public Position Positions { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime DateHired { get; set; }
    }
}

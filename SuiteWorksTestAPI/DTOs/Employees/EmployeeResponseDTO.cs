using SuiteWorksTestAPI.Models;

namespace SuiteWorksTestAPI.DTOs.Employees
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }
        public string PositionName { get; set; }

        public int DepartmentId { get; set; }

        public Department Departments { get; set; }
        public string DepartmentName { get; set; }

        public DateTime DateHired { get; set; }
    }
}

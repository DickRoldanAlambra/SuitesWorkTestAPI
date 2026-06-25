namespace SuiteWorksTestAPI.DTOs.Departments
{
    public class DepartmentResponseDTO
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

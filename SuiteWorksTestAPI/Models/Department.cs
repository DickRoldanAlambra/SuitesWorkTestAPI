using System.ComponentModel.DataAnnotations;

namespace SuiteWorksTestAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}

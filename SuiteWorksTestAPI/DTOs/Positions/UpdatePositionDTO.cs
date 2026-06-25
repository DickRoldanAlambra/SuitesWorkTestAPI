using System.ComponentModel.DataAnnotations;

namespace SuiteWorksTestAPI.DTOs.Positions
{
    public class UpdatePositionDTO
    {
        [Required]
        public string PositionName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

    }
}

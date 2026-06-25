namespace SuiteWorksTestAPI.DTOs.Positions
{
    public class PositionResponseDTO
    {
        public int Id { get; set; }

        public string PositionName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

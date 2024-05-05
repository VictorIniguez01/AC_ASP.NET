namespace AccessControl.DTOs
{
    public class VisitorDto : ICommonDto
    {
        public int VisitorId { get; set; }

        public string? VisitorName { get; set; }

        public string? VisitorLastName { get; set; }

        public int? CarId { get; set; }

        public int? HouseId { get; set; }

        public int Id => VisitorId;
    }
}

namespace AccessControl.DTOs
{
    public class AccessVisitorDto : ICommonDto
    {
        public int AccessVisitorId { get; set; }

        public DateTime? AccessVisitorEntry { get; set; }

        public int? VisitorId { get; set; }

        public bool? IsEntry { get; set; }

        public bool? IsGoingZone { get; set; }

        public int Id => AccessVisitorId;
    }
}

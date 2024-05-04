namespace AccessControl.DTOs
{
    public class AccessVisitorInsertDto
    {
        public DateTime? AccessVisitorEntry { get; set; }

        public int? HouseId { get; set; }

        public int? CarId { get; set; }

        public int? VisitorId { get; set; }

        public bool? IsEntry { get; set; }

        public bool? IsGoingZone { get; set; }
    }
}

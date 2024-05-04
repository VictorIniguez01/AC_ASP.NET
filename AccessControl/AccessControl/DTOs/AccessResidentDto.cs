namespace AccessControl.DTOs
{
    public class AccessResidentDto : ICommonDto
    {
        public int AccessResidentId { get; set; }

        public DateTime? AccessResidentEntry { get; set; }

        public int? ResidentId { get; set; }

        public int Id => AccessResidentId;
    }
}

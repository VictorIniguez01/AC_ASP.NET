namespace AccessControl.DTOs
{
    public class UserAcDto
    {
        public int UserAcId { get; set; }

        public string? UserAcName { get; set; }

        public string? UserAcPassword { get; set; }

        public string? UserAcMqttTopic { get; set; }

        public bool? UserAcIsCoto { get; set; }
    }
}

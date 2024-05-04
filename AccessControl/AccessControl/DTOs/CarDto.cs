namespace AccessControl.DTOs
{
    public class CarDto: ICommonDto
    {
        public int CarId { get; set; }

        public string? CarBrand { get; set; }

        public string? CarModel { get; set; }

        public string? CarPlate { get; set; }

        public string? CarColor { get; set; }

        public int Id => CarId;
    }
}

﻿namespace AccessControl.DTOs
{
    public class VisitorUpdateDto
    {
        public int VisitorId { get; set; }

        public string? VisitorName { get; set; }

        public string? VisitorLastName { get; set; }

        public DateTime? VisitorEntryDatetime { get; set; }

        public bool? VisitorGoingCoto { get; set; }

        public bool? VisitorIsEntry { get; set; }

        public int? HouseId { get; set; }

        public int? CarId { get; set; }
    }
}

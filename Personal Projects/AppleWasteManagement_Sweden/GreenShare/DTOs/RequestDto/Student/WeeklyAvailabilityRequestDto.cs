namespace GreenShare.DTOs.RequestDto.Student
{
    public class WeeklyAvailabilityRequestDto
    {
        public int WeekNumber { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }


    }
}

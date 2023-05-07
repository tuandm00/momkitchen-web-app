namespace momkitchen.Mapper
{
    public class SessionDto
    {
        public DateTime? CreateDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string? Status { get; set; }
    }
}

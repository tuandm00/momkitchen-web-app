﻿namespace momkitchen.Mapper
{
    public class SessionDto
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace Domain
{
    public class GetTogether
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string PassCode { get; set; }
        public bool IsCancelled { get; set; }
        public ICollection<GetTogetherAttendee> Attendees { get; set; } = new List<GetTogetherAttendee>();
    }
}

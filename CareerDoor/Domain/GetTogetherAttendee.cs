using System;

namespace Domain
{
    public class GetTogetherAttendee
    {
        public string AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid GetTogetherId { get; set; }
        public GetTogether GetTogether { get; set; }
        public bool IsHost { get; set; }
    }
}

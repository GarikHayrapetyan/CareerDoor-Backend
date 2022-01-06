using Application.Comments;
using Application.GetTogethers;
using AutoMapper;
using Domain;
using System.Linq;




using AutoMapper;
using Domain;
using Application.Profiles;
using Application.Jobs;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;

            CreateMap<GetTogether, GetTogether>();
            CreateMap<GetTogether, GetTogetherDTO>()
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
                    .FirstOrDefault(x => x.IsHost).AppUser.UserName));
            CreateMap<GetTogetherAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(d => d.Following, o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)));
            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d=>d.Country,o=>o.MapFrom(s=>s.Country))
                .ForMember(d=>d.City,o=>o.MapFrom(s=>s.City))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(d => d.Following, o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<GetTogetherAttendee, UserActivityDto>()
                  .ForMember(d => d.Id, o => o.MapFrom(s => s.GetTogether.Id))
                  .ForMember(d => d.Date, o => o.MapFrom(s => s.GetTogether.Date))
                  .ForMember(d => d.Title, o => o.MapFrom(s => s.GetTogether.Title))
                  .ForMember(d => d.Category, o => o.MapFrom(s => s.GetTogether.Description))
                  .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.GetTogether.Attendees
                  .FirstOrDefault(x => x.IsHost).AppUser.UserName));

            CreateMap<Job, Job>();
            CreateMap<Job,JobDto>()
                .ForMember(d=>d.EmployeerUsername, o=>o.MapFrom(s=>s.Candidates.FirstOrDefault(x=>x.IsEmployer).AppUser.UserName));
            CreateMap<JobCandidate, Profiles.Profile>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}

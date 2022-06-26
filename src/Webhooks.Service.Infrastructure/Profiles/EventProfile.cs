using AutoMapper;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;

namespace Webhooks.Service.Infrastructure.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventParameters, EventDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(x => x.Created, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.Updated, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => true))
                .ReverseMap();

            CreateMap<EventDto, Event>().ReverseMap();

            CreateMap<EventDto, EntityResult>().ReverseMap();

            CreateMap<Event, EntityResult>().ReverseMap();
        }
    }
}

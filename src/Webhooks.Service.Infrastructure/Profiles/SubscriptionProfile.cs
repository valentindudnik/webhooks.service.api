using AutoMapper;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;

namespace Webhooks.Service.Infrastructure.Profiles
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<SubscriptionParameters, SubscriptionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(x => x.Created, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.Updated, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => true))
                .ReverseMap();

            CreateMap<SubscriptionDto, Subscription>().ReverseMap();

            CreateMap<SubscriptionDto, EntityResult>().ReverseMap();

            CreateMap<Subscription, EntityResult>().ReverseMap();
        }
    }
}

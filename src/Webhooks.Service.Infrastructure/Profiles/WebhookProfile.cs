using AutoMapper;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;

namespace Webhooks.Service.Infrastructure.Profiles
{
    public class WebhookProfile : Profile
    {
        public WebhookProfile()
        {
            CreateMap<WebhookParameters, WebhookDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(x => x.Created, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.Updated, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => true))
                .ReverseMap();

            CreateMap<WebhookDto, Webhook>().ReverseMap();
        }
    }
}

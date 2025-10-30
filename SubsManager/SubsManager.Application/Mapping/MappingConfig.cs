using Mapster;
using SubsManager.Application.DTOs;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDto>();
            config.NewConfig<Service, ServiceDto>();
            config.NewConfig<Plan, PlanDto>().Map(dest => dest.Period, src => (int)src.Period);
            config.NewConfig<Subscription, SubscriptionDto>()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.PlanName, src => src.Plan.Name)
                .Map(dest => dest.ServiceName, src => src.Plan.Service.Name);
        }
    }
}

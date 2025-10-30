using Mapster;
using MediatR;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;

namespace SubsManager.Application.Queries
{
    public record GetServicesQuery() : IRequest<List<ServiceDto>>;
    
    public class GetServicesHandler(IServiceRepository services) : IRequestHandler<GetServicesQuery, List<ServiceDto>>
    {
        public async Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken ct)
        {
            var all = await services.GetAllAsync(ct);
            return all.Adapt<List<ServiceDto>>();
        }
    }
}

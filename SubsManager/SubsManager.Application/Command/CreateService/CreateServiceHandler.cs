using MediatR;
using Mapster;
using SubsManager.Application.Abstractions;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.UseCases
{
    public class CreateServiceHandler(IServiceRepository services, IUnitOfWork uow) : IRequestHandler<CreateServiceCommand, ServiceDto>
    {
        public async Task<ServiceDto> Handle(CreateServiceCommand r, CancellationToken ct)
        {
            var s = new Service { Name = r.Name, Description = r.Description };
            await services.AddAsync(s, ct);
            await uow.SaveChangesAsync(ct);
            return s.Adapt<ServiceDto>();
        }
    }
}

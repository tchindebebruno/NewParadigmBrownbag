using MediatR;
using SubsManager.Application.DTOs;

namespace SubsManager.Application.UseCases
{
    public record CreateServiceCommand(string Name, string? Description) : IRequest<ServiceDto>;
}

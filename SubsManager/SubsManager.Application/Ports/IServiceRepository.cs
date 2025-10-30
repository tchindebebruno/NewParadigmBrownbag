using SubsManager.Application.Abstractions;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.Ports
{
    public interface IServiceRepository : IRepository<Service> { }
}

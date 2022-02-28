
using Microsoft.EntityFrameworkCore;
using Gateways.Entities;
namespace Gateways.Persistence
{
    public interface IDataContext
    {
        DbSet<Gateway> Gateways { get; set; }
        DbSet<Peripheral> Peripherals { get; set; }
    }
}
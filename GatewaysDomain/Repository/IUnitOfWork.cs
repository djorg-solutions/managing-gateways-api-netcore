using GatewaysDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GatewaysDomain.Repository
{
    public interface IUnitOfWork
    {
        Repository<Gateway> GatewayRepository { get; }
        Repository<Device> DeviceRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}

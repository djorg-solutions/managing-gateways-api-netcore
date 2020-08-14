using GatewaysDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GatewaysDomain.Repository
{
    public class UnitOfWork: IUnitOfWork
    {

        private readonly GatewayContext _gatewaycontext;
        public UnitOfWork(GatewayContext gatewaycontext)
        {
            if (gatewaycontext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            _gatewaycontext = gatewaycontext;
        }

        Repository<Gateway> gatewayRepository;
        Repository<Device> deviceRepository;

        public Repository<Gateway> GatewayRepository
        {
            get
            {
                if (this.gatewayRepository == null)
                {
                    this.gatewayRepository = new Repository<Gateway>(this._gatewaycontext);
                }
                return this.gatewayRepository;
            }
        }

        public Repository<Device> DeviceRepository
        {
            get
            {
                if (this.deviceRepository == null)
                {
                    this.deviceRepository = new Repository<Device>(this._gatewaycontext);
                }
                return this.deviceRepository;
            }
        }

        public void SaveChanges()
        {
            this._gatewaycontext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await this._gatewaycontext.SaveChangesAsync();
        }

    }
}

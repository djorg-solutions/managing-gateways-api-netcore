using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagingGateways.ViewModels
{
    public class GatewayViewModel
    {
        public GatewayViewModel()
        {
            GatewayDevices = new HashSet<DeviceViewModel>();
        }
        public int GatewayId { get; set; }
        public string GatewaySerialNumber { get; set; }
        public string GatewayHumanReadable { get; set; }
        public string GatewayIpAddress { get; set; }
        public ICollection<DeviceViewModel> GatewayDevices { get; set; }
    }
}

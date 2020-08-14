using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagingGateways.ViewModels
{
    public class DeviceViewModel
    {
        public int DeviceId { get; set; }
        public int DeviceUID { get; set; }
        public string DeviceVendor { get; set; }
        public DateTime DeviceCreateAt { get; set; }
        public bool DeviceStatus { get; set; }
        public int DeviceGatewayId { get; set; }
    }
}
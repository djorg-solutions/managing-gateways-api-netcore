using System;
using System.Collections.Generic;
using System.Text;

namespace GatewaysDomain.Models
{
    public partial class Gateway
    {
        public Gateway()
        {
            Devices = new HashSet<Device>();
        }
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string HumanReadable { get; set; }
        public string IpAddress { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}

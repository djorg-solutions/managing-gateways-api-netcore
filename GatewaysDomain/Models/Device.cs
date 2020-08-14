using System;
using System.Collections.Generic;
using System.Text;

namespace GatewaysDomain.Models
{
    public partial class Device
    {
        public int Id { get; set; }
        public int UID { get; set; }
        public string Vendor { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Status { get; set; }
        public Gateway Gateway { get; set; }
    }
}

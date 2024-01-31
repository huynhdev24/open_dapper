using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GasStationGasType
    {
        public long GasStationGasTypeId { get; set; }
        public long? GasStationId { get; set; }
        public string GasType { get; set; }
        public GasStation GasStation { get; set; }
    }
}

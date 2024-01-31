using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAddGasStationService
    {
        Task<int> addGasStation(GasStation gasStation);
        Task<int> addGasStationGasType(GasStationGasType gasStationGasType);
    }
}

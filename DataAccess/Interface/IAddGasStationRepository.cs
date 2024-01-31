using Models;
using System;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IAddGasStationRepository : IDisposable
    {
        Task<int> addGasStation(GasStation gasStation);
        Task<int> addGasStationGasType(GasStationGasType gasStationGasType);
    }
}

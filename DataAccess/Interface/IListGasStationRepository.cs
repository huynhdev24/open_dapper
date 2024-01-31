using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IListGasStationRepository : IDisposable
    {
        Task<MDistrict> GetDistrictById(long MDistrictId);
        Task<MDistrict> getMDistrictById(long Id);
        Task<IEnumerable<MDistrict>> GetListDistrict();
        Task<IEnumerable<MType>> GetType();
        Task<IEnumerable<MType>> GetRating();
        //Task<IEnumerable<GasStation>> getGasStation(string searchGasName, string searchDistrict, string StringListGasType);
        Task<IEnumerable<GasStationViewModel>> getGasStation(string searchGasName, string searchDistrict, string StringListGasType);
        GasStation getGasStationByName(string gasStationName);
        Task<IEnumerable<MType>> GetGasTypeById(string typeId);
        Task<int> getListGasTypeIsNotNull();
        Task<string> getTypeTextFromMType(string typeCode);
        Task<GasStation> GetGasStationLast();
        Task<int> deleteGasStation(long gasStationId, long userId);
    }
}

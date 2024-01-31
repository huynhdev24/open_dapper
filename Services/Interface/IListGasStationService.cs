
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IListGasStationService
    {
        Task<IEnumerable<MDistrict>> GetListDistrict();
        Task<IEnumerable<MType>> GetType();
        Task<IEnumerable<MType>> GetRating();
        Task<IEnumerable<GasStationViewModel>> getGasStation(string searchGasName, string searchDistrict, string StringListGasType);
        Task<IEnumerable<MType>> GetGasTypeById(string typeId);
        Task<GasStation> getGasStationByName(string gasStationName);
        Task<MDistrict> getMDistrictById(long Id);
        Task<MDistrict> GetDistrictById(long MDistrictId);
        Task<GasStation> GetGasStationLast();
        Task<int> deleteGasStation(long gasStationId, long userId);
    }
}

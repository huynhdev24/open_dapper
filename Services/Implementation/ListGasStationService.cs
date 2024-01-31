using DataAccess.Implementation;
using Microsoft.Extensions.Configuration;
using Models;
using Models.ViewModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Services.Implementation
{
	/// <summary>
	/// HomeController
	/// </summary>
	/// <newpara>Author: Cam Quyen</newpara>
	/// <newpara>Date: 2021/05/25</newpara>
	public class ListGasStationService : IListGasStationService
	{
		#region Variable
		private readonly string _connectString;
		#endregion

		#region Constructor
		public ListGasStationService(IConfiguration configuration)
		{
			_connectString = configuration.GetConnectionString("DefaultConnection");
		}
		#endregion

		#region Function

		/// <summary>
		/// Get District by ID
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<MDistrict> getMDistrictById(long Id)
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.getMDistrictById(Id);
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get District by ID
		/// </summary>
		/// <param name="MDistrictId"></param>
		/// <returns></returns>
		public async Task<MDistrict> GetDistrictById(long MDistrictId)
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.GetDistrictById(MDistrictId);
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get a list District was ordered by District Name
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<MDistrict>> GetListDistrict()
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.GetListDistrict();
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get a list GasType
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<MType>> GetType()
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.GetType();
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get a list Rating
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<MType>> GetRating()
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.GetRating();
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get a list GasStation with searching
		/// </summary>
		/// <param name="searchGasName"></param>
		/// <param name="searchDistrict"></param>
		/// <param name="StringListGasType"></param>
		/// <returns></returns>
		public async Task<IEnumerable<GasStationViewModel>> getGasStation(string searchGasName, string searchDistrict, string StringListGasType)
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.getGasStation(searchGasName, searchDistrict, StringListGasType);
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get GasType by ID
		/// </summary>
		/// <param name="typeId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<MType>> GetGasTypeById(string typeId)
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.GetGasTypeById(typeId);
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get GasStation by name
		/// </summary>
		/// <param name="gasStationName"></param>
		/// <returns></returns>
		public async Task<GasStation> getGasStationByName(string gasStationName)
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = res.getGasStationByName(gasStationName);
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// get the GasStation lastest
		/// </summary>
		/// <returns></returns>
		public async Task<GasStation> GetGasStationLast()
        {
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.GetGasStationLast();
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// delete a GasStation by ID
		/// </summary>
		/// <param name="gasStationId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<int> deleteGasStation(long gasStationId, long userId)
		{
			try
			{
				using (ListGasStationRepository res = new ListGasStationRepository(_connectString))
				{
					var result = await res.deleteGasStation(gasStationId, userId);
					res.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}

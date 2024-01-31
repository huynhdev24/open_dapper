using DataAccess.Implementation;
using Microsoft.Extensions.Configuration;
using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class AddGasStationService : IAddGasStationService
    {
		#region Variable
		private readonly string _connectString;
		#endregion

		#region Constructor
		public AddGasStationService(IConfiguration configuration)
		{
			_connectString = configuration.GetConnectionString("DefaultConnection");
		}
		#endregion

		#region Function
		/// <summary>
		/// add new a GasStation
		/// </summary>
		/// <param name="gasStation"></param>
		/// <returns></returns>
		public async Task<int> addGasStation(GasStation gasStation)
		{
			try
			{
				using (AddGasStationRepository res = new AddGasStationRepository(_connectString))
				{
					var result = await res.addGasStation(gasStation);
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
		/// when add a new GasStation then add a new GasStationGasType
		/// </summary>
		/// <param name="gasStationGasType"></param>
		/// <returns></returns>
		public async Task<int> addGasStationGasType(GasStationGasType gasStationGasType)
		{
			try
			{
				using (AddGasStationRepository res = new AddGasStationRepository(_connectString))
				{
					var result = await res.addGasStationGasType(gasStationGasType);
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

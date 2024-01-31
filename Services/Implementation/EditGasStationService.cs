using Microsoft.Extensions.Configuration;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Implementation
{
    public class EditGasStationService : IEditGasStationService
    {
		#region Variable
		private readonly string _connectString;
		#endregion

		#region Constructor
		public EditGasStationService(IConfiguration configuration)
		{
			_connectString = configuration.GetConnectionString("DefaultConnection");
		}
		#endregion

		#region Function
		#endregion
	}
}

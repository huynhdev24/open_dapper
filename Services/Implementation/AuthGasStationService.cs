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
    public class AuthGasStationService : IAuthGasStationService
    {
		#region Variable
		private readonly string _connectString;
		#endregion

		#region Constructor
		public AuthGasStationService(IConfiguration configuration)
		{
			_connectString = configuration.GetConnectionString("DefaultConnection");
		}
		#endregion

		#region Function

		/// <summary>
		/// handle Login with Email, Password and role
		/// </summary>
		/// <param name="Email"></param>
		/// <param name="Password"></param>
		/// <returns></returns>
		public async Task<UserDTO> Login(string Email, string Password)
		{
			try
			{
				using (AuthGasStationRepository res = new AuthGasStationRepository(_connectString))
				{
					var result = res.Login(Email, Password);
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

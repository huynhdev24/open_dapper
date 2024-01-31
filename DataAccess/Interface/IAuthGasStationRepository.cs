using Models;
using System;

namespace DataAccess.Interface
{
    public interface IAuthGasStationRepository : IDisposable
    {
        UserDTO Login(string Email, string Password);
    }
}

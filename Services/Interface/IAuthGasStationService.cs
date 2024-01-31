using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAuthGasStationService
    {
        Task<UserDTO> Login(string Email, string Password);
    }
}

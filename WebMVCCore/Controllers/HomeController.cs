using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCCore.Controllers
{
	/// <summary>
	/// HomeController
	/// </summary>
	/// <newpara>Author: Cam Quyen</newpara>
	/// <newpara>Date: 2021/05/25</newpara>
	public class HomeController : Controller
	{
		#region Variable
		private readonly ILogger<HomeController> _logger;
		private readonly IAuthGasStationService _service;
		#endregion

		#region Constructor
		public HomeController(ILogger<HomeController> logger, IAuthGasStationService service)
		{
			_logger = logger;
			_service = service;

		}
		#endregion

		#region Function
		public IActionResult Login()
		{
			return View();
		}

		/// <summary>
		/// Login by email and password have validation datas
		/// </summary>
		/// <param name="email"></param>
		/// <param name="pass"></param>
		/// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Login(string email, string pass)
        {
            var Email = email;
            var Password = pass;
			// handle Login
            var result = await _service.Login(Email, Password);

			if (result != null && ModelState.IsValid)
			{
				// save User sessions about: UserId && Email
				HttpContext.Session.SetString("UserId", result.UserId.ToString());
				HttpContext.Session.SetString("Email", result.Email.ToString());
				return Json(Ok("ListGasStation/GasStationList"));
			}
			else
			{
				// This viewbag use to notify error
                ViewBag.Msg = "ログインに失敗しました。電子メールまたはパスワードを確認してください。";
                ViewBag.emailSaved = Email;
				ViewBag.passSaved = Password;
                return Json(BadRequest("ログインに失敗しました。電子メールまたはパスワードを確認してください。"));
            }
		}
        #endregion
    }
}

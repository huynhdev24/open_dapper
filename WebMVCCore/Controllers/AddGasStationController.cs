using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.ViewModels;
using Newtonsoft.Json;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCCore.Controllers
{
    public class AddGasStationController : Controller
    {
        #region Variable
        private readonly ILogger<AddGasStationController> _logger;
        private readonly IAddGasStationService _serviceAdd;
        private readonly IListGasStationService _serviceList;
        #endregion

        #region Constructor
        public AddGasStationController(ILogger<AddGasStationController> logger, IAddGasStationService serviceAdd, IListGasStationService serviceList)
        {
            _logger = logger;
            _serviceAdd = serviceAdd;
            _serviceList = serviceList;

        }
        #endregion

        #region Function
        [HttpGet]
        public async Task<IActionResult> GasStationAdd()
        {
            ViewBag.getType = await _serviceList.GetType();
            ViewBag.getRating = await _serviceList.GetRating();
            ViewBag.getDistrict = await _serviceList.GetListDistrict();
            return View();
        }

        /// <summary>
        /// Add GasStation using AJAX
        /// </summary>
        /// <param name="GasStationName"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <param name="District"></param>
        /// <param name="Address"></param>
        /// <param name="OpeningTime"></param>
        /// <param name="Rating"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GasStationAdd(string GasStationName,
            string JsonListGasType,
            string Longitude, string Latitude, string District, string Address, string OpeningTime, string Rating)
        {
            var resultAdded = 0;

            // handle listGasType
            var listGasType = JsonListGasType != null ? JsonConvert.DeserializeObject<string[]>(JsonListGasType) : new string[] { };

            if (ModelState.IsValid)
            {
                GasStation gasStation = new GasStation();
                gasStation.GasStationName = GasStationName;
                gasStation.Longitude = Convert.ToDouble(Longitude);
                gasStation.Latitude = Convert.ToDouble(Latitude);
                gasStation.District = Convert.ToInt64(District);
                gasStation.Address = Address;
                gasStation.Rating = Rating;
                gasStation.OpeningTime = OpeningTime;
                gasStation.InsertedAt = DateTime.Now;
                gasStation.UpdatedAt = DateTime.Now;
                gasStation.InsertedBy = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
                gasStation.UpdatedBy = Convert.ToInt64(HttpContext.Session.GetString("UserId"));

                var checkExist = await _serviceList.getGasStationByName(GasStationName);
                if (checkExist != null) // GasStation name is exist from database
                {
                    ViewBag.GasStationNameExist = GasStationName;
                    return Json(BadRequest($"{GasStationName}名情報はすでに存在します。"));
                }
                else
                {
                    resultAdded = await _serviceAdd.addGasStation(gasStation);
                    // handle add GasStationGasType
                    string[] arrayGastype = listGasType;
                    GasStation gasStationLast = await _serviceList.GetGasStationLast(); // get the latest gasstation
                    foreach (var item in arrayGastype)
                    {
                        GasStationGasType gasStationGasType = new GasStationGasType(); // GasStationGasType
                        gasStationGasType.GasStationId = gasStationLast.GasStationId;
                        gasStationGasType.GasType = item;
                        await _serviceAdd.addGasStationGasType(gasStationGasType);
                    }
                }
            }
            if (resultAdded == 1)
            {
                return Json(Content("Ok"));
            }
            else
            {
                return Json(Content("Fail"));
            }
        }
        #endregion
    }
}

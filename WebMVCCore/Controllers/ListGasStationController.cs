using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.ViewModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;
namespace WebMVCCore.Controllers
{
    public class ListGasStationController : Controller
    {
        #region Variable
        private readonly ILogger<ListGasStationController> _logger;
        private readonly IListGasStationService _service;
        #endregion

        #region Constructor
        public ListGasStationController(ILogger<ListGasStationController> logger, IListGasStationService service)
        {
            _logger = logger;
            _service = service;

        }
        #endregion

        #region Function
        /// <summary>
        /// Get list GasStation when load first data or search
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="searchGasName"></param>
        /// <param name="searchDistrict"></param>
        /// <param name="jsonListGasType"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GasStationList(string searchGasName, string searchDistrict, string jsonListGasType, int? page)
        {
            // pagination GasStation
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Init data to Select options and Checkboxes 
            ViewBag.getDistrict = await _service.GetListDistrict(); // Order by DistrictName
            ViewBag.getType = await _service.GetType(); // Get list GasType

            // handle listGasType
            var listGasType = jsonListGasType != null ? JsonConvert.DeserializeObject<string[]>(jsonListGasType) : new string[] { };
            var stringListGasType = "";
            for (int i = 0; i < listGasType.Count(); i++)
            {
                stringListGasType += listGasType.Last() != listGasType[i] ? (listGasType[i] + ",") : listGasType[i];
            }

            // get list GasStation
            List<GasStationViewModel> gasStations = (List<GasStationViewModel>)await _service.getGasStation(searchGasName, searchDistrict, stringListGasType);

            // This viewbags use to render input, checkboxs and select options
            ViewBag.searchGasName = searchGasName;
            ViewBag.searchDistrict = searchDistrict;
            ViewBag.listGasType = listGasType;

            var gas = gasStations.ToPagedList(pageNumber, pageSize);
            ViewBag.listGasStation = gas;
            return View(gas);
        }

        [HttpPost]
        public async Task<JsonResult> GasStationDelete(long gasStationId)
        {
            var resultDeleted = await _service.deleteGasStation(gasStationId, Convert.ToInt64(HttpContext.Session.GetString("UserId")));
            if (resultDeleted == 1)
            {
                return Json(Content("Ok"));
            }
            else
            {
                return Json(Content("Fail"));
            }
        }

        /// <summary>
        /// Get list GasStation by AJAX when load first data or search
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="searchGasName"></param>
        /// <param name="searchDistrict"></param>
        /// <param name="jsonListGasType"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> LoadGasStationList(string searchGasName, string searchDistrict, string jsonListGasType, int? page)
        {
            // pagination GasStation
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Init data to Select options and Checkboxes 
            ViewBag.getDistrict = await _service.GetListDistrict(); // Order by DistrictName
            ViewBag.getType = await _service.GetType(); // Get list GasType

            // handle listGasType
            var listGasType = jsonListGasType != null ? JsonConvert.DeserializeObject<string[]>(jsonListGasType) : new string[] { };
            var stringListGasType = "";
            for (int i = 0; i < listGasType.Count(); i++)
            {
                stringListGasType += listGasType.Last() != listGasType[i] ? (listGasType[i] + ",") : listGasType[i];
            }

            // get list GasStation
            List<GasStationViewModel> gasStations = (List<GasStationViewModel>)await _service.getGasStation(searchGasName, searchDistrict, stringListGasType);

            // This viewbags use to render input, checkboxs and select options
            ViewBag.searchGasName = searchGasName;
            ViewBag.searchDistrict = searchDistrict;
            ViewBag.listGasType = listGasType;

            //var gas = gasStations.ToPagedList(pageNumber, pageSize);
            var gas = gasStations;
            return Json(gas);
            //return Json(new
            //{
            //    data = gas,
            //    status = true
            //});
        }

        /// <summary>
        /// Delete a GasStation by Id
        /// </summary>
        /// <author>huynhnvdev</author>
        /// <param name="gasStationId"></param>
        /// <returns></returns>
        #endregion
    }
}

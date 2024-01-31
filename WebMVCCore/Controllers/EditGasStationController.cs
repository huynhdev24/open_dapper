using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCCore.Controllers
{
    public class EditGasStationController : Controller
    {
        #region Variable
        private readonly ILogger<EditGasStationController> _logger;
        private readonly IEditGasStationService _serviceEdit;
        private readonly IListGasStationService _serviceList;
        #endregion

        #region Constructor
        public EditGasStationController(ILogger<EditGasStationController> logger, IEditGasStationService serviceEdit, IListGasStationService serviceList)
        {
            _logger = logger;
            _serviceEdit = serviceEdit;
            _serviceList = serviceList;
        }
        #endregion

        #region Function
        public async Task<IActionResult> GasStationEdit(long id)
        {
            ViewBag.getType = await _serviceList.GetType();
            ViewBag.getRating = await _serviceList.GetRating();
            ViewBag.getDistrict = await _serviceList.GetListDistrict();
            return View();
        }
        #endregion
    }
}

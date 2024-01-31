using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCCore.Controllers
{
    public class FeedBackGasStationController : Controller
    {
        #region Variable
        private readonly ILogger<FeedBackGasStationController> _logger;
        private readonly IFeedBackGasStationService _service;
        #endregion

        #region Constructor
        public FeedBackGasStationController(ILogger<FeedBackGasStationController> logger, IFeedBackGasStationService service)
        {
            _logger = logger;
            _service = service;

        }
        #endregion

        #region Function
        public async Task<IActionResult> GasStationFeedBackView(long id)
        {
            return View();
        }
        #endregion
    }
}

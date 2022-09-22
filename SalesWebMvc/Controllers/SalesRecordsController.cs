using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue && !maxDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
                maxDate = DateTime.Now;
            }

            if (minDate.HasValue)
            {
                ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            }
            if (maxDate.HasValue)
            {
                ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            }


            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}

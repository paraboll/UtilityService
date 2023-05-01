using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UtilityService.Extensions;
using UtilityService.Models;
using UtilityService.Repository.Interfaces;
using UtilityService.Services;

namespace UtilityService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        private ICurrentCoefficientRepository _currentCoefficientRepository;
        private IHistoryCoefficientRepository _historyCoefficientRepository;

        private IHistoryCalculationsRepository _historyCalculationsRepository;
        private IHistoryCounterValuesRepository _historyCounterValuesRepository;


        public HomeController(ILogger<HomeController> logger, 
            ICurrentCoefficientRepository currentCoefficientRepository,
            IHistoryCoefficientRepository historyCoefficientRepository,
            IHistoryCalculationsRepository historyCalculationsRepository,
            IHistoryCounterValuesRepository historyCounterValuesRepository)
        {
            _logger = logger;

            _currentCoefficientRepository = currentCoefficientRepository;
            _historyCoefficientRepository = historyCoefficientRepository;
            _historyCalculationsRepository = historyCalculationsRepository;
            _historyCounterValuesRepository = historyCounterValuesRepository;
        }

        [HttpGet]
        public IActionResult Index() 
        {
            var result = new IndexDTO()
            {
                CounterValues = _historyCounterValuesRepository.GetLastCounterValues(),
                CurrentCoefficients = _currentCoefficientRepository.GetCurrentCoefficients()
            };

            return View(result); 
        }

        [HttpPost]
        public IActionResult UpdateСoefficients(CurrentCoefficients coefficients)
        {
            var currentCoefficients =  _currentCoefficientRepository.GetCurrentCoefficients();
            if (currentCoefficients.Overwrite(coefficients))
            {
                _currentCoefficientRepository.SetCurrentCoefficients(currentCoefficients);
            }

            var result = new IndexDTO()
            {
                CurrentCoefficients = currentCoefficients,
                CounterValues = _historyCounterValuesRepository.GetLastCounterValues()
            };
             
            return View("Index", result); 
        }

        [HttpPost]
        public IActionResult CalculatePay(CounterValues counterValues)
        {
            counterValues.CheckCounterValues(_logger);

            var lastCounterValues = _historyCounterValuesRepository.GetLastCounterValues();
            var currentCounterValues = _currentCoefficientRepository.GetCurrentCoefficients();

            ISettlementService settlementService =
                new SettlementService(_logger, lastCounterValues, counterValues,
                currentCounterValues.Convert());

            var result = settlementService.CalculatePayment();
            result.Coefficients = currentCounterValues.Convert();
            result.CounterValues = counterValues;

            _historyCalculationsRepository.AddCalculations(result);

            return View("Index", currentCounterValues);
            
        }

        public IActionResult Report()
        {
            var reports = new List<Report>();

            //TODO: сервис изначально не расчитан на огромное количество записей.
            //на данном этапе нет смысла придумывать оптимизации на формирование
            //и отображение данных для отчета.
            var CalculationsIds = _historyCalculationsRepository.GetListCalculationsId();

            foreach (var CalculationsId in CalculationsIds)
            {
                reports.Add(new Report()
                {
                    HistoryCalculations = _historyCalculationsRepository.GetCalculationsById(CalculationsId),
                    HistoryCoefficients = _historyCoefficientRepository.GetCoefficientsById(CalculationsId),
                    HistoryCounterValues = _historyCounterValuesRepository.GetCounterValuesById(CalculationsId)
                });
            }

            return View("Report", reports);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var result = new IndexDTO()
            {
                CounterValues = _historyCounterValuesRepository.GetLastCounterValues(),
                CurrentCoefficients = _currentCoefficientRepository.GetCurrentCoefficients()
            };

            return View("Index", result);
        }
    }
}

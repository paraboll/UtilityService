using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using UtilityService.Models;
using UtilityService.Repository.Interfaces;

namespace UtilityService.Repository
{
    public class CurrentCoefficientRepository : ICurrentCoefficientRepository
    {
        private readonly ILogger _log;
        private readonly UtilityDBContext _dbContext;

        public CurrentCoefficientRepository(ILogger<CurrentCoefficientRepository> log, UtilityDBContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;
        }

        public CurrentCoefficients GetCurrentCoefficients()
        {
            _log.LogTrace($"Вызван метод GetCurrentCoefficients.");

            try
            {
               var result = _dbContext.CurrentCoefficients.FirstOrDefault();
                if(result != null)
                {
                    return result;
                }

                _log.LogTrace($"Таблица пуста.");
                return new CurrentCoefficients();
            }
            catch (Exception exc)
            {
                _log.LogError($"GetCurrentCoefficients: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить коэффициенты, произошла ошибка: {exc.Message}");
            }
        }

        public void SetCurrentCoefficients(CurrentCoefficients coefficients)
        {
            _log.LogTrace($"Вызван метод SetCurrentCoefficients. Запись: {JsonConvert.SerializeObject(coefficients)}");
            try
            {
                var result = _dbContext.CurrentCoefficients.FirstOrDefault();
                if (result != null)
                {
                    result = coefficients;
                }
                else
                {
                    _log.LogTrace($"Таблица пуста. Добавим запись {JsonConvert.SerializeObject(coefficients)}");
                    _dbContext.CurrentCoefficients.Add(coefficients);
                }

                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                _log.LogError($"SetCurrentCoefficients: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке обновить коэффициенты, произошла ошибка: {exc.Message}");
            }
        }
    }
}

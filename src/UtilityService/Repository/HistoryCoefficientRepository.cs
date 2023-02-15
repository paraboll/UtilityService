using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilityService.Models;
using UtilityService.Repository.Interfaces;

namespace UtilityService.Repository
{
    public class HistoryCoefficientRepository : IHistoryCoefficientRepository
    {
        private readonly ILogger _log;
        private readonly UtilityDBContext _dbContext;

        public HistoryCoefficientRepository(ILogger<HistoryCoefficientRepository> log, UtilityDBContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;
        }

        public void AddCoefficients(Coefficients coefficients)
        {
            _log.LogTrace($"Вызван метод AddCoefficients. Запись: {JsonConvert.SerializeObject(coefficients)}");
            try
            {
                _dbContext.HistoryCoefficients.Add(coefficients);
                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                _log.LogError($"AddCoefficients: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке добавить значения коэффициентов произошла ошибка: {exc.Message}");
            }
        }

        public IEnumerable<Coefficients> GetAllCoefficients()
        {
            _log.LogTrace($"Вызван метод GetAllCoefficients.");
            try
            {
                return _dbContext.HistoryCoefficients.ToList();
            }
            catch(Exception exc)
            {
                _log.LogError($"GetAllCoefficients: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить историю коэффициентов, произошла ошибка: {exc.Message}");
            }
        }

        public Coefficients GetCoefficientsById(int id)
        {
            _log.LogTrace($"Вызван метод GetCoefficientsById({id})");
            try
            {
                var result = _dbContext.HistoryCoefficients.FirstOrDefault(_ => _.Id == id);
                if(result != null)
                {
                    return result;
                }

                _log.LogTrace("Таблица пуста.");
                return new Coefficients();
            }
            catch (Exception exc)
            {
                _log.LogError($"GetCoefficientsById: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить коэффициенты по id, произошла ошибка: {exc.Message}");
            }
        }
    }
}

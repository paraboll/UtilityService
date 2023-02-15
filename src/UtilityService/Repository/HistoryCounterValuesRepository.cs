using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilityService.Models;
using UtilityService.Repository.Interfaces;

namespace UtilityService.Repository
{
    public class HistoryCounterValuesRepository : IHistoryCounterValuesRepository
    {
        private readonly ILogger _log;
        private readonly UtilityDBContext _dbContext;

        public HistoryCounterValuesRepository(ILogger<HistoryCounterValuesRepository> log, UtilityDBContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;
        }

        public void AddCounterValues(CounterValues counterValues)
        {
            _log.LogTrace($"Вызван метод AddCounterValues. Запись: {JsonConvert.SerializeObject(counterValues)}");
            try
            {
                _dbContext.HistoryCounterValues.Add(counterValues);
                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                _log.LogError($"AddCoefficients: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке добавить показания счетчиков произошла ошибка: {exc.Message}");
            }

        }

        public IEnumerable<CounterValues> GetAllCounterValues()
        {
            _log.LogTrace($"Вызван метод GetAllCounterValues.");
            try
            {
                return _dbContext.HistoryCounterValues.ToList();
            }
            catch(Exception exc)
            {
                _log.LogError($"GetAllCounterValues: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить историю показаний счетчиков произошла ошибка: {exc.Message}");
            }
        }

        public CounterValues GetCounterValuesById(int id)
        {
            _log.LogTrace($"Вызван метод GetCounterValuesById({id}).");
            try
            {
                var result = _dbContext.HistoryCounterValues.FirstOrDefault(_ => _.Id == id);
                if(result != null)
                {
                    return result;
                }

                _log.LogTrace("Таблица пуста.");
                return new CounterValues();
            }
            catch (Exception exc)
            {
                _log.LogError($"GetCounterValuesById: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить показания счетчиков по id произошла ошибка: {exc.Message}");
            }
        }

        public CounterValues GetLastCounterValues()
        {
            _log.LogTrace($"Вызван метод GetLastCounterValues.");
            try
            {
                var result = _dbContext.HistoryCounterValues.ToList().LastOrDefault();
                if(result != null)
                {
                    return result;
                }

                _log.LogTrace("Таблица пуста.");
                return new CounterValues();
            }
            catch (Exception exc)
            {
                _log.LogError($"GetLastCounterValues: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить крайние показания счетчиков произошла ошибка: {exc.Message}");
            }
        }
    }
}

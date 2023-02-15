using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilityService.Models;
using UtilityService.Repository.Interfaces;

namespace UtilityService.Repository
{
    public class HistoryCalculationsRepository : IHistoryCalculationsRepository
    {
        private readonly ILogger _log;
        private readonly UtilityDBContext _dbContext;

        public HistoryCalculationsRepository(ILogger<HistoryCalculationsRepository> log, UtilityDBContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;
        }

        public IEnumerable<Calculations> GetAllCalculations()
        {
            _log.LogTrace($"Вызван метод GetAllCalculations.");
            try
            {
                return _dbContext.HistoryCalculations.ToList();
            }
            catch (Exception exc)
            {
                _log.LogError($"GetAllCalculations: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить историю расчетов, произошла ошибка: {exc.Message}");
            }
        }
        
        public void AddCalculations(Calculations calculations)
        {
            _log.LogTrace($"Вызван метод AddCalculations. Запись: {JsonConvert.SerializeObject(calculations)}");
            try
            {
                _dbContext.HistoryCalculations.Add(calculations);
                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                _log.LogError($"AddCalculations: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке добавить расчет, произошла ошибка: {exc.Message}");
            }
        }
        
        public Calculations GetLastCalculations()
        {
            _log.LogTrace($"Вызван метод GetLastCalculations");
            try
            {
                var result = _dbContext.HistoryCalculations
                                        .ToList()
                                        .LastOrDefault();
                if(result != null)
                {
                    return result;
                }

                _log.LogTrace("Таблица пуста.");
                return new Calculations();
            }
            catch (Exception exc)
            {
                _log.LogError($"GetLastCalculations: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить крайний расчет, произошла ошибка: {exc.Message}");
            }
        }

        public Calculations GetCalculationsById(int id)
        {
             _log.LogTrace($"Вызван метод GetCalculationsById({id})");
            try
            {
                var result = _dbContext.HistoryCalculations.FirstOrDefault(_ => _.Id == id);
                if(result != null)
                {
                    return result;
                }

                _log.LogTrace($"Таблица пуста.");
                return new Calculations();
            }
            catch (Exception exc)
            {
                 _log.LogError($"GetCalculationsById: {exc.Message} - {exc.StackTrace}");
                throw new Exception($"При попытке получить расчет по id, произошла ошибка: {exc.Message}");
            }
        }

        public int GetItemCount()
        {
            _log.LogTrace($"Вызван метод GetItemCount.");
            return _dbContext.HistoryCalculations.Count();
        }
    }
}

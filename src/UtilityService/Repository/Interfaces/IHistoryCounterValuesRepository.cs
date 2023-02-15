using System.Collections.Generic;
using UtilityService.Models;

namespace UtilityService.Repository.Interfaces
{
    public interface IHistoryCounterValuesRepository
    {
        IEnumerable<CounterValues> GetAllCounterValues();
        CounterValues GetLastCounterValues();
        CounterValues GetCounterValuesById(int id);
        void AddCounterValues(CounterValues counterValues);
    }
}

using System.Collections.Generic;
using UtilityService.Models;

namespace UtilityService.Repository.Interfaces
{
    public interface IHistoryCalculationsRepository
    {
        IEnumerable<Calculations> GetAllCalculations();
        Calculations GetLastCalculations();
        Calculations GetCalculationsById(int id);
        void AddCalculations(Calculations coefficients);
        IEnumerable<int> GetListCalculationsId();
        int GetItemCount();
    }
}

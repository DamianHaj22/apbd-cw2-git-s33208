using System;

namespace EquipmentRental.Application.Policies
{
    public class DailyPenaltyCalculator : IPenaltyCalculator
    {
        private readonly decimal _dailyRate;
        
        public DailyPenaltyCalculator(decimal dailyRate = 10m)
        {
            _dailyRate = dailyRate;
        }

        public decimal CalculatePenalty(DateTime expectedReturnDate, DateTime actualReturnDate)
        {
            if (actualReturnDate.Date <= expectedReturnDate.Date)
            {
                return 0m; // kara wynosi 0 jezeli oddane w terminie
            }
            
            int daysLaye = (actualReturnDate.Date - expectedReturnDate).Days;
            return daysLaye * _dailyRate;
        }
    }
}
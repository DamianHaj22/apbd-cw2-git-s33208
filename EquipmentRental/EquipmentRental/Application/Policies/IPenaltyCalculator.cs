using System;

namespace EquipmentRental.Application.Policies
{
    public interface IPenaltyCalculator
    {
        decimal CalculatePenalty(DateTime expectedReturnDate, DateTime actualReturnDate);
    }
}
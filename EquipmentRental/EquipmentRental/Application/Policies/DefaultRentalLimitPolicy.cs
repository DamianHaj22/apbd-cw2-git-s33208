using EquipmentRental.Domain.Models;

namespace EquipmentRental.Application.Policies
{
    public class DefaultRentalLimitPolicy : IRentalLimitPolicy
    {
        public int GetMaxActiveRentals(UserType userType)
        {
            switch (userType)
            {
                case UserType.Student:
                    return 2;
                case UserType.Employee:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
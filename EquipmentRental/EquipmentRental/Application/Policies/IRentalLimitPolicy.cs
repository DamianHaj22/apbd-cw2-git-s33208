using EquipmentRental.Domain.Models;

namespace EquipmentRental.Application.Policies
{
    public interface IRentalLimitPolicy
    {
        int GetMaxActiveRentals(UserType userType);
    }
}
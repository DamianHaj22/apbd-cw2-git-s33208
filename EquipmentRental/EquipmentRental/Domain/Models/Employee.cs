namespace EquipmentRental.Domain.Models
{
    public class Employee : User
    {
        public string Department { get; }

        public Employee(string firstName, string lastName, string department) : base(firstName, lastName,
            UserType.Employee)
        {
            Department = department;
        }
    }
}
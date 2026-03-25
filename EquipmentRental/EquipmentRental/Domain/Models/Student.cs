namespace EquipmentRental.Domain.Models
{
    public class Student : User
    {
        public string StudentName { get; }
        
        public Student(string firstName, string lastName, string email, string phoneNumber) : base(firstName, lastName, UserType.Student)
            {
            StudentName = firstName;
            }
    }
}
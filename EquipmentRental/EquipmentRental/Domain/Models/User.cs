using System;

namespace EquipmentRental.Domain.Models
{
    public abstract class User
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public UserType Type { get; }

        protected User(string firstName, string lastName, UserType type)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Type = type;
        }
    }
}